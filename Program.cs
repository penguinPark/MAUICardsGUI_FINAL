using MAUICardsGUI;
using System;

namespace RaceTo21;

class Program
{
    
    public static void StartGame(string[] args)
    { 
        MainPage mainPage = new MainPage();
        while (mainPage.nextTask != Task.Done) // changed to Task.GameOver
        {
            if (mainPage.nextTask == Task.GameOver) // if the game nextTask is GameOver it will go to the FinalTask method that I made
            {
                //FinalTask(); // created final task for LEVEL 2 TASK on the homework
            }
            mainPage.DoNextTask(); // to continue the game
        }
    }
}

