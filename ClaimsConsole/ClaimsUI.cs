using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Claims.Claim;

namespace Claims
{
    class ClaimsUI
    {
        private ClaimRepo claimRepo = new ClaimRepo();

        public void Run()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Select an Option:\n" +
                                  "1.\tDisplay Claims\n" +
                                  "2.\tCreate New Claim\n" +
                                  "3.\tHandle Claims\n" +
                                  "4.\tExit");
                char response = Console.ReadKey().KeyChar;
                switch (response)
                {
                    case '1':
                        DisplayClaims();
                        PressToContinue();
                        break;
                    case '2':
                        AddClaims();
                        break;
                    case '3':
                        HandleClaims();
                        break;
                    case '4':
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Response");
                        PressToContinue();
                        break;
                }
            }
        }

        private void HandleClaims()
        {
            Console.Clear();
            Queue<Claim> claims = claimRepo.GetClaims();
            DisplayClaim(claims.Peek());
            Console.WriteLine("Do you want to handle this now? (y/n)");
            if (GetYesNoResponse("y", "n"))
            {
                Claim claim = claims.Dequeue();
            }
        }

        private void AddClaims()
        {
            Console.Clear();
            Claim newClaim = new Claim();
            Console.WriteLine("Enter the claim id: (q to quit)");
            newClaim.ClaimID = GetIntResponse("q", "Enter the claim id: (q to quit)");
        }

        private void DisplayClaims()
        {
            Console.WriteLine();
            Queue<Claim> claims = claimRepo.GetClaims();
            foreach (Claim claim in claims)
            {
                DisplayClaim(claim);
            }
        }

        private void DisplayClaim(Claim claim)
        {
            Console.WriteLine($"{claim.ClaimID}\t{claim.Type}\t{claim.Description, -30}{claim.Amount}\t{claim.DateOfIncident, -10}{claim.DateOfClaim, -10}{claim.IsValid}");
        }

        /*private Claim UserSelectClaim()
        {
            DisplayClaims();
            Console.WriteLine("Claim id (q to quit):");
            int response = GetIntResponse("q");
            if (response != -1)
            {
                Claim claim = claimRepo.FindClaimById(response);
                if (claim != null)
                {
                    return claim;
                }
                Console.WriteLine("Invalid Response");
                PressToContinue();
                return UserSelectClaim();
            }
            return null;
        }*/

        private string GetResponse(string quitCharacter, string prompt = "")
        {
            string response = Console.ReadLine();
            if (response.ToLower() == quitCharacter.ToLower())
            {
                return null;
            }
            return response;
        }

        private bool GetYesNoResponse(string affirmative, string negative)
        {
            string response = Console.ReadLine().ToLower();
            if (response == affirmative.ToLower())
            {
                return true;
            }
            if (response == negative.ToLower())
            {
                return false;
            }
            return false;
        }

        private ClaimType? GetClaimTypeResponse(string quitCharacter, string prompt = "")
        {
            string response = Console.ReadLine();
            if(response.ToLower() == quitCharacter.ToLower())
            {
                return null;
            }
            try
            {
                ClaimType type = (ClaimType)Enum.Parse(typeof(ClaimType), response);
                return type;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid Response");
                PressToContinue();
                Console.WriteLine($"{prompt}");
                return GetClaimTypeResponse(quitCharacter, prompt);
            }
        }

        private int GetIntResponse(string quitCharacter, string prompt = "")
        {
            string response = Console.ReadLine();
            if (response.ToLower() == quitCharacter.ToLower())
            {
                return 0;
            }
            try
            {
                int intResponse = int.Parse(response);
                return intResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Response");
                PressToContinue();
                Console.WriteLine($"{prompt}");
                return GetIntResponse(quitCharacter, prompt);
            }
        }

        private decimal GetDecimalResponse(string quitCharacter)
        {
            string response = Console.ReadLine();
            if (response.ToLower() == quitCharacter.ToLower())
            {
                return 0;
            }
            try
            {
                decimal decimalResponse = decimal.Parse(response);
                return decimalResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Response");
                PressToContinue();
                GetDecimalResponse(quitCharacter);
            }
            return -1;
        }

        private DateTime? GetDateTimeResponse(string quitCharacter)
        {
            string response = Console.ReadLine();
            if(response.ToLower() == quitCharacter.ToLower())
            {
                return null;
            }
            try
            {
                DateTime dateTimeResponse = DateTime.Parse(response);
                Console.WriteLine($"Is this correct: {dateTimeResponse} (y/n)");
                if(!GetYesNoResponse("y", "n"))
                {
                    GetDateTimeResponse(quitCharacter);
                }
                return dateTimeResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Response");
                PressToContinue();
                GetDateTimeResponse(quitCharacter);
            }
            return null;
        }

        private void PressToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
