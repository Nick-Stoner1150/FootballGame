using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGameClasses
{
    public class Plays
    {
        public string Name { get; }
        public int PlayNumber { get; }
        public int MinYards { get; set; }
        public int MaxYards { get; set; }
        public int YardsGained;
        private static Random rand = new Random();
        public enum Type
        { Run = 1, 
          Pass
        };
        public Type PlayType;

        public Plays()
        {

        }

        public Plays(string name, int minYards, int maxYards, int playNumber, Type playType)
        {
            Name = name;
            MinYards = minYards;
            MaxYards = maxYards;
            YardsGained = rand.Next(minYards, maxYards);
            PlayNumber = playNumber;
            PlayType = playType;
            
        }
        public void SetRandom(int minYards, int maxYards)
        {
            YardsGained = rand.Next(minYards, maxYards);
        }

    }


    
}
