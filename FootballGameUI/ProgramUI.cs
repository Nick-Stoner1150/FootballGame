﻿using FootballGame_Repos;
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
                        Console.WriteLine("Please select the play number you would like to run...");
                        int runNumber = int.Parse(Console.ReadLine());
                        FootballField newFieldPosition = CalculateFieldPosition(RunRunPlayByRunNumber(runNumber), currentFieldPosition);
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
                        Console.Clear();
                        DisplayPassPlays();
                        Console.WriteLine("Please select the play number you would like to run...");
                        int passNumber = int.Parse(Console.ReadLine());
                        FootballField newFieldPositionPass = CalculateFieldPosition(RunPassPlayByPlayNumber(passNumber), currentFieldPosition);
                        bool isGameOver2 = IsGameOver(newFieldPositionPass.YardLine);
                        if (!isGameOver2)
                        {
                            ResetFieldPosition(currentFieldPosition);
                        }
                        else
                        {
                            SetNewFieldPosition(currentFieldPosition, newFieldPositionPass);
                            Console.WriteLine($"It is now {newFieldPositionPass.Down} down on the {newFieldPositionPass.YardLine} yard line.");
                        }
                        break;
                    case "3":
                        ResetFieldPosition(currentFieldPosition);
                        break;
                    case "4":
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
                              $"1. Run Plays\n" +
                              $"2. Pass Plays\n" +
                              $"3. Reset or Start Over\n" +
                              $"4. Quit Game\n");
        }

        public void DisplayRunPlays()
        {
            Console.Clear();
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
            var runPlay1 = new RunPlays("Quarterback Draw", -3, 12, 1);
            var runPlay2 = new RunPlays("Running Back Toss", -5, 7, 2);
            var runPlay3 = new RunPlays("Running Back Dive", -1, 5, 3);

            _runPlaysRepo.AddRunPlayToList(runPlay1);
            _runPlaysRepo.AddRunPlayToList(runPlay2);
            _runPlaysRepo.AddRunPlayToList(runPlay3);
        }

        public void SeedPassPlays()
        {
            var passPlay1 = new PassPlays("Tight End Post", 0, 15, 1);
            var passPlay2 = new PassPlays("Wide Receiver Screen", 0, 15, 2);
            var passPlay3 = new PassPlays("Wide Receiver Fade", 0, 10, 3);

            _passPlaysRepo.AddPassPlayToList(passPlay1);
            _passPlaysRepo.AddPassPlayToList(passPlay2);
            _passPlaysRepo.AddPassPlayToList(passPlay3);
        }
        
        public int RunRunPlayByRunNumber(int runNumber)
        
        {
            Console.Clear();
            RunPlays runPlay = _runPlaysRepo.GetRunPlayByPlayNumber(runNumber);
            runPlay.SetRandom(runPlay.MinYards, runPlay.MaxYards);
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
            Console.Clear();
            
            PassPlays passPlay = _passPlaysRepo.GetPassPlayByPlayNumber(passPlayNumber);
            passPlay.SetRandom(passPlay.MinYards, passPlay.MaxYards);
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
