using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class WeaponList
    {
        public List<Weapon> weapons { get; private set; }

        public WeaponList()
        {
            weapons = new List<Weapon>
            {
                new Weapon("Pistol", 65, 0.75),
                new Weapon("Crossbow", 50, 0.6),
                new Weapon("M4A4", 100, 1.25),
                new Weapon("Slingshot", 25, 0.25),
                new Weapon("Sniper Rifle", 150, 1.75),
                new Weapon("Rocket Launcher", 200, 2.5),
                new Weapon("Rifle", 85, 1),
                new Weapon("Shotgun", 175, 2)
            };
        }
                
        public void DisplayWeapons()
        {
            for(int i = 0; i < weapons.Count; i++ )
            {
                Console.WriteLine($"{i}: {weapons[i].ToString()}");
            }
        }
    }
}
