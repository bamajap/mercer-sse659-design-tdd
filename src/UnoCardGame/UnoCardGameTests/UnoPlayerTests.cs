using NUnit.Framework;
using Uno.Library;

namespace UnoCardGameTests
{
   [TestFixture]
   class UnoPlayerTests:UnoTestSetupBase
   {
      [Test]
      public void CanCreateUnoPlayer()
      {
         var expectedName = "Test";
         var player = new Player(expectedName);
         Assert.That(player!=null);
         Assert.That(player.Name == expectedName);
      }

      [Test]
      public void MultiplePlayersWithSameNameStillDifferent()
      {
         var name = "Player_1";
         var player1 = new Player(name);
         var player2 = new Player(name);

         Assert.That(!Equals(player1, player2));
      }
   }
}
