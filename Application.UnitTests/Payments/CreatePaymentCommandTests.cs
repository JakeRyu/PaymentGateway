using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Payments.Commands.CreatePayment;
using Application.UnitTests.Common;
using Moq;
using Persistence;
using Xunit;

namespace Application.UnitTests.Payments
{
    [Collection("Application test collection")]
    public class CreatePaymentCommandTests
    {
        private IApplicationDbContext Db { get; }
        private static CreatePaymentCommand Command => new CreatePaymentCommand
        {
            MerchantId = Guid.NewGuid(),
            CardHolderName = "John Smith",
            CardNumber = "1111222233334444",
            ExpiryMonth = 12,
            ExpiryYear = 2020,
            Cvv = 999,
            Amount = 10,
            Currency = "GBP"
        };

        public CreatePaymentCommandTests(TestFixture testFixture)
        {
            Db = testFixture.Db;
        }
        
        [Fact]
        public async Task Handler_GivenValidRequest_ShouldUseBankClientToProcessPayment()
        {
            // Arrange
            var bankClientMock = new Mock<IBankClient>();
            bankClientMock.Setup(x => x.ProcessPayment(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(), 
                    It.IsAny<string>()))
                .Returns(new PaymentResult
                {
                    Status = "Success",
                    PaymentId = Guid.NewGuid()
                });

            var acquireBankMock = new Mock<IAcquireBank>();
            acquireBankMock.Setup(x => x.Create(It.IsAny<string>()))
                .Returns(bankClientMock.Object);

            var sut = new CreatePaymentCommand.Handler(Db, acquireBankMock.Object);

            // Act
            await sut.Handle(Command, CancellationToken.None);

            // Assert
            bankClientMock.Verify(x => x.ProcessPayment(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(),
                    It.IsAny<string>()), 
                Times.Once);
        }

        [Fact]
        public async Task Handler_WhenBankReturnsError_ShouldRaisePaymentNotAcceptedException()
        {
            // Arrange
            var bankClientMock = new Mock<IBankClient>();
            bankClientMock.Setup(x => x.ProcessPayment(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(),
                    It.IsAny<string>()))
                .Returns(new PaymentResult
                {
                    Status = "Card number invalid"
                });
            
            var acquireBankMock = new Mock<IAcquireBank>();
            acquireBankMock.Setup(x => x.Create(It.IsAny<string>()))
                .Returns(bankClientMock.Object);

            var sut = new CreatePaymentCommand.Handler(Db, acquireBankMock.Object);
            
            // Act & Assert
            await Assert.ThrowsAsync<PaymentNotAcceptedException>(() => 
                sut.Handle(Command, CancellationToken.None));
        }
        
        [Fact]
        public async Task Handler_GivenValidRequest_ShouldStorePaymentDetails()
        {
            // Arrange
            var bankClientMock = new Mock<IBankClient>();
            bankClientMock.Setup(x => x.ProcessPayment(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(),
                    It.IsAny<string>()))
                .Returns(new PaymentResult
                {
                    Status = "Success",
                    PaymentId = Guid.NewGuid()
                });

            var acquireBankMock = new Mock<IAcquireBank>();
            acquireBankMock.Setup(x => x.Create(It.IsAny<string>()))
                .Returns(bankClientMock.Object);

            var sut = new CreatePaymentCommand.Handler(Db, acquireBankMock.Object);

            // Act
            await sut.Handle(Command, CancellationToken.None);
            
            // Assert
            Assert.Equal(1, Db.Payments.Count());
        }
    }
}