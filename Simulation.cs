using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Simulation
    {
        Fleet robots;
        Herd dinosaurs;
        Battlefield battlefield;

        public void init()
        {
           
            Robot robo1 = new Robot("Calvin", 1000, 150, new Weapon("Pistol",75));
            Robot robo2 = new Robot("Steve", 1000, 200, new Weapon("Slingshot", 10));
            Robot robo3 = new Robot("Chris", 1000, 200, new Weapon("MK2 Carbine",150));

            robots = new Fleet(new List<Robot>{ robo1, robo2, robo3});

            Dinosaur dino1 = new Dinosaur("Trex", 1000, 400, 200);
            Dinosaur dino2 = new Dinosaur("Brontosaurus", 600, 250, 100);
            Dinosaur dino3 = new Dinosaur("Spinosaurus", 700, 400, 250);

            dinosaurs = new Herd(new List<Dinosaur> { dino1, dino2, dino3 });

            battlefield = new Battlefield(dinosaurs, robots);
        }

        public void Run()
        {
            init();

            while(battlefield.BattleIsActive() == true)
            {
                SimulateBattle();
                battlefield.DisplayStats(dinosaurs,robots);
                Console.ReadLine();
            }

            battlefield.DisplayWinner();
            Console.ReadLine();
        }

        //turns consist of random matchups of dino vs robot, and rng decides who attacks who in those matchups
        public void SimulateBattle()
        {
            (Dinosaur, Robot) matchup = battlefield.GetMatchup(dinosaurs,robots);

            int turn;
            turn = battlefield.RandomNumber(0,2);

            bool successfulAttack;

            //0 ---> dinosaur attacks robot
            //1 ---> robot attacks dinosaur
            if(turn == 0)
            {
                successfulAttack = matchup.Item1.Attack(matchup.Item2);
                if(successfulAttack == true)
                {
                    Console.WriteLine($"{matchup.Item1.type} hit {matchup.Item2.name} for {matchup.Item1.attackPower} points of damage!");
                    Console.WriteLine($"{matchup.Item2.name} now has {matchup.Item2.Health()} health points left!");
                }
                else
                {
                    //TODO Attack missed
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
                else
                {
                    //TODO attack missed
                }
            }
        }

    }
}
