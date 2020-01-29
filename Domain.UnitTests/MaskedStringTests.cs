using System.Resources;
using Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace Domain.UnitTests
{
    public class MaskedStringTests
    {
        [Fact]
        public void MaskedString_ValueShouldNotBeOriginalValue()
        {
            var cardNumber = "1111-2222-3333-4444";
            var sut = new MaskedString(cardNumber);
            
            sut.Value.ShouldNotBe(cardNumber);
        }
        
        [Fact]
        public void MaskedString_OnComparison_ShouldCompareOriginalValue()
        {
            var cardNumber = "1111-2222-3333-4444";
            var maskedString1 = new MaskedString(cardNumber);
            var maskedString2 = new MaskedString(cardNumber);

            maskedString1.ShouldBe(maskedString2);
        }
        
        [Fact]
        public void MaskedString_WhenGeneratingWithSameString_ShouldMaskRandomly()
        {
            var cardNumber = "1111-2222-3333-4444";
            var maskedString1 = new MaskedString(cardNumber);
            var maskedString2 = new MaskedString(cardNumber);

            maskedString1.Value.ShouldNotBe(maskedString2.Value);
        }

        [Fact]
        public void MaskedString_WhenConvertedToString_ShouldHaveMaskedValue()
        {
            var cardNumber = "1111-2222-3333-4444";
            var sut = new MaskedString(cardNumber);

            string converted = sut;
            
            sut.Value.ShouldNotBe(cardNumber);
        }
    }
}