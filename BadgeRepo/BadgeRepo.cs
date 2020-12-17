using System.Collections.Generic;

namespace Badges
{
    public class BadgeRepo
    {
        private Dictionary<int, List<string>> badgeDictionary = new Dictionary<int, List<string>>();

        //C
        public void AddNewBadge(Badge badge)
        {
            badgeDictionary.Add(badge.ID, badge.DoorNames);
        }

        //R
        public Dictionary<int, List<string>> GetBadges()
        {
            return badgeDictionary;
        }

        //U
        public bool UpdateExistingBadge(int id, Badge badge)
        {
            Badge oldBadge = FindBadgeById(id);
            if(oldBadge != null)
            {
                oldBadge.ID = badge.ID;
                oldBadge.DoorNames = badge.DoorNames;
                return true;
            }
            return false;
        }

        //D
        public bool RemoveBadge(int id)
        {
            Badge badge = FindBadgeById(id);
            if(badge != null)
            {
                int initialCount = badgeDictionary.Count;
                badgeDictionary.Remove(badge.ID);
                if(initialCount > badgeDictionary.Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public Badge FindBadgeById(int id)
        {
            foreach(KeyValuePair<int, List<string>> badge in badgeDictionary)
            {
                if(badge.Key == id)
                {
                    return new Badge()
                    {
                        DoorNames = badge.Value,
                        ID = badge.Key
                    };
                }
            }
            return null;
        }
    }
}
