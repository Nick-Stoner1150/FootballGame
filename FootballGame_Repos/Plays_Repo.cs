using FootballGameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGame_Repos
{
    public class Plays_Repo
    {
        private List<Plays> _listOfPlays = new List<Plays>();

        //Create
        public void AddPlayToList(Plays runPlay)
        {
            _listOfPlays.Add(runPlay);
        }

        // Read
        public List<Plays> ShowPlays()
        {
            return _listOfPlays;
        }


        //Update



        // Delete


        // Helper Methods
        public Plays GetRunPlayByPlayNumber(int playNumber)
        {

            foreach (var content in _listOfPlays)
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
