using System;
using Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClaimRepo_Tests
{
    [TestClass]
    public class ClaimRepoTests
    {
        [TestMethod]
        public void AddClaim_ShouldGetNotNull()
        {
            ClaimRepo repo = new ClaimRepo();
            Claim claim = new Claim()
            {
                Amount = 2.50m,
                Description = "desc",
                ClaimID = 1,
                Type = Claim.ClaimType.Car,
                DateOfClaim = DateTime.Now,
                DateOfIncident = DateTime.Today,
                IsValid = true
            };
            repo.AddNewClaim(claim);
            Assert.IsNotNull(repo.GetClaims());
        }
    }
}
