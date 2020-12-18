using System.Collections.Generic;

namespace Badges
{
    public class Badge
    {
        public int ID { get; set; }
        public List<string> DoorNames { get; set; }

        public Badge()
        {
            DoorNames = new List<string>();
        }

        public Badge(int id, List<string> names)
        {
            ID = id;
            DoorNames = names;
        }
    }
}
