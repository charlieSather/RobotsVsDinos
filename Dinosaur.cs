using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Dinosaur
    {
        public string type { get; private set; }
        double health;
        double energy;
        public double attackPower;
        public List<AttackType> attacks { get; private set; } 

        public Dinosaur(string type, double health, double energy, double attackPower, List<AttackType> attacks)
        {
            this.type = type;
            this.health = health;
            this.energy = energy;
            this.attackPower = attackPower;
            this.attacks = attacks;
        }
        
        public bool Attack(Robot robot, AttackType attack)
        {
            double attackCost = 25;
            attackCost *= attack.energyMultiplier;

            if(energy > attackCost)
            {
                robot.IncomingHit(attackPower * attack.attackMultiplier);
                energy -= attackCost;
                return true;
            }
            else
            {
                Console.WriteLine($"{type} only has {energy} points of energy which is not enough to attack!");
                Console.WriteLine($"{type} restores 15 points of energy");
                energy += 10;
                return false;

            }
        }

        public void IncomingHit(double damage)
        {
            if (health < damage)
            {
                health = 0;
                Console.WriteLine($"{type} has died!!!");
            }
            else
            {
                health -= damage;
                if (health == 0)
                {
                    Console.WriteLine($"{type} has died!!!");
                }
            }
        }
        public bool IsAlive()
        {
            return health > 0 ? true : false;
        }
        public void DisplayAttacks()
        {
            for(int i = 0; i < attacks.Count; i++)
            {
                Console.WriteLine($"{i}: {attacks[i].ToString()}");
            }
        }
        public AttackType SelectAttack()
        {
            bool validInput = false;
            int AttackIndex = 0;
            while (validInput == false)
            {
                Console.WriteLine($"{type} please select an attack (enter number)");
                DisplayAttacks();
                try
                {
                    AttackIndex = int.Parse(Console.ReadLine());
                    if (AttackIndex >= 0 && AttackIndex < attacks.Count)
                    {
                        validInput = true;
                      //return attacks[AttackIndex];
                    }
                    else
                    {
                        Console.WriteLine("Sorry invalid selection please try again");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }             

            }
            return attacks[AttackIndex];
        }


        public double Health()
        {
            return health;
        }

        public string DisplayStatus()
        {
            return string.Format("Type: {0}, Health: {1}, Energy: {2}, Attack Power: {3}", type, health, energy, attackPower);
        }



    }
}
