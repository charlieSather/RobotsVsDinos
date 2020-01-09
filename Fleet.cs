using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne
{
    class Fleet
    {
        public List<Robot> robots;

        public Fleet(List<Robot> robots)
        {
            this.robots = robots;
        }

        public bool HasSurvivors()
        {
            bool alive = false;
            foreach (Robot robo in robots)
            {
                if (robo.Health() > 0)
                {
                    alive = true;
                    break;
                }
            }
            return alive;
        }



    }
}
