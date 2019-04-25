using System;
using System.Collections.Generic;
using System.Linq;

namespace Uno.Library
{
   public sealed class UnoGame
   {
      public const int MinNumOfPlayers = 2;
      public const int MaxNumOfPlayers = 10;

      private List<Player> m_players;
      private List<UnoCard> m_deck;

      private UnoGame(){}

      internal UnoGame(List<UnoCard> deck)
      {
         m_players = new List<Player>();
         m_deck = deck;
      }

      public IList<UnoCard> CardDeck
      {
         get { return m_deck.AsReadOnly(); }
      }

      public IList<Player> Players
      {
         get { return m_players.AsReadOnly(); }
      }

      public Player Dealer { get; private set; }

      public void AddPlayer(string name)
      {
         m_players.Add(new Player(name));
      }

      public string ListPlayers()
      {
         return Players.Aggregate("",
                                  (current, player) =>
                                  current + (player.Name + Environment.NewLine));
      }

      public void SelectDealer()
      {
         if (m_players == null || !m_players.Any())
            throw new ApplicationException(
               "There are no players in the game yet! Please add some players before "+
               "attempting to select the dealer.");
         
         // If the dealer has already been set, then do not allow the dealer to be reset.
         if (Dealer != null) return;
         
         ShuffleDeck();

         var faceCards = m_deck.Where(ac => ac.Action == UnoCard.UnoCardAction.None);

         IDictionary<Player, UnoCard> players = new Dictionary<Player, UnoCard>();
         for (int i = 0; i < m_players.Count; i++)
         {
            var p = m_players[i];
            var c = faceCards.ElementAt(i);
            players.Add(p, c);
         }

         var playerRanks =
            players.OrderBy(kvp => kvp.Value.Rank)
                   .ThenBy(kvp => kvp.Value.Color)
                   .ThenBy(kvp => kvp.Key.UniqueId);

         m_players = playerRanks.Select(kvp => kvp.Key).ToList();

         Dealer = m_players.Last();
      }

      public void ShuffleDeck()
      {
         var tempDeck = new List<UnoCard>(m_deck.Count);

         var random = new Random();

         for (int i = 0; i < m_deck.Count; i++)
         {
            do
            {
               var card = m_deck[random.Next(m_deck.Count)];
               if (tempDeck.Contains(card)) continue;
               tempDeck.Add(card);
               break;
            } while (true);
         }

         m_deck = tempDeck;
      }
   }
}
