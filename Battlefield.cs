using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Battlefield
    {
        Herd herd;
        Fleet fleet;

        public static Random rand = new Random();

        public Battlefield(Herd herd, Fleet fleet)
        {
            this.herd = herd;
            this.fleet = fleet;
        }     

        public bool HitChance()
        {
            return RandomNumber(11) > 0 ? true : false;
        }
        public bool BattleIsActive()
        {
            bool checkHerd = herd.HasSurvivors();
            bool checkFleet = fleet.HasSurvivors();

            if (checkHerd == false || checkFleet == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public (Dinosaur, Robot) GetMatchup(Herd herd, Fleet fleet)
        {
            int dinoIndex = RandomNumber(herd.dinosaurs.Count);
            int roboIndex = RandomNumber(fleet.robots.Count);    

            Dinosaur dinosaur = null;
            Robot robot = null;
            while(dinosaur == null)
            {
                if (herd.dinosaurs[dinoIndex].IsAlive() == true)
                {
                    dinosaur = herd.dinosaurs[dinoIndex];
                }
                else
                {
                    dinoIndex  = RandomNumber(herd.dinosaurs.Count);
                }
            }

            while (robot == null)
            {
                if (fleet.robots[roboIndex].IsAlive() == true)
                {
                    robot = fleet.robots[roboIndex];
                }
                else
                {
                    roboIndex = RandomNumber(fleet.robots.Count);
                }
            }
            return (dinosaur, robot);
        }

        public void DisplayStats(Herd herd, Fleet fleet)
        {
            foreach(Dinosaur dino in herd.dinosaurs)
            {
                Console.WriteLine(dino.DisplayStatus());
            }

            foreach(Robot robo in fleet.robots)
            {
                Console.WriteLine(robo.DisplayStatus());
            }
        }
        public int RandomNumber(int range)
        {
            return rand.Next(range);
        }
        public void DisplayWinner(Herd herd, Fleet fleet)
        {
            if (herd.HasSurvivors() == true)
            {
                Console.WriteLine("Winner winner chicken dinner!!!\nTeam Dinosaur is victorious!!!");
            }
            else
            {
                Console.WriteLine("Winner winner chicken dinner!!!\nTeam Robot is victorious!!!");
            }
        }
        public Robot ChooseRobot(Dinosaur dino, Fleet fleet)
        {
            Console.WriteLine($"{dino.type} choose your target (enter robot name)");
            foreach(Robot robot in fleet.robots)
            {
                if(robot.IsAlive() == true)
                {
                    Console.WriteLine(robot.name);
                }
            }

            bool validInput = false;
            string input;

            Robot target = null;

            while(validInput == false)
            {
                input = Console.ReadLine();
                foreach(Robot robot in fleet.robots)
                {
                    if(robot.name == input && robot.health > 0)
                    {
                        validInput = true;
                        return robot;
                        //target = robot;                        
                        //break;
                    }
                }
                Console.WriteLine("Oops we're having trouble finding the robot you want to destroy");
            }
            return target;      

        }
        public Dinosaur ChooseDinosaur(Robot robot, Herd herd)
        {
            Console.WriteLine($"{robot.name} choose your target (enter dinosaur name)");
            foreach (Dinosaur dino in herd.dinosaurs)
            {
                if (dino.IsAlive() == true)
                {
                    Console.WriteLine(dino.type);
                }
            }

            bool validInput = false;
            string input;

            Dinosaur target = null;

            while (validInput == false)
            {
                input = Console.ReadLine();
                foreach (Dinosaur dino in herd.dinosaurs)
                {
                    if (dino.type == input && dino.health > 0)
                    {
                        validInput = true;
                        return dino;
                        //target = dino;
                        //break;
                    }                    
                }
                Console.WriteLine("Oops we're having trouble finding the dinosaur you're trying to destroy");
            }
            return target;
        }

        public void HealRemainingCompetitors(Herd herd, Fleet fleet)
        {
            foreach(Dinosaur dino in herd.dinosaurs)
            {
                dino.RegenHealth();
            }
            foreach(Robot robot in fleet.robots)
            {
                robot.RegenHealth();
            }

        }
    }
}
