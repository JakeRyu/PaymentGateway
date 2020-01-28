using Domain.Exceptions;
using Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace Domain.UnitTests
{
    public class CardExpiryDateTests
    {
        [Theory]
        [InlineData("12/20", "31/12/2020")]
        [InlineData("11/20", "30/11/2020")]
        [InlineData("02/20", "29/02/2020")]
        public void CardExpiryDate_GivenExpiryYearMonthString_ShouldCreateExpiryDate(string expiryYearMonthString, string expiryDate)
        {
            var sut = CardExpiryDate.For(expiryYearMonthString);

            // Date property should be the end of month
            sut.Date.ToString("dd/MM/yyyy").ShouldBe(expiryDate);
            
            // ToString is overriden to return original input
            sut.ToString().ShouldBe(expiryYearMonthString);
        }

        [Theory]
        [InlineData("13/20")]
        [InlineData("1l/20")]
        [InlineData("10/2O")]
        public void CardExpiryDate_GivenWrongInput_ShouldRaiseCardExpiryDateInvalidException(string cardExpiryYearMonthString)
        {
            Should.Throw<CardExpiryDateInvalidException>(() => CardExpiryDate.For(cardExpiryYearMonthString));
        }

        [Fact]
        public void CardExpiryDate_ShouldConvertToStringImplicitly()
        {
            var expiryYearMonthString = "01/20";
            var sut = CardExpiryDate.For(expiryYearMonthString);

            string converted = sut;
            
            sut.Date.ToString("dd/MM/yyyy").ShouldBe("31/01/2020");
            converted.ShouldBe(expiryYearMonthString);
        }

        [Fact]
        public void CardExpiryDate_GivenSameValue_ShouldBeEqual()
        {
            var expiryYearMonthString = "01/20";
            var sut1 = CardExpiryDate.For(expiryYearMonthString);   
            var sut2 = CardExpiryDate.For(expiryYearMonthString);  
            
            sut1.ShouldBe(sut2);
        }
    }
}