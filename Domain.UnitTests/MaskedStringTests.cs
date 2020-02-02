using System.Resources;
using Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace Domain.UnitTests
{
    public class MaskedStringTests
    {
        private const string _cardNumber = "1111-2222-3333-4444";

        [Fact]
        public void MaskedString_ValueShouldNotBeOriginalValue()
        {
            var sut = new MaskedString(_cardNumber);
            
            sut.Value.ShouldNotBe(_cardNumber);
        }
        
        [Fact]
        public void MaskedString_OnComparison_ShouldCompareOriginalValue()
        {
            var maskedString1 = new MaskedString(_cardNumber);
            var maskedString2 = new MaskedString(_cardNumber);

            maskedString1.ShouldBe(maskedString2);
        }
        
        [Fact]
        public void MaskedString_WhenGeneratingWithSameString_ShouldMaskRandomly()
        {
            var maskedString1 = new MaskedString(_cardNumber);
            var maskedString2 = new MaskedString(_cardNumber);

            maskedString1.Value.ShouldNotBe(maskedString2.Value);
        }

        [Fact]
        public void MaskedString_WhenConvertedToString_ShouldHaveMaskedValue()
        {
            var sut = new MaskedString(_cardNumber);

            string converted = sut;
            
            sut.Value.ShouldNotBe(_cardNumber);
        }

        [Fact]
        public void MaskedString_GivenNullObjectOnComparison_ShouldReturnFalse()
        {
            var sut = new MaskedString(_cardNumber);
            
            sut.Equals(null).ShouldBeFalse();
        }
    }
}