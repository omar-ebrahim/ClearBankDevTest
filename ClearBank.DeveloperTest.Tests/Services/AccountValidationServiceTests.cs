using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class AccountValidationServiceTests
    {
        [Test]
        public void ValidateAccount_PaymentSchemeIsBacsAndAccountIsNull_ReturnsFalse()
        {
            // Arrange
            var scheme = PaymentScheme.Bacs;
            Account account = null;

            var service = new AccountValidationService();

            // Act
            var result = service.ValidateAccount(scheme, account);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateAccount_PaymentSchemeIsBacsAndAccountDoesNotHaveBacsAsAllowedPaymentScheme_ReturnsFalse()
        {
            // Arrange
            var scheme = PaymentScheme.Bacs;
            Account account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Chaps };

            var service = new AccountValidationService();

            // Act
            var result = service.ValidateAccount(scheme, account);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateAccount_PaymentSchemeIsBacsAndAccountIsValid_ReturnsTrue()
        {
            // Arrange
            var scheme = PaymentScheme.Bacs;
            Account account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs };

            var service = new AccountValidationService();

            // Act
            var result = service.ValidateAccount(scheme, account);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ValidateAccount_PaymentSchemeIsFasterPaymentsAndAccountIsNull_ReturnsFalse()
        {
            // Arrange
            var scheme = PaymentScheme.FasterPayments;
            Account account = null;

            var service = new AccountValidationService();

            // Act
            var result = service.ValidateAccount(scheme, account);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateAccount_PaymentSchemeIsFasterPaymentsAndAccountDoesNotHaveFasterPaymentsAsAllowedPaymentScheme_ReturnsFalse()
        {
            // Arrange
            var scheme = PaymentScheme.FasterPayments;
            Account account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps };

            var service = new AccountValidationService();

            // Act
            var result = service.ValidateAccount(scheme, account);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateAccount_PaymentSchemeIsFasterPaymentsAndAccountBalanceLessThanRequestAmount_ReturnsFalse()
        {
            // Arrange
            var scheme = PaymentScheme.FasterPayments;
            var balance = 1;
            var requestAmount = 2;
            Account account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = balance,
            };

            var service = new AccountValidationService();

            // Act
            var result = service.ValidateAccount(scheme, account, requestAmount);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateAccount_PaymentSchemeIsFasterPaymentsAndAccountValid_ReturnsTrue()
        {
            // Arrange
            var scheme = PaymentScheme.FasterPayments;
            var balance = 2;
            var requestAmount = 1;
            Account account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = balance,
            };

            var service = new AccountValidationService();

            // Act
            var result = service.ValidateAccount(scheme, account, requestAmount);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ValidateAccount_PaymentSchemeIsChapsAndAccountIsNull_ReturnsFalse()
        {
            // Arrange
            var scheme = PaymentScheme.Chaps;
            Account account = null;

            var service = new AccountValidationService();

            // Act
            var result = service.ValidateAccount(scheme, account);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateAccount_PaymentSchemeIsChapsAndAccountDoesNotHaveChapsAsAllowedPaymentScheme_ReturnsFalse()
        {
            // Arrange
            var scheme = PaymentScheme.Chaps;
            Account account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments };

            var service = new AccountValidationService();

            // Act
            var result = service.ValidateAccount(scheme, account);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateAccount_PaymentSchemeIsChapsAccountIsNotLive_ReturnsFalse()
        {
            // Arrange
            var scheme = PaymentScheme.Chaps;
            Account account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Status = AccountStatus.Disabled };

            var service = new AccountValidationService();

            // Act
            var result = service.ValidateAccount(scheme, account);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
