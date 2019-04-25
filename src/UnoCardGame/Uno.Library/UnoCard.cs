using System.ComponentModel.DataAnnotations;

namespace Uno.Library
{
   public class UnoCard
   {
      public UnoCardColor Color { get; internal set; }

      [Range(-1, 9, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
      public int Rank { get; internal set; }

      public UnoCardAction Action { get; internal set; }

      public int Weight { get; private set; }

      public enum UnoCardColor
      {
         Red,
         Green,
         Blue,
         Yellow,
         Black
      }

      public enum UnoCardAction
      {
         None,
         Skip,
         Reverse,
         DrawTwo,
         Wild,
         WildDraw4
      }

      private UnoCard(){}

      private UnoCard(UnoCardColor color)
      {
         Color = color;
      }

      public UnoCard(UnoCardColor color, int rank)
         : this(color)
      {
         Rank = rank;

         if (rank > -1) Weight = rank;
      }

      public UnoCard(UnoCardColor color, UnoCardAction action)
         : this(color, -1)
      {
         Action = action;

         switch (action)
         {
            case UnoCardAction.DrawTwo:
            case UnoCardAction.Reverse:
            case UnoCardAction.Skip:
               Weight = 20;
               break;
            case UnoCardAction.Wild:
            case UnoCardAction.WildDraw4:
               Weight = 50;
               break;
         }
      }

      public override string ToString()
      {
         return (Action == UnoCardAction.None)
                   ? string.Format("{0}-{1}", Color, Rank)
                   : string.Format("{0}-{1}", Color, Action);
      }
   }
}
