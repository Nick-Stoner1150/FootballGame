using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGameClasses
{
    public class FootballField
    {
        public int YardLine { get; set; }
        // public string FieldZone { get; set; }
        public int Down { get; set; }
    

        public FootballField()
        {
                
        }

        public FootballField(int yardLine, int down)
        {
            this.YardLine = yardLine;
            // this.FieldZone = fieldZone;
            this.Down = down;
        }

    }

}
