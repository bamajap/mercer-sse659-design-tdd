using System;
using System.Linq;
using NUnit.Framework;
using Uno.Library;
using UnoCardColor = Uno.Library.UnoCard.UnoCardColor;
using UnoCardAction = Uno.Library.UnoCard.UnoCardAction;

namespace UnoCardGameTests
{
   [TestFixture]
   public class UnoTests : UnoTestSetupBase
   {
      /// <summary>
      /// Test that when multiple requests for an UNO game are made, that only a single
      /// instance of an UNO game is being created.
      /// </summary>
      [Test]
      public void CreateSingleInstanceOfUnoGame()
      {
         var numOfPlayers = 2;
         //var unoGame = new UnoGame(numOfPlayers);
         var unoGame = UnoGameFactory.UnoGame;

         Assert.NotNull(unoGame);

         for (int i = 0; i < numOfPlayers; i++)
         {
            unoGame.AddPlayer(string.Format("Player_{0}", i + 1));
         }


         numOfPlayers = 8;
         //var unoGame2 = new UnoGame(numOfPlayers);
         var unoGame2 = UnoGameFactory.UnoGame;

         Assert.NotNull(unoGame2);

         for (int i = 0; i < numOfPlayers; i++)
         {
            unoGame2.AddPlayer(string.Format("Player_{0}", i + 3));
         }

         Assert.AreEqual(unoGame, unoGame2);
         Assert.AreEqual(unoGame.Players.Count, unoGame2.Players.Count);
      }

      /// <summary>
      /// Test that an UnoGame can be successfully created.
      /// </summary>
      [Test]
      public void CreateUnoGame()
      {
         var numOfPlayers = 2;

         Assert.NotNull(Uno);

         for (int i = 0; i < numOfPlayers; i++)
         {
            Uno.AddPlayer(string.Format("Player_{0}", i + 1));
         }

         Assert.AreEqual(numOfPlayers, Uno.Players.Count);
      }

      /// <summary>
      /// Test to see if the list of Players can be properly formatted as a string.
      /// </summary>
      [Test]
      public void DisplayPlayersAsString()
      {
         AddPlayers(5);

         Uno.ListPlayers();

         var expectedFormat =
            Uno.Players.Aggregate("",
                                  (current, player) =>
                                  current + (player.Name + Environment.NewLine));

         Assert.That(Uno.ListPlayers(), Is.EqualTo(expectedFormat));
      }

      [Test]
      [ExpectedException(typeof(ApplicationException))]
      public void UnoGameThrowsExceptionWhenSelectingDealerWithNoPlayers()
      {
         Uno.SelectDealer();
      }

      /// <summary>
      /// Test to see that the UnoGame can select the dealer.
      /// </summary>
      [Test]
      public void UnoGameCanSelectDealer()
      {
         AddPlayers(3);

         Assert.That(Uno.Dealer, Is.Null);

         Uno.SelectDealer();

         Assert.That(Uno.Dealer, Is.Not.Null);
         Assert.That(Uno.Players.Contains(Uno.Dealer));
      }

      [Test]
      public void UnoGameCannotChangeDealerOnceSet()
      {
         AddPlayers(3);

         Assert.That(Uno.Dealer, Is.Null);

         Uno.SelectDealer();

         var initialDealer = Uno.Dealer;

         for (int i = 0; i < 10; i++)
         {
            Uno.SelectDealer();
            Assert.That(initialDealer.Equals(Uno.Dealer), Is.True);
         }
      }

      /// <summary>
      /// Test that the UnoGame deck of cards contains 108 cards.
      /// </summary>
      [Test]
      public void UnoGameCardDeckHas108Cards()
      {
         Assert.That(Uno.CardDeck.Count, Is.EqualTo(108));
      }

      /// <summary>
      /// Test that the UnoGame card deck has been properly setup per stated requirements from
      /// the card deck description.
      /// </summary>
      [Test]
      public void UnoGameCardDeckIsProperlySetup()
      {
         ValidateUnoGameCardDeck();
      }

