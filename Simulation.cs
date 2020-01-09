using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Simulation
    {
        Fleet robots;
        Herd dinosaurs;
        List<AttackType> attacks;
        Battlefield battlefield;

        public void init()
        {
            Robot robo1 = new Robot("Calvin", 1200, 150, new Weapon("Pistol",75));
            Robot robo2 = new Robot("Steve", 1200, 200, new Weapon("Slingshot", 50));
            Robot robo3 = new Robot("Chris", 1200, 200, new Weapon("MK2 Carbine",150));

            robots = new Fleet(new List<Robot>{ robo1, robo2, robo3});

            Dinosaur dino1 = new Dinosaur("Trex", 900, 250, 100, attacks);
            Dinosaur dino2 = new Dinosaur("Brontosaurus", 1200, 150, 50, attacks);
            Dinosaur dino3 = new Dinosaur("Spinosaurus", 700, 350, 125, attacks);

            dinosaurs = new Herd(new List<Dinosaur> { dino1, dino2, dino3 });

            battlefield = new Battlefield(dinosaurs, robots);
        }

        public void initAttacks()
        {
            AttackType one = new AttackType("Tail Whip", 0.95, 0.8);
            AttackType two = new AttackType("Swipe", 1.2, 1.2);
            AttackType three = new AttackType("Stomp", 1.5, 2);
            attacks = new List<AttackType>{ one, two, three };
        }

        public void Run()
        {
            init();

            while(battlefield.BattleIsActive() == true)
            
            {
                SimulateBattle();
                Console.WriteLine();
                //Thread.Sleep(250);
            }

            battlefield.DisplayWinner(dinosaurs,robots);
            Console.WriteLine();
            battlefield.DisplayStats(dinosaurs, robots);
            Console.ReadLine();
        }

        //turns consist of random matchups of dino vs robot, and rng decides who attacks who in those matchups
        public void SimulateBattle()
        {
            (Dinosaur, Robot) matchup = battlefield.GetMatchup(dinosaurs,robots);

            int turn;
            turn = battlefield.RandomNumber(2);

            bool successfulAttack;
            bool hitchance = battlefield.HitChance();

            //0 ---> dinosaur attacks robot
            //1 ---> robot attacks dinosaur


            if(hitchance == true)
            {
                if (turn == 0)
                {
                    successfulAttack = matchup.Item1.Attack(matchup.Item2);
                    if (successfulAttack == true)
                    {
                        Console.WriteLine($"{matchup.Item1.type} hit {matchup.Item2.name} for {matchup.Item1.attackPower} points of damage!");
                        Console.WriteLine($"{matchup.Item2.name} now has {matchup.Item2.Health()} health points left!");
                    }
                    
                }
                else
                {
                    successfulAttack = matchup.Item2.Attack(matchup.Item1);
                    if (successfulAttack == true)
                    {
                        Console.WriteLine($"{matchup.Item2.name} hit {matchup.Item1.type} for {matchup.Item2.weapon.Damage()} points of damage!");
                        Console.WriteLine($"{matchup.Item1.type} now has {matchup.Item1.Health()} health points left!");
                    }                  
                }
            }
            else
            {
                if(turn == 0)
                {
                    Console.WriteLine($"{matchup.Item1.type} missed {matchup.Item2.name}!!!");
                }
                else
                {
                    Console.WriteLine($"{matchup.Item2.name} missed {matchup.Item1.type}!!!");

                }
            }

           
        }

    }
}
