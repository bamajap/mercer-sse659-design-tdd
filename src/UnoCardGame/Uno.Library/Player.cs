namespace Uno.Library
{
   public class Player
   {
      private static int m_id;

      private int m_uniqueId = -1;

      public int UniqueId
      {
         get { return m_uniqueId; }
         private set { m_uniqueId = value; }
      }

      public string Name { get; private set; }

      public Player(string name)
      {
         UniqueId = m_id++;
         Name = name;
      }

      public override bool Equals(object obj)
      {
         if ((obj == null) || !(obj is Player)) return false;
         return Equals(obj as Player);
      }

      public override int GetHashCode()
      {
         return (Name != null ? Name.GetHashCode() : 0);
      }

      protected bool Equals(Player other)
      {
         return (string.Equals(Name, other.Name)) && (UniqueId == other.UniqueId);
      }

      public override string ToString()
      {
         return string.Format("{0}, Id: {1}", Name, UniqueId);
      }
   }
}