      [Test]
      public void UnoGameCardDeckIsProperlySetupAfterShuffle()
      {
         Uno.ShuffleDeck();
         ValidateUnoGameCardDeck();
      }

      [Test]
      public void UnoCardDeckCanBeShuffled()
      {
         var beforeDeck = Uno.CardDeck;

         Uno.ShuffleDeck();

         Assert.That(beforeDeck, Is.EquivalentTo(Uno.CardDeck));
         Assert.That(beforeDeck, Is.Not.EqualTo(Uno.CardDeck));
      }

      [Test]
      public void UnoGameCardDeckHas108CardsAfterShuffle()
      {
         Uno.ShuffleDeck();
         Assert.That(Uno.CardDeck.Count, Is.EqualTo(108));
      }

      #region Helper Methods

      /// <summary>
      /// This method provides a set of procedures for validating the card deck.
      /// </summary>
      private void ValidateUnoGameCardDeck()
      {
         CheckUnoCardColor(UnoCardColor.Red);
         CheckUnoCardColor(UnoCardColor.Green);
         CheckUnoCardColor(UnoCardColor.Blue);
         CheckUnoCardColor(UnoCardColor.Yellow);

         var wildCardsCount = Uno.CardDeck.Count(c => c.Color == UnoCardColor.Black);
         Assert.That(wildCardsCount, Is.EqualTo(8));

         // Check that there are exactly 4 of 'Wild' and 'WildDraw4' cards.
         for (var i = 4; i <= 5; i++)
         {
            var coloredWildActionCardsByTwosCount =
               Uno.CardDeck.Count(c => (int)c.Action == i);
            Assert.That(coloredWildActionCardsByTwosCount, Is.EqualTo(4),
                        "There are {0} {1}-{2} cards. Was expecting 4 cards.",
                        coloredWildActionCardsByTwosCount, UnoCardColor.Black,
                        ((UnoCardAction)i));
         }
      }

      /// <summary>
      /// Helper method to help verify the UnoGame card deck based on requirements.
      /// </summary>
      /// <param name="unoCardColor">The UNO color "suit" to check.</param>
      private void CheckUnoCardColor(UnoCardColor unoCardColor)
      {
         // Check for exactly 25 colored cards.
         var colorCards = Uno.CardDeck.Where(c => c.Color == unoCardColor);
         Assert.That(colorCards.Count(), Is.EqualTo(25));

         // Check for exactly 19 numbered colored cards.
         var numberedColorCards = colorCards.Where(c => c.Rank >= 0);
         Assert.That(numberedColorCards.Count(), Is.EqualTo(19));

         // Check that there are exactly 2 of the '1' through '9' cards.
         for (var i = 1; i < 10; i++)
         {
            var coloredFaceCardsByTwos = numberedColorCards.Where(c => c.Rank == i);
            Assert.That(coloredFaceCardsByTwos.Count(), Is.EqualTo(2),
                        "There are {0} {1}-{2} cards. Was expecting 2 cards.",
                        coloredFaceCardsByTwos.Count(), unoCardColor, i);
         }

         // Check that there is only a single colored zero card.
         var coloredZeroCardCount = numberedColorCards.Count(c => c.Rank == 0);
         Assert.That(coloredZeroCardCount, Is.EqualTo(1));

         // Check that there are exactly 6 action cards.
         var coloredActionCards = colorCards.Where(c => c.Action != UnoCardAction.None);
         Assert.That(coloredActionCards.Count(), Is.EqualTo(6));

         // Check that there are exactly 2 of 'Skip', 'Reverse', and 'DrawTwo' cards.
         for (var i = 1; i <= 3; i++)
         {
            var coloredActionCardsByTwos = coloredActionCards.Where(c => (int) c.Action == i);
            Assert.That(coloredActionCardsByTwos.Count(), Is.EqualTo(2),
                        "There are {0} {1}-{2} cards. Was expecting 2 cards.",
                        coloredActionCardsByTwos.Count(), unoCardColor, ((UnoCardAction) i));
         }
      }

      #endregion // Helper Methods
   }
}
