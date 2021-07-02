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
        public int MinYards { get; set; }
        public int MaxYards { get; set; }
        public int YardsGained;
        private static Random rand = new Random();

        public RunPlays()
        {

        }

        public RunPlays(string name, int minYards, int maxYards, int playNumber)
        {
            Name = name;
            MinYards = minYards;
            MaxYards = maxYards;
            YardsGained = rand.Next(minYards, maxYards);
            PlayNumber = playNumber;
            
        }
        public void SetRandom(int minYards, int maxYards)
        {
            YardsGained = rand.Next(minYards, maxYards);
        }
    }

    
}
