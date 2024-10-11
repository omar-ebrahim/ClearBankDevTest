using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Retrieves the account from the given data store
        /// </summary>
        /// <param name="dataStoreType">The data store type to retrieve from</param>
        /// <param name="accountNumber">The account number of the account to update</param>
        /// <returns>The <see cref="Account"/> object</returns>
        Account GetAccount(string dataStoreType, string accountNumber);

        /// <summary>
        /// Updates the given account in the relevant data store
        /// </summary>
        /// <param name="dataStoreType">The data store type to update the account in</param>
        /// <param name="account">The account to update</param>
        void UpdateAccount(string dataStoreType, Account account);
    }
}
