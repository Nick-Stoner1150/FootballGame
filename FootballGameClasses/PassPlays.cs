using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGameClasses
{
    public class PassPlays
    {
        public string Name { get; }
        public int PlayNumber { get; }
        public int MinYards { get;  }
        public int MaxYards { get;  }

        public int YardsGained;
        public readonly Random rand = new Random();

        public PassPlays()
        {

        }

        public PassPlays(string name, int minYards, int maxYards, int playNumber)
        {
            this.Name = name;
            MinYards = minYards;
            MaxYards = maxYards;
            this.YardsGained = rand.Next(minYards, maxYards);
            this.PlayNumber = playNumber;
        }

        public void SetRandom(int minYards, int maxYards)
        {
            YardsGained = rand.Next(minYards, maxYards);
        }
    }
}
