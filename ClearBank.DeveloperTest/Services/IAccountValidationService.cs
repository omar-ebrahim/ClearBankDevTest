using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IAccountValidationService
    {
        /// <summary>
        /// Validates the given account and payment scheme
        /// </summary>
        /// <param name="scheme">The payment scheme to use</param>
        /// <param name="account">The account to check againt the given payment scheme</param>
        /// <param name="requestAmount">The request amount to check, if required.</param>
        /// <returns>true if the account is valid, false if it is not</returns>
        bool ValidateAccount(PaymentScheme scheme, Account account, decimal requestAmount = 0);
    }
}
