using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGameClasses
{
    public class RunPlays
    {
        public string Name { get; }
        public int PlayNumber { get; }
        public int YardsGained;
        private readonly Random rand = new Random();

        public RunPlays()
        {

        }

        public RunPlays(string name, int minYards, int maxYards, int playNumber)
        {
            Name = name;
            YardsGained = rand.Next(minYards, maxYards);
            PlayNumber = playNumber;
            
        }




    }
}
