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
            weaponList = new WeaponList();

            Robot robo1 = new Robot("Calvin", 1200, 300, new Weapon());
            Robot robo2 = new Robot("Steve", 1200, 375, new Weapon());
            Robot robo3 = new Robot("Chris", 1200, 400, new Weapon());

            robots = new Fleet(new List<Robot>{ robo1, robo2, robo3});

           foreach(Robot robot in robots.robots)
            {
                robot.UpdateWeapon(robot.SelectWeapon(weaponList));
                //robot.UpdateWeapon(AutomateWeaponSelection(8));

            }
           // PromptRobotCHoiceOrRandom();

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
                    attack = matchup.Item1.SelectAttack();
                   // attack = AutomateDinosaurAttacks(matchup.Item1);
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

        public void PromptRobotCHoiceOrRandom()
        {
            Console.WriteLine("Do you wish to let robots choose a weapon(yes or no)");
            string input;
            bool ValidInput = false;

            while(ValidInput == false)
            {
                input = Console.ReadLine();
                if(input.ToLower() == "yes")
                {
                    ValidInput = true;
                    foreach (Robot robot in robots.robots)
                    {
                        robot.UpdateWeapon(robot.SelectWeapon(weaponList));

                    }
                }
                else if(input.ToLower() == "no")
                {
                    ValidInput = true;
                    foreach (Robot robot in robots.robots)
                    {
                        robot.UpdateWeapon(AutomateWeaponSelection(weaponList.weapons.Count));
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please try again!");
                }
            }


        }
        public void PromptDinosaurAttacks()
        {

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
