using FootballGameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGameRepositories
{
    public class RunPlays_Repo
    {
        private List<RunPlays> _listOfRunPlays = new List<RunPlays>();

        //Create
        public void AddRunPlayToList(RunPlays runPlay)
        {
            _listOfRunPlays.Add(runPlay);
        }

        // Read
        public List<RunPlays> ShowRunPlays()
        {
            return _listOfRunPlays;
        }


        //Update



        // Delete


        // Helper Methods
        public RunPlays GetPlayByPlayNumber(int playNumber)
        {
            
            foreach (var content in _listOfRunPlays)
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
