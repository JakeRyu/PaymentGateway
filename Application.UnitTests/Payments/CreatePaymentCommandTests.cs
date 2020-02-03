using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Payments.Commands.CreatePayment;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using TestCommon;
using Xunit;

namespace Application.UnitTests.Payments
{
    [Collection("Application test collection")]
    public class CreatePaymentCommandTests
    {
        private IApplicationDbContext DbContext { get; }
        private static CreatePaymentCommand Command => new CreatePaymentCommand
        {
            MerchantId = 1,
            CardHolderName = "Test",
            CardNumber = "1111222233334444",
            ExpiryYearMonthString = "10/2020",
            Cvv = "999",
            Amount = 10,
            Currency = "GBP"
        };

        public CreatePaymentCommandTests(TestFixture testFixture)
        {
            DbContext = testFixture.DbContext;
        }
        
        [Fact]
        public async Task Handler_GivenValidRequest_ShouldUseBankClientToProcessPayment()
        {
            // Arrange
            var bankClientMock = new Mock<IBankClient>();
            bankClientMock.Setup(x => x.ProcessPayment(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 
                    It.IsAny<decimal>(), It.IsAny<string>()))
                .Returns(new PaymentResult
                {
                    Status = "Success",
                    PaymentId = Guid.NewGuid()
                });

            var bankClientFactoryMock = new Mock<IBankClientFactory>();
            bankClientFactoryMock.Setup(x => x.Create(It.IsAny<string>()))
                .Returns(bankClientMock.Object);

            var sut = new CreatePaymentCommand.Handler(DbContext, bankClientFactoryMock.Object);

            // Act
            await sut.Handle(Command, CancellationToken.None);

            // Assert
            bankClientMock.Verify(x => x.ProcessPayment(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 
                    It.IsAny<decimal>(), It.IsAny<string>()), 
                Times.Once);
        }

        [Fact]
        public async Task Handler_WhenBankReturnsError_ShouldRaisePaymentNotAcceptedException()
        {
            // Arrange
            var bankClientMock = new Mock<IBankClient>();
            bankClientMock.Setup(x => x.ProcessPayment(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 
                    It.IsAny<decimal>(), It.IsAny<string>()))
                .Returns(new PaymentResult
                {
                    Status = "Card number invalid"
                });
            
            var bankClientFactoryMock = new Mock<IBankClientFactory>();
            bankClientFactoryMock.Setup(x => x.Create(It.IsAny<string>()))
                .Returns(bankClientMock.Object);

            var sut = new CreatePaymentCommand.Handler(DbContext, bankClientFactoryMock.Object);
            
            // Act & Assert
            await Should.ThrowAsync<PaymentNotAcceptedException>(() =>
                sut.Handle(Command, CancellationToken.None));
        }
        
        [Fact]
        public async Task Handler_GivenValidRequest_ShouldStorePaymentDetails()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            var bankClientMock = new Mock<IBankClient>();
            bankClientMock.Setup(x => x.ProcessPayment(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 
                    It.IsAny<decimal>(), It.IsAny<string>()))
                .Returns(new PaymentResult
                {
                    Status = "Success",
                    PaymentId = paymentId
                });

            var bankClientFactoryMock = new Mock<IBankClientFactory>();
            bankClientFactoryMock.Setup(x => x.Create(It.IsAny<string>()))
                .Returns(bankClientMock.Object);

            var sut = new CreatePaymentCommand.Handler(DbContext, bankClientFactoryMock.Object);

            // Act
            await sut.Handle(Command, CancellationToken.None);
            
            // Assert
            var entity = await DbContext.Payments.SingleOrDefaultAsync(x => x.Id == paymentId);
            entity.ShouldNotBeNull();
        }
    }
}