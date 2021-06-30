using FootballGame_Repos;
using FootballGameClasses;
using System;
using System.Collections.Generic;

namespace FootballGameUI
{
    public class ProgramUI
    {
        private RunPlays_Repo _runPlaysRepo = new RunPlays_Repo();
        private PassPlays_Repo _passPlaysRepo = new PassPlays_Repo();
        FootballField currentFieldPosition = new FootballField(15, 1);

        public void Run()
        {
            SeedPassPlays();
            SeedRunPlays();

            Console.WriteLine("You are down by 3 at the Patriots 15 yard line.\n" +
                                "In order to help Russel Wilson and the Seahawks the Super Bowl, you must choose the right combination" +
                                "of plays for a touchdown.\n" +
                                "Fied Goal ties the game and forces Overtime. And Tom Brady always wins in Overtime.");


            Console.WriteLine("************************");
            Console.ReadKey();
            Console.Clear();

            RunMenu();  

        }

        public void RunMenu()
        {

            bool isRunning = true;
            while (isRunning)
            {
                DisplayMenu();

                switch (Console.ReadLine())
                {
                    case "1":
                        DisplayRunPlays();
                        Console.WriteLine("Please choose a play number you would like to run...");
                        int runNumber = int.Parse(Console.ReadLine());
                        FootballField newFieldPosition = CalculateFieldPosition(RunRunPlayByRunNumber(runNumber), currentFieldPosition);
                        IsGameOver(newFieldPosition.YardLine);
                        SetNewFieldPosition(currentFieldPosition, newFieldPosition); 
                        Console.WriteLine($"It is now {newFieldPosition.Down} down on the {newFieldPosition.YardLine} yard line.");
                        break;
                    case "2":
                        DisplayPassPlays();
                        Console.WriteLine("Please choose the play number you would like to run...");
                        int passNumber = int.Parse(Console.ReadLine());
                        FootballField newFieldPositionPass = newFieldPosition = CalculateFieldPosition(RunPassPlayByPlayNumber(passNumber), currentFieldPosition);
                        IsGameOver(currentFieldPosition.YardLine);
                        SetNewFieldPosition(currentFieldPosition, newFieldPositionPass);
                        Console.WriteLine($"It is now {newFieldPositionPass.Down} down on the {newFieldPositionPass.YardLine} yard line.");
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid numer!");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine($"-----Playbook-------\n" +
                              $"1. Run\n" +
                              $"2. Pass\n" +
                              $"3. Quit Game");
        }

        public void DisplayRunPlays()
        {
            List<RunPlays> listOfPlays = _runPlaysRepo.ShowRunPlays();

            foreach (RunPlays play in listOfPlays)
            {
                Console.WriteLine($"{play.PlayNumber} {play.Name}");
            }
        }

        public void DisplayPassPlays()
        {
            Console.Clear();
            List<PassPlays> listOfPassPlays = _passPlaysRepo.ShowPassPlays();

            foreach (PassPlays play in listOfPassPlays)
            {
                Console.WriteLine($"{play.PlayNumber} {play.Name}");
            }
        }

        public void SeedRunPlays()
        {
            var runPlay1 = new RunPlays("QB Draw", -3, 12, 1);
            var runPlay2 = new RunPlays("RB Toss", -5, 7, 2);
            var runPlay3 = new RunPlays("RB Dive", -1, 5, 3);

            _runPlaysRepo.AddRunPlayToList(runPlay1);
            _runPlaysRepo.AddRunPlayToList(runPlay2);
            _runPlaysRepo.AddRunPlayToList(runPlay3);
        }

        public void SeedPassPlays()
        {
            var passPlay1 = new PassPlays("TE Post", 0, 15, 1);
            var passPlay2 = new PassPlays("WR Screen", 0, 12, 2);
            var passPlay3 = new PassPlays("WR Fade", 0, 15, 3);

            _passPlaysRepo.AddPassPlayToList(passPlay1);
            _passPlaysRepo.AddPassPlayToList(passPlay2);
            _passPlaysRepo.AddPassPlayToList(passPlay3);
        }
        
        public int RunRunPlayByRunNumber(int runNumber)
        {
            RunPlays runPlay = _runPlaysRepo.GetRunPlayByPlayNumber(runNumber);

            if (runPlay != null)
            {
                Console.WriteLine($"You ran {runPlay.Name} and you gained {runPlay.YardsGained} yards!");
                return runPlay.YardsGained;
            }
            else
            {
                Console.WriteLine("There is no run play with that number...");
                return 0;
            }
        }

        public int RunPassPlayByPlayNumber(int passPlayNumber)
        {
            PassPlays passPlay = _passPlaysRepo.GetPassPlayByPlayNumber(passPlayNumber);

            if (passPlay != null)
            {
                Console.WriteLine($"You ran {passPlay.Name} and you gained {passPlay.YardsGained} yards!");
                return passPlay.YardsGained;
            }
            else
            {
                Console.WriteLine("There is no pass play with that number...");
                return 0;
            }
        }

        public FootballField CalculateFieldPosition(int yardsGained, FootballField currentFieldPosition)
        {
            

            if (yardsGained >= currentFieldPosition.YardLine)
            {
                Console.WriteLine("Congratulations! You are going to Disney World!!!!");
                FootballField newFieldPosition = new FootballField(0, 0);
                return newFieldPosition;
            }
            else if (yardsGained < currentFieldPosition.YardLine)
            {
                currentFieldPosition.YardLine = currentFieldPosition.YardLine - yardsGained;
                if (currentFieldPosition.Down < 4)
                {
                    int newYardLine = currentFieldPosition.YardLine;
                    int newDown = currentFieldPosition.Down + 1;
                    FootballField newFieldPosition = new FootballField(newYardLine, newDown);
                    return newFieldPosition;
                }
                else
                {
                    Console.WriteLine("You turned the ball over on downs... Tom Brady wins again!!!");
                    FootballField newFieldPosition = new FootballField(0, 0);
                    return newFieldPosition;
                }
            }
            else
            {
                return null;
            }

        }

        public bool IsGameOver(int currentYardLine)
        {
            if (currentYardLine == 0)
            {
                return false;
            }
            return true;
        }

        public FootballField SetNewFieldPosition(FootballField currentFieldPosition, FootballField newFieldPosition)
        
        {
            if (newFieldPosition != null)
            {
                currentFieldPosition.Down = newFieldPosition.Down;
                currentFieldPosition.YardLine = newFieldPosition.YardLine;
                return currentFieldPosition;
            }

            return null;
        }
    }
}
