using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class WeaponList
    {
        List<Weapon> weapons;

        public WeaponList(List<Weapon> weapons)
        {
            this.weapons = weapons;
        }

        public void DisplayWeapons()
        {
            foreach(Weapon weapon in weapons)
            {
                Console.WriteLine(weapon);
            }
        }
    }
}
