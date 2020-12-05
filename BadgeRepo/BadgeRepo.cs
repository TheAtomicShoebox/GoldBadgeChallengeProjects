using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badges
{
    public class BadgeRepo
    {
        private List<Badge> badgeList = new List<Badge>();

        //C
        public void AddNewBadge(Badge badge)
        {
            badgeList.Add(badge);
        }

        //R
        public List<Badge> GetBadges()
        {
            return badgeList;
        }

        //U
        public bool UpdateExistingBadge(int id, Badge badge)
        {
            Badge oldBadge = FindBadgeById(id);
            if(oldBadge != null)
            {
                oldBadge.ID = badge.ID;
                oldBadge.Name = badge.Name;
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
                int initialCount = badgeList.Count;
                badgeList.Remove(badge);
                if(initialCount > badgeList.Count)
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
            foreach(Badge badge in badgeList)
            {
                if(badge.ID == id)
                {
                    return badge;
                }
            }
            return null;
        }
    }
}
