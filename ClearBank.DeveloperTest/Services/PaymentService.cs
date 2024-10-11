using ClearBank.DeveloperTest.Configuration;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IDataStoreTypeOptions _datastoreTypeOptions;
        private readonly IAccountValidationService _accountValidationService;

        public PaymentService(
            IDataStoreTypeOptions dataStoreTypeOptions,
            IAccountService accountService,
            IAccountValidationService accountValidationService)
        {
            _accountService = accountService;
            _datastoreTypeOptions = dataStoreTypeOptions;
            _accountValidationService = accountValidationService;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = _accountService.GetAccount(_datastoreTypeOptions.DataStoreType, request.DebtorAccountNumber);

            var result = new MakePaymentResult
            {
                Success = _accountValidationService.ValidateAccount(request.PaymentScheme, account, request.Amount)
            };

            if (result.Success)
            {
                account.Balance -= request.Amount;
                _accountService.UpdateAccount(_datastoreTypeOptions.DataStoreType, account);
            }

            return result;
        }
    }
}
