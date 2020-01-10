using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Robot
    {
        public string name;
        double health;
        double powerLevel;
        public Weapon weapon;

        public Robot(string name, double health, double powerLevel, Weapon weapon)
        {
            this.name = name;
            this.health = health;
            this.powerLevel = powerLevel;
            this.weapon = weapon;
        }
        public void UpdateWeapon(Weapon weapon)
        {
            this.weapon = weapon;
        }

        public bool Attack(Dinosaur dinosaur)
        {
            double AttackCost = 50 * weapon.drainMultiplier;

            if(powerLevel > AttackCost)
            {
                powerLevel -= AttackCost;
                dinosaur.IncomingHit(weapon.attackPower);
                return true;
            }
            else
            {
                Console.WriteLine($"{name}'s Power Level is only { powerLevel } and not enough to attack");
                Console.WriteLine($"{name} restores his powerLevel by 25");
                powerLevel += 25;
                return false;
            }

        }
        public Weapon SelectWeapon(WeaponList weapons)
        {
            bool validInput = false;
            int WeaponIndex = 0;
            while (validInput == false)
            {
                Console.WriteLine($"{name} please select your weapon (enter number)");
                weapons.DisplayWeapons();
                try
                {
                    WeaponIndex = int.Parse(Console.ReadLine());
                    if (WeaponIndex >= 0 && WeaponIndex < weapons.weapons.Count)
                    {
                        validInput = true;
                        //return weapons[AttackIndex];
                    }
                    else
                    {
                        Console.WriteLine("Sorry invalid selection please try again");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
            return weapons.weapons[WeaponIndex];
        }


        public void IncomingHit(double damage)
        {
            if(health < damage)
            {
                health = 0;
                Console.WriteLine($"{name} has died!!!");
            }
            else
            {
                health -= damage;
                if(health == 0)
                {
                    Console.WriteLine($"{name} has died!!!");
                }
            }
        }
        public double Health()
        {
            return health;
        }
        public bool IsAlive()
        {
            return health > 0 ? true : false;
        }

        public string DisplayStatus()
        {
            return string.Format("(Name: {0}, Health: {1}, Power Level: {2}, Weapon: {3})", name, health, powerLevel, weapon.ToString());
        }

    }
}
