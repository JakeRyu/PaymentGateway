using Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace Domain.UnitTests
{
    public class MoneyTests
    {
        [Fact]
        public void Money_GivenObjectWithSameProperties_ShouldEqual()
        {
            var money1 = new Money(100m, "GBP");
            var money2 = new Money(100m, "GBP");
            
            money1.Equals(money2).ShouldBeTrue();
        }

        [Fact]
        public void Money_GivenObjectWithDifferentProperties_ShouldNotEqual()
        {
            var money1 = new Money(100m, "GBP");
            var money2 = new Money(100m, "USD");

            money1.Equals(money2).ShouldBeFalse();
        }
        
        [Fact]
        public void Money_GivenObjectWithNullProperties_ShouldNotEqual()
        {
            var money1 = new Money(100m, "GBP");
            var money2 = new Money(100m, null);
            
            money1.Equals(money2).ShouldBeFalse();
        }
    }
}