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

        public WeaponList(List<Weapon> weapons)
        {
            this.weapons = weapons;
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
