using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claims
{
    public class ClaimRepo
    {
        private List<Claim> claimList = new List<Claim>();

        //C
        public void AddNewClaim(Claim claim)
        {
            claimList.Add(claim);
        }

        //R
        public List<Claim> GetClaims()
        {
            return claimList;
        }

        //U
        public bool UpdateExistingClaim(int id, Claim newClaim)
        {
            Claim oldClaim = FindClaimById(id);
            if(oldClaim != null)
            {
                oldClaim.ClaimID = newClaim.ClaimID;
                oldClaim.Type = newClaim.Type;
                oldClaim.Description = newClaim.Description;
                oldClaim.Amount = newClaim.Amount;
                oldClaim.DateOfIncident = newClaim.DateOfIncident;
                oldClaim.DateOfClaim = newClaim.DateOfClaim;
                oldClaim.IsValid = newClaim.IsValid;
                return true;
            }
            return false;
        }

        //D
        public bool RemoveClaim(int id)
        {
            Claim claim = FindClaimById(id);
            if(claim != null)
            {
                int initialCount = claimList.Count;
                claimList.Remove(claim);
                if(initialCount > claimList.Count)
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

        public Claim FindClaimById(int id)
        {
            foreach(Claim claim in claimList)
            {
                if(claim.ClaimID == id)
                {
                    return claim;
                }
            }
            return null;
        }
    }
}
