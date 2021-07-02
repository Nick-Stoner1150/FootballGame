using FootballGame_Repos;
using FootballGameClasses;
using System;
using System.Collections.Generic;

namespace FootballGameUI
{
    public class ProgramUI
    {
        private Plays_Repo _playsRepo = new Plays_Repo();
        FootballField currentFieldPosition = new FootballField(15, 1);

        public void Run()
        {
            SeedPlays();

            Console.WriteLine(" RedZone: 1st Down.  30 Seconds on the Clock.\n" +
                                " You are down by 3 at the Patriots 15 yard line.\n" +
                                " In order to help Russell Wilson and the Seahawks win the Super Bowl, you\n" +
                                " must choose the right combination of plays for a touchdown.\n" +
                                " \n" +
                                " Field Goal ties the game and forces Overtime. Don't Forget: Tom Brady ALWAYS wins in Overtime!");


            Console.WriteLine("************************");
            Console.WriteLine("Press any key to continue...");
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
                        Console.WriteLine();
                        DisplayPassPlays();
                        Console.WriteLine("Please select the play number you would like to run...");
                        int runNumber = int.Parse(Console.ReadLine());
                        FootballField newFieldPosition = CalculateFieldPosition(RunPlayByPlayNumber(runNumber), currentFieldPosition);
                        bool isGameOver = IsGameOver(newFieldPosition.YardLine);
                        if (!isGameOver)
                        {
                            ResetFieldPosition(currentFieldPosition);
                        }
                        else
                        {
                            SetNewFieldPosition(currentFieldPosition, newFieldPosition);
                            Console.WriteLine($"It is now {newFieldPosition.Down} down on the {newFieldPosition.YardLine} yard line.");
                        }
                        break;
                    case "2":
                        ResetFieldPosition(currentFieldPosition);
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
            Console.WriteLine($"-----Beat Tom Brady-------\n" +
                              $"1. PlayBook\n" +
                              $"2. Reset or Start Over\n" +
                              $"3. Quit Game\n");
        }

        public void DisplayRunPlays()
        {
            Console.Clear();
            Console.WriteLine("------Run Plays------");
            List<Plays> listOfPlays = _playsRepo.ShowPlays();

            foreach (Plays play in listOfPlays)
            {
                if (play.PlayType == Plays.Type.Run)
                {
                    Console.WriteLine($"{play.PlayNumber}. - {play.Name}");
                }
            }
        }

        public void DisplayPassPlays()
        {
            Console.WriteLine("------Pass Plays------");
            List<Plays> listOfPlays = _playsRepo.ShowPlays();

            foreach (Plays play in listOfPlays)
            {
                if (play.PlayType == Plays.Type.Pass )
                {
                    Console.WriteLine($"{play.PlayNumber}. - {play.Name}");
                }
                
            }
        }

        public void SeedPlays()
        {
            var runPlay1 = new Plays("Quarterback Draw", -3, 12, 1, Plays.Type.Run);
            var runPlay2 = new Plays("Running Back Toss", -5, 7, 2, Plays.Type.Run);
            var runPlay3 = new Plays("Running Back Dive", -1, 5, 3, Plays.Type.Run);

            _playsRepo.AddPlayToList(runPlay1);
            _playsRepo.AddPlayToList(runPlay2);
            _playsRepo.AddPlayToList(runPlay3);
      
            Plays passPlay1 = new Plays("Tight End Post", 0, 15, 4, Plays.Type.Pass);
            var passPlay2 = new Plays("Wide Receiver Screen", 0, 15, 5, Plays.Type.Pass);
            var passPlay3 = new Plays("Wide Receiver Fade", 0, 10, 6, Plays.Type.Pass);
            
            _playsRepo.AddPlayToList(passPlay1);
            _playsRepo.AddPlayToList(passPlay2);
            _playsRepo.AddPlayToList(passPlay3);

        }
        
        public int RunPlayByPlayNumber(int runNumber)
        
        {
            Console.Clear();
            Plays runPlay = _playsRepo.GetRunPlayByPlayNumber(runNumber);
            runPlay.SetRandom(runPlay.MinYards, runPlay.MaxYards);
            if (runPlay != null)
            {
                if (runPlay.YardsGained < 0)
                {
                    Console.WriteLine($"You ran {runPlay.Name} and you lossed {Math.Abs(runPlay.YardsGained)} yards!");
                    return runPlay.YardsGained;
                }
                else
                {
                    Console.WriteLine($"You ran {runPlay.Name} and you gained {runPlay.YardsGained} yards!");
                    return runPlay.YardsGained;
                }
            }
            else
            {
                Console.WriteLine("There is no run play with that number...");
                return 0;
            }
        }

        public FootballField CalculateFieldPosition(int yardsGained, FootballField currentFieldPosition)
        {
            

            if (yardsGained >= currentFieldPosition.YardLine)
            {
                Console.WriteLine("TOUCHDOWN!!! You beat Tom Brady!!! CONGRATULATIONS! You're going to DISNEY WORLD!!!!");
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

        public FootballField ResetFieldPosition(FootballField currentFieldPosition)
        {
            if (currentFieldPosition != null)
            {
                currentFieldPosition.Down = 1;
                currentFieldPosition.YardLine = 15;
                Console.WriteLine("The game is over and has been reset");
                return currentFieldPosition;
            }

            return null;
        }
    }
}
