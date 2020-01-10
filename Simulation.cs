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

            Robot robo1 = new Robot("Calvin", 1200, 300, new Weapon());
            Robot robo2 = new Robot("Steve", 1200, 375, new Weapon());
            Robot robo3 = new Robot("Chris", 1200, 400, new Weapon());

            robots = new Fleet(new List<Robot>{ robo1, robo2, robo3});

            foreach(Robot robot in robots.robots)
            {
                //robot.UpdateWeapon(robot.SelectWeapon(weaponList));
                robot.UpdateWeapon(AutomateWeaponSelection(8));

            }

            initAttacks();

            Dinosaur dino1 = new Dinosaur("Trex", 900, 275, 85, attacks);
            Dinosaur dino2 = new Dinosaur("Brontosaurus", 1200, 200, 55, attacks);
            Dinosaur dino3 = new Dinosaur("Spinosaurus", 725, 300, 115, attacks);

            dinosaurs = new Herd(new List<Dinosaur> { dino1, dino2, dino3 });

            battlefield = new Battlefield(dinosaurs, robots);

        }

        public void initAttacks()
        {
            AttackType one = new AttackType("Tail Whip", 0.95, 0.9);
            AttackType two = new AttackType("Swipe", 1.2, 1.2);
            AttackType three = new AttackType("Stomp", 1.5, 2);
            attacks = new List<AttackType>{ one, two, three };
        }

        public void initWeapons()
        {
            Weapon weapon1 = new Weapon("Pistol", 65, 0.75);
            Weapon weapon2 = new Weapon("Crossbow", 50, 0.6);
            Weapon weapon3 = new Weapon("M4A4", 100, 1.25);
            Weapon weapon4 = new Weapon("Slingshot", 25, 0.25);
            Weapon weapon5 = new Weapon("Sniper Rifle", 150, 1.75);
            Weapon weapon6 = new Weapon("Rocket Launcher", 200, 2.5);
            Weapon weapon7 = new Weapon("Rifle", 85, 1);
            Weapon weapon8 = new Weapon("Shotgun", 175, 2);

            weaponList = new WeaponList (new List<Weapon> { weapon1, weapon2, weapon3, weapon4, weapon5, weapon6, weapon7, weapon8 });

        }

        public void Run()
        {
            init();

            while(battlefield.BattleIsActive() == true)
            {
                SimulateBattleRound();
                //SimulateWithChoice();
                Console.WriteLine();
                //Thread.Sleep(250);
            }

            battlefield.DisplayWinner(dinosaurs,robots);
            Console.WriteLine();
            battlefield.DisplayStats(dinosaurs, robots);
            Console.ReadLine();
        }

        //turns consist of random matchups of dino vs robot, and rng decides who attacks who in those matchups
        public void SimulateBattleRound()
        {
            (Dinosaur, Robot) matchup = battlefield.GetMatchup(dinosaurs,robots);

            //0 ---> dinosaur attacks robot
            //1 ---> robot attacks dinosaur

            int Attacker;
            Attacker = battlefield.RandomNumber(2);

            bool successfulAttack;
            bool hitchance = battlefield.HitChance();
            AttackType attack;

            if(hitchance == true)
            {
                if (Attacker == 0)
                {
                    Console.WriteLine(matchup.Item1.DisplayStatus());
                    //attack = matchup.Item1.SelectAttack();
                    attack = AutomateDinosaurAttacks(matchup.Item1);
                    successfulAttack = matchup.Item1.Attack(matchup.Item2, attack);
                    if (successfulAttack == true)
                    {
                        Console.WriteLine($"{matchup.Item1.type} hit {matchup.Item2.name} for {matchup.Item1.attackPower * attack.attackMultiplier} points of damage!");
                        Console.WriteLine($"{matchup.Item2.name} now has {matchup.Item2.health} health points left!");
                    }
                    Console.WriteLine(matchup.Item1.DisplayStatus());
                }
                else
                {
                    Console.WriteLine(matchup.Item2.DisplayStatus());
                    successfulAttack = matchup.Item2.Attack(matchup.Item1);
                    if (successfulAttack == true)
                    {
                        Console.WriteLine($"{matchup.Item2.name} hit {matchup.Item1.type} for {matchup.Item2.weapon.attackPower} points of damage!");
                        Console.WriteLine($"{matchup.Item1.type} now has {matchup.Item1.health} health points left!");
                    }
                    Console.WriteLine(matchup.Item2.DisplayStatus());
                }
            }
            else
            {
                if(Attacker == 0)
                {
                    Console.WriteLine($"{matchup.Item1.type} missed {matchup.Item2.name}!!!");
                }
                else
                {
                    Console.WriteLine($"{matchup.Item2.name} missed {matchup.Item1.type}!!!");

                }
            }
            //Console.ReadLine();
            //Console.Clear();
            battlefield.HealRemainingCompetitors(dinosaurs, robots);
        }
        public void SimulateWithChoice()
        {
            bool successfulAttack;
            bool hitchance = battlefield.HitChance();

            Robot dinoTarget;
            Dinosaur roboTarget;
            AttackType attack;


            foreach (Dinosaur dino in dinosaurs.dinosaurs)
            {
                dinoTarget = battlefield.ChooseRobot(dino, robots);
                attack = dino.SelectAttack();

                successfulAttack = dino.Attack(dinoTarget,attack);
                if(successfulAttack == true)
                {                   
                   Console.WriteLine($"{dino.type} hit {dinoTarget.name} for {dino.attackPower * attack.attackMultiplier} points of damage!");
                   Console.WriteLine($"{dinoTarget.name} now has {dinoTarget.health} health points left!");

                }
                Console.WriteLine(dino.DisplayStatus());

            }
            foreach (Robot robo in robots.robots)
            {
                roboTarget = battlefield.ChooseDinosaur(robo, dinosaurs);
                robo.Attack(roboTarget);

                successfulAttack = robo.Attack(roboTarget);
                if (successfulAttack == true)
                {
                    Console.WriteLine($"{robo.name} hit {roboTarget.type} for {robo.weapon.attackPower} points of damage!");
                    Console.WriteLine($"{roboTarget.type} now has {roboTarget.health} health points left!");

                }
                Console.WriteLine(robo.DisplayStatus());
            }

        }
                              

        public AttackType AutomateDinosaurAttacks(Dinosaur dino)
        {
            return dino.attacks[battlefield.RandomNumber(3)];
        }

        public Weapon AutomateWeaponSelection(int range)
        {
            return weaponList.weapons[new Battlefield(dinosaurs, robots).RandomNumber(range)];
        }

    }
}
