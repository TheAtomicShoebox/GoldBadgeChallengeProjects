using System;
using System.Collections.Generic;
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
            try
            {
                Claim temp = claims.Peek();
                if (temp != null)
                {
                    DisplayClaim(temp, true);
                    Console.WriteLine("Do you want to handle this now? (y/n)");
                    if (GetYesNoResponse("y", "n"))
                    {
                        Claim claim = claims.Dequeue();
                        HandleClaims();
                    }
                }
                else
                {
                    Console.WriteLine("No Claims to handle at this time");
                }
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine("No Claims to handle at this time");
            }
        }

        private bool AddClaims()
        {
            Console.Clear();
            Claim newClaim = new Claim();
            Console.WriteLine("Claim id: (q to quit)");
            newClaim.ClaimID = GetIntResponse("q", "Claim id: (q to quit)");
            if(newClaim.ClaimID == -1)
            {
                return false;
            }
            Console.WriteLine("Claim Type: (q to quit)");
            Console.WriteLine("Possible types\n" +
                              "Car\n" +
                              "Theft\n" +
                              "Home");
            ClaimType? temp = GetClaimTypeResponse("q", "Claim Type: (q to quit)");
            if(temp == null)
            {
                return false;
            }
            newClaim.Type = temp.Value;
            Console.WriteLine("Description: (q to quit)");
            newClaim.Description = GetResponse("q", "Description: (q to quit)");
            if(newClaim.Description == null)
            {
                return false;
            }
            Console.Write("Claim amount: (q to quit)\n$");
            newClaim.Amount = GetDecimalResponse("q", "Claim amount: (q to quit)\n$");
            if(newClaim.Amount == -1)
            {
                return false;
            }
            newClaim.Amount = Math.Round(newClaim.Amount, 2);
            Console.WriteLine("Date of Incident: (q to quit)");
            DateTime? tempDateTime = GetDateTimeResponse("q", "Date of Incident: (q to quit)");
            if(tempDateTime == null)
            {
                return false;
            }
            newClaim.DateOfIncident = tempDateTime.Value;
            Console.WriteLine("Date of Claim: (q to quit)");
            tempDateTime = GetDateTimeResponse("q", "Date of Claim: (q to quit)");
            if (tempDateTime == null)
            {
                return false;
            }
            newClaim.DateOfClaim = tempDateTime.Value;
            newClaim.IsValid = true;
            claimRepo.AddNewClaim(newClaim);
            Console.WriteLine("Claim added");
            PressToContinue();
            Console.WriteLine("Add another item? (y/n)");
            if (GetYesNoResponse("y", "n"))
            {
                return AddClaims();
            }
            return true;
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

        private void DisplayClaim(Claim claim, bool verbose = false)
        {
            if (!verbose)
            {
                Console.WriteLine($"{claim.ClaimID}\t{claim.Type}\t{claim.Description,-30}{claim.Amount}\t{claim.DateOfIncident,-15:d}{claim.DateOfClaim,-15:d}{claim.IsValid,-5}");
            }
            else
            {
                Console.WriteLine($"ClaimID: {claim.ClaimID}\n" +
                    $"Type: {claim.Type}\n" +
                    $"Description: {claim.Description}\n" +
                    $"Amount: {claim.Amount}\n" +
                    $"DateOfAccident: {claim.DateOfIncident:d}\n" +
                    $"DateOfClaim: {claim.DateOfClaim:d}\n" +
                    $"IsValid: {claim.IsValid}\n");
            }
        }

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
                ClaimType type = (ClaimType)Enum.Parse(typeof(ClaimType), response, true);
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
                return -1;
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

        private decimal GetDecimalResponse(string quitCharacter, string prompt = "")
        {
            string response = Console.ReadLine();
            if (response.ToLower() == quitCharacter.ToLower())
            {
                return -1;
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
                Console.WriteLine($"{prompt}");
                GetDecimalResponse(quitCharacter, prompt);
            }
            return -1;
        }

        private DateTime? GetDateTimeResponse(string quitCharacter, string prompt = "")
        {
            string response = Console.ReadLine();
            if(response.ToLower() == quitCharacter.ToLower())
            {
                return null;
            }
            try
            {
                DateTime dateTimeResponse = DateTime.Parse(response);
                Console.WriteLine($"Is this correct: {dateTimeResponse:d} (y/n)");
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
                Console.WriteLine($"{prompt}");
                GetDateTimeResponse(quitCharacter, prompt);
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
