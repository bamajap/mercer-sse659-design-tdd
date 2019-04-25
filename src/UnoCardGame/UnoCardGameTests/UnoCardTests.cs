using NUnit.Framework;
using Uno.Library;
using UnoCardColor = Uno.Library.UnoCard.UnoCardColor;
using UnoCardAction = Uno.Library.UnoCard.UnoCardAction;

namespace UnoCardGameTests
{
   [TestFixture]
   public class UnoCardTests : UnoTestSetupBase
   {
      [Test]
      public void UnoCardCanBeCreated()
      {
         var unoCard = new UnoCard(UnoCardColor.Red, 0);
         Assert.That(unoCard, Is.Not.Null);
         Assert.That(unoCard.Action,Is.EqualTo(UnoCardAction.None));
         Assert.That(unoCard.Rank,Is.EqualTo(0));
         Assert.That(unoCard.Color,Is.EqualTo(UnoCardColor.Red));
         Assert.That(unoCard.Weight, Is.EqualTo(0));

         var actionUnoCard = new UnoCard(UnoCardColor.Black, UnoCardAction.WildDraw4);
         Assert.That(actionUnoCard, Is.Not.Null);
         Assert.That(actionUnoCard.Action, Is.EqualTo(UnoCardAction.WildDraw4));
         Assert.That(actionUnoCard.Rank, Is.EqualTo(-1));
         Assert.That(actionUnoCard.Color, Is.EqualTo(UnoCardColor.Black));
         Assert.That(actionUnoCard.Weight, Is.EqualTo(50));
      }

      [TestCase(UnoCardColor.Red, 0)]
      [TestCase(UnoCardColor.Green, 1)]
      [TestCase(UnoCardColor.Blue, 2)]
      [TestCase(UnoCardColor.Yellow, 3)]
      [TestCase(UnoCardColor.Red, 4)]
      [TestCase(UnoCardColor.Green, 5)]
      [TestCase(UnoCardColor.Blue, 6)]
      [TestCase(UnoCardColor.Yellow, 7)]
      [TestCase(UnoCardColor.Red, 8)]
      [TestCase(UnoCardColor.Green, 9)]
      public void TestUnoFaceCardWeights(UnoCardColor color, int rank)
      {
         var card = new UnoCard(color, rank);
         Assert.That(card.Weight, Is.EqualTo(rank));
      }

      [TestCase(UnoCardColor.Red, UnoCardAction.DrawTwo, 20)]
      [TestCase(UnoCardColor.Green, UnoCardAction.DrawTwo, 20)]
      [TestCase(UnoCardColor.Blue, UnoCardAction.Reverse, 20)]
      [TestCase(UnoCardColor.Yellow, UnoCardAction.Reverse, 20)]
      [TestCase(UnoCardColor.Red, UnoCardAction.Skip, 20)]
      [TestCase(UnoCardColor.Green, UnoCardAction.Skip, 20)]
      [TestCase(UnoCardColor.Black, UnoCardAction.Wild, 50)]
      [TestCase(UnoCardColor.Black, UnoCardAction.Wild, 50)]
      [TestCase(UnoCardColor.Black, UnoCardAction.WildDraw4, 50)]
      [TestCase(UnoCardColor.Black, UnoCardAction.WildDraw4, 50)]
      public void TestUnoActionCardWeights(UnoCardColor color, UnoCardAction action,
                                           int expected)
      {
         var card = new UnoCard(color, action);
         Assert.That(card.Weight, Is.EqualTo(expected));
      }
   }
}
