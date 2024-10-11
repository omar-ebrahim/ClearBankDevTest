using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data
{
    public interface IDataStore
    {
        /// <summary>
        /// Retrieves the <see cref="Account"/> given the account number
        /// </summary>
        /// <param name="accountNumber">The account number to search for</param>
        /// <returns>The relevant <see cref="Account"/> object.</returns>
        Account GetAccount(string accountNumber);

        /// <summary>
        /// Updates the given account
        /// </summary>
        /// <param name="account">The account to update</param>
        void UpdateAccount(Account account);
    }
}
