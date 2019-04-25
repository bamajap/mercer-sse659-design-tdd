using Uno.Library;
using NUnit.Framework;

namespace UnoCardGameTests
{
   public class UnoTestSetupBase
   {
      protected UnoGame Uno { get; set; }

      [SetUp]
      public void Initialize()
      {
         UnoGameFactory.Initialize();
         Uno = UnoGameFactory.UnoGame;
      }

      /// <summary>
      /// Helper method that creates a certain number of players and adds them to the Uno game
      /// instance.
      /// </summary>
      /// <param name="num">The number of players to create.</param>
      protected void AddPlayers(int numOfPlayers)
      {
         for (int i = 0; i < numOfPlayers; i++)
         {
            Uno.AddPlayer(string.Format("Player_{0}", i + 1));
         }
      }

      /// <summary>
      /// Helper method that creates a certain number of players with the same name and adds
      /// them to the Uno game instance.
      /// </summary>
      /// <param name="num">The number of players to create.</param>
      protected void AddPlayersWithSameName(int numOfPlayers)
      {
         for (int i = 0; i < numOfPlayers; i++)
         {
            Uno.AddPlayer("PlayerWithSameName");
         }
      }

  }
}
