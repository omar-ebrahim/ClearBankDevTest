using ClearBank.DeveloperTest.Configuration;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        [Test]
        public void MakePayment_AccountIsValid_UpdatesAccountBalanceAndReturnsTrue()
        {
            // Arrange
            var dataStoreType = "Backup";
            var accountBalance = 200;
            var requestAmount = 100;
            var account = new Account() { AccountNumber = "testaccount", Balance = accountBalance };
            var request = new MakePaymentRequest()
            {
                CreditorAccountNumber = "abcd",
                PaymentScheme = PaymentScheme.Chaps,
                Amount = requestAmount,
                DebtorAccountNumber = "1234"
            };

            var mockOptions = new Mock<IDataStoreTypeOptions>();
            mockOptions.SetupGet(x => x.DataStoreType).Returns(dataStoreType);

            var mockAccountService = new Mock<IAccountService>();
            mockAccountService.Setup(x => x.GetAccount(dataStoreType, request.DebtorAccountNumber)).Returns(account);

            var mockAccountValidationService = new Mock<IAccountValidationService>();
            mockAccountValidationService
                .Setup(x => x.ValidateAccount(request.PaymentScheme, account, 100))
                .Returns(true);

            var service = new PaymentService(mockOptions.Object, mockAccountService.Object, mockAccountValidationService.Object);
           

            // Act
            var result = service.MakePayment(request);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Success, Is.True);
            mockAccountService.Verify(x => x.GetAccount(dataStoreType, "1234"), Times.Once());
            mockAccountService.Verify(x => x.UpdateAccount(dataStoreType, It.Is<Account>(
                account => account.AccountNumber == account.AccountNumber && account.Balance == 100
                )), Times.Once());
        }

        [Test]
        public void MakePayment_AccountIsNotValid_DoesNotUpdateAccountBalanceAndReturnsFalse()
        {
            // Arrange
            var dataStoreType = "Backup";
            var account = new Account();
            var request = new MakePaymentRequest()
            {
                DebtorAccountNumber = "1234"
            };

            var mockAccountValidationService = new Mock<IAccountValidationService>();
            mockAccountValidationService
                .Setup(x => x.ValidateAccount(It.IsAny<PaymentScheme>(), It.IsAny<Account>(), It.IsAny<decimal>()))
                .Returns(false);

            var mockOptions = new Mock<IDataStoreTypeOptions>();
            mockOptions.SetupGet(x => x.DataStoreType).Returns(dataStoreType);
            
            var mockAccountService = new Mock<IAccountService>();
            mockAccountService.Setup(x => x.GetAccount(dataStoreType, request.DebtorAccountNumber)).Returns(account);

            var service = new PaymentService(mockOptions.Object, mockAccountService.Object, mockAccountValidationService.Object);

            // Act
            var result = service.MakePayment(request);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Success, Is.False);
            mockAccountService.Verify(x => x.UpdateAccount(It.IsAny<string>(), It.IsAny<Account>()), Times.Never());
        }
    }
}
