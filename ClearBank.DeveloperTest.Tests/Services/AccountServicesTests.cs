using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class AccountServicesTests
    {
        [Test]
        public void GetAccount_DataStoreTypeIsBackup_ReturnsAccountFromBackupAccountDataStore()
        {
            // Arrange
            var mockAccountDataStore = new Mock<IAccountDataStore>();
            mockAccountDataStore.Setup(x => x.GetAccount("test")).Returns(new Account());
            var mockBackupAccountDataStore = new Mock<IBackupAccountDataStore>();
            mockBackupAccountDataStore.Setup(x => x.GetAccount("test")).Returns(new Account());

            var service = new AccountService(mockAccountDataStore.Object, mockBackupAccountDataStore.Object);
            var dataStoreType = "Backup";

            // Act
            var account = service.GetAccount(dataStoreType, "test");

            // Assert
            Assert.That(account, Is.Not.Null);
            mockBackupAccountDataStore.Verify(x => x.GetAccount("test"), Times.Once());
            mockAccountDataStore.Verify(x => x.GetAccount(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void GetAccount_DataStoreTypeIsNotBackup_ReturnsAccountFromAccountDataStore()
        {
            // Arrange
            var mockAccountDataStore = new Mock<IAccountDataStore>();
            mockAccountDataStore.Setup(x => x.GetAccount("test")).Returns(new Account());
            var mockBackupAccountDataStore = new Mock<IBackupAccountDataStore>();
            mockBackupAccountDataStore.Setup(x => x.GetAccount("test")).Returns(new Account());

            var service = new AccountService(mockAccountDataStore.Object, mockBackupAccountDataStore.Object);
            var dataStoreType = "OTHER";

            // Act
            var account = service.GetAccount(dataStoreType, "test");

            // Assert
            Assert.That(account, Is.Not.Null);
            mockBackupAccountDataStore.Verify(x => x.GetAccount(It.IsAny<string>()), Times.Never());
            mockAccountDataStore.Verify(x => x.GetAccount("test"), Times.Once());
        }

        [Test]
        public void UpdateAccount_AccountTypeIsBackup_CallsBackupAccountDataStore()
        {
            // Arrange
            var mockAccountDataStore = new Mock<IAccountDataStore>();
            var mockBackupAccountDataStore = new Mock<IBackupAccountDataStore>();

            var service = new AccountService(mockAccountDataStore.Object, mockBackupAccountDataStore.Object);
            var dataStoreType = "Backup";
            var accountToUpdate = new Account();

            // Act
            service.UpdateAccount(dataStoreType, accountToUpdate);

            // Assert
            mockBackupAccountDataStore.Verify(x => x.UpdateAccount(accountToUpdate), Times.Once());
            mockAccountDataStore.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Never());
        }

        [Test]
        public void UpdateAccount_AccountTypeIsNotBackup_CallsAccountDataStore()
        {
            // Arrange
            var mockAccountDataStore = new Mock<IAccountDataStore>();
            var mockBackupAccountDataStore = new Mock<IBackupAccountDataStore>();

            var service = new AccountService(mockAccountDataStore.Object, mockBackupAccountDataStore.Object);
            var dataStoreType = "OTHER";
            var accountToUpdate = new Account();

            // Act
            service.UpdateAccount(dataStoreType, accountToUpdate);

            // Assert
            mockAccountDataStore.Verify(x => x.UpdateAccount(accountToUpdate), Times.Once());
            mockBackupAccountDataStore.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Never());
        }
    }
}
