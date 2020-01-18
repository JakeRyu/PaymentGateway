using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Payments.Commands.CreatePayment;
using Application.UnitTests.Common;
using BankClient;
using Moq;
using Xunit;

namespace Application.UnitTests
{
    public class CreatePaymentCommandTests : CommandTestBase
    {
        private readonly CreatePaymentCommand.Handler sut;    // Stands for System Under Test
        private readonly Mock<IBankClient> bankClientMock;
        private readonly CreatePaymentCommand command;
        
        public CreatePaymentCommandTests() : base()
        {
            command = new CreatePaymentCommand
            {
                MerchantId = Guid.NewGuid(),
                CardHolderName = "John Smith",
                CardNumber = "1111222233334444",
                ExpiryMonth = 12,
                ExpiryYear = 2020,
                Amount = 10,
                Currency = "GBP"
            };
            
            bankClientMock = new Mock<IBankClient>();
            // Mock process-payment-behaviour to be successful for happy path
            bankClientMock.Setup(x => x.ProcessPayment(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(),
                    It.IsAny<string>()))
                .Returns(new PaymentResult
                {
                    Status = "Success",
                    PaymentId = Guid.NewGuid()
                });

            var bankClientFactoryMock = new Mock<IBankClientFactory>();
            bankClientFactoryMock.Setup(x => x.Create(It.IsAny<string>()))
                .Returns(bankClientMock.Object);
            
            sut = new CreatePaymentCommand.Handler(Context, bankClientFactoryMock.Object);
        }
        
        [Fact]
        public async Task Handler_GivenValidRequest_ShouldUseBankClientToProcessPayment()
        {
            // Act
            await sut.Handle(command, CancellationToken.None);

            // Assert
            bankClientMock.Verify(m => m.ProcessPayment(command.MerchantId.ToString(), command.CardHolderName,
                command.CardNumber, command.ExpiryYear, command.ExpiryMonth, command.Amount, command.Currency), 
                Times.Once);
        }

        [Fact]
        public async Task Handler_WhenBankReturnsError_ShouldRaisePaymentNotAcceptedException()
        {
            // Arrange (Override mock behaviour)
            bankClientMock.Setup(x => x.ProcessPayment(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(),
                    It.IsAny<string>()))
                .Returns(new PaymentResult
                {
                    Status = "Card number invalid"
                });
            
            // Act & Assert
            await Assert.ThrowsAsync<PaymentNotAcceptedException>(() => 
                sut.Handle(command, CancellationToken.None));
        }
        
        [Fact]
        public async Task Handler_GivenValidRequest_ShouldStorePaymentDetails()
        {
            // Act
            await sut.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.Equal(1, Context.Payments.Count());
        }
    }
}