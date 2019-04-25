using System;
using Uno.Library;

namespace Uno.UI
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         UnoGameFactory.Initialize();
         var uno = UnoGameFactory.UnoGame;
         var min = UnoGame.MinNumOfPlayers;
         var max = UnoGame.MaxNumOfPlayers;

         Console.WriteLine(
            "Welcome to the card game UNO!\n\nHow many players will be playing ({0}-{1})?",
            min, max);

         int numOfPlayers;
         while ((!int.TryParse(Console.ReadLine(), out numOfPlayers)) ||
                (numOfPlayers < min || numOfPlayers > max))
         {
            Console.WriteLine("Please enter a valid number of players ({0}-{1}):", min, max);
         }

         for (int i = 0; i < numOfPlayers; i++)
         {
            Console.WriteLine("Enter player {0}'s name: ", i + 1);
            uno.AddPlayer(Console.ReadLine());
         }

         Console.WriteLine();

         Console.WriteLine("Let's get started!  Selecting a dealer now...");
         uno.SelectDealer();
         Console.WriteLine("{0} is the dealer!", uno.Dealer.Name);

         // TODO: Implement UNO gameplay.

         Console.ReadKey();
      }
   }
}
