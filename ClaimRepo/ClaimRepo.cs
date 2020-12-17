using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claims
{
    public class ClaimRepo
    {
        private Queue<Claim> claimQueue = new Queue<Claim>();

        //C
        public void AddNewClaim(Claim claim)
        {
            claimQueue.Enqueue(claim);
        }

        //R
        public Queue<Claim> GetClaims()
        {
            return claimQueue;
        }
    }
}
