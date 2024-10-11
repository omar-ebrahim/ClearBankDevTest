using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountValidationService : IAccountValidationService
    {
        public bool ValidateAccount(PaymentScheme scheme, Account account, decimal requestAmount = 0)
        {
            if (account == null)
            {
                return false;
            }

            if (scheme == PaymentScheme.Bacs && !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            {
                return false;
            }

            if (scheme == PaymentScheme.FasterPayments && !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                return false;
            }
            else if (scheme == PaymentScheme.FasterPayments && account.Balance < requestAmount)
            {
                return false;
            }

            if (scheme == PaymentScheme.Chaps && !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                return false;
            }
            else if (account.Status != AccountStatus.Live)
            {
                return false;
            }

            return true;
        }
    }
}
