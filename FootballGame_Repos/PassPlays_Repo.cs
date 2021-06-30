using FootballGameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGame_Repos
{
    public class PassPlays_Repo
    {
        private List<PassPlays> _listOfPassPlays = new List<PassPlays>();

        //Create 
        public void AddPassPlayToList(PassPlays passPlay)
        {
            _listOfPassPlays.Add(passPlay);
        }

        // Read
        public List<PassPlays> ShowPassPlays()
        {
            return _listOfPassPlays;
        }

        // Helper Methods
        public PassPlays GetPassPlayByPlayNumber (int playNumber)
        {
            foreach (var content in _listOfPassPlays)
            {
                if (content.PlayNumber == playNumber)
                {
                    return content;
                }
            }

            return null;
        }


    }
}
