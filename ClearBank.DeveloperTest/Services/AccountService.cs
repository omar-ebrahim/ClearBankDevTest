using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Utils;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IBackupAccountDataStore _backupAccountDataStore;

        public AccountService(
            IAccountDataStore accountDataStore,
            IBackupAccountDataStore backupAccountDataStore)
        {
            _accountDataStore = accountDataStore;
            _backupAccountDataStore = backupAccountDataStore;
        }

        public Account GetAccount(string dataStoreType, string accountNumber)
        {
            if (dataStoreType == Constants.BACKUP_DATA_STORE_TYPE)
            {
                return _backupAccountDataStore.GetAccount(accountNumber);
            }

            return _accountDataStore.GetAccount(accountNumber);
        }

        public void UpdateAccount(string dataStoreType, Account account)
        {
            if (dataStoreType == Constants.BACKUP_DATA_STORE_TYPE)
            {
                _backupAccountDataStore.UpdateAccount(account);
            }
            else
            {
                _accountDataStore.UpdateAccount(account);
            }
        }
    }
}
