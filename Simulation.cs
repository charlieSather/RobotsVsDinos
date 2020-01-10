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
        WeaponList weaponList;
        Battlefield battlefield;

        public void init()
        {
            initWeapons();

            Robot robo1 = new Robot("Calvin", 1200, 150, new Weapon());
            Robot robo2 = new Robot("Steve", 1200, 200, new Weapon());
            Robot robo3 = new Robot("Chris", 1200, 200, new Weapon());

            robots = new Fleet(new List<Robot>{ robo1, robo2, robo3});

            foreach(Robot robot in robots.robots)
            {
                robot.UpdateWeapon(robot.SelectWeapon(weaponList));
            }

            initAttacks();

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

        public void initWeapons()
        {
            Weapon weapon1 = new Weapon("Pistol", 65, 0.75);
            Weapon weapon2 = new Weapon("Crossbow", 50, 0.6);
            Weapon weapon3 = new Weapon("M4A4", 100, 1.25);
            Weapon weapon4 = new Weapon("Slingshot", 25, 0.5);
            Weapon weapon5 = new Weapon("Sniper Rifle", 150, 1.5);
            Weapon weapon6 = new Weapon("Rocket Launcher", 200, 2);
            Weapon weapon7 = new Weapon("Rifle", 85, 0.9);
            Weapon weapon8 = new Weapon("Shotgun", 175, 1.75);

            weaponList = new WeaponList (new List<Weapon> { weapon1, weapon2, weapon3, weapon4, weapon5, weapon6, weapon7, weapon8 });

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
            AttackType attack;

            //0 ---> dinosaur attacks robot
            //1 ---> robot attacks dinosaur


            if(hitchance == true)
            {
                if (turn == 0)
                {
                    Console.WriteLine(matchup.Item1.DisplayStatus());
                    attack = matchup.Item1.SelectAttack();
                    successfulAttack = matchup.Item1.Attack(matchup.Item2, attack);
                    if (successfulAttack == true)
                    {
                        Console.WriteLine($"{matchup.Item1.type} hit {matchup.Item2.name} for {matchup.Item1.attackPower * attack.attackMultiplier} points of damage!");
                        Console.WriteLine($"{matchup.Item2.name} now has {matchup.Item2.Health()} health points left!");
                    }
                    
                }
                else
                {
                    successfulAttack = matchup.Item2.Attack(matchup.Item1);
                    if (successfulAttack == true)
                    {
                        Console.WriteLine($"{matchup.Item2.name} hit {matchup.Item1.type} for {matchup.Item2.weapon.attackPower} points of damage!");
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
