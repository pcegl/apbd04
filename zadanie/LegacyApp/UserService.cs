using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private ICreditLimitService _creditService;
        
        [Obsolete]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditService = new UserCreditService();
        }

        public UserService(IClientRepository clientRepository, ICreditLimitService creditService)
        {
            _clientRepository = clientRepository;
            _creditService = creditService;
        }
        
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidFirstName(firstName) || !IsValidLastName(lastName) || !IsValidEmail(email) || !IsUserOldEnough(dateOfBirth))
            {
                return false;
            }

            var client = _clientRepository.GetById(clientId);

            var user = CreateUser(firstName, lastName, email, dateOfBirth, client);

            if (!IsValidCreditLimit(client, user))
            {
                return false;
            }

            SaveUser(user);

            return true;
        }
        
        private bool IsValidFirstName(string firstName)
        {
            return !string.IsNullOrEmpty(firstName);
        }

        private bool IsValidLastName(string lastName)
        {
            return !string.IsNullOrEmpty(lastName);
        }

        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && (email.Contains("@") && email.Contains("."));
        }

        private bool IsUserOldEnough(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }
            return age >= 21;
        }

        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else
            {
                int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = client.Type == "ImportantClient" ? creditLimit * 2 : creditLimit;
                user.HasCreditLimit = true;
            }

            return user;
        }

        private bool IsValidCreditLimit(Client client, User user)
        {
            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }

        private void SaveUser(User user)
        {
            UserDataAccess.AddUser(user);
        }
    }
}
