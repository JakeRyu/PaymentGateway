using System;
using Application.Features.Payments.Queries.GetPaymentsList;
using AutoMapper;
using Domain.Entities;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        // [Theory]
        // [InlineData(typeof(Payment), typeof(PaymentDto))]
        // public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        // {
        //     var instance = Activator.CreateInstance(source);
        //
        //     var result = Should.NotThrow(() => _mapper.Map(instance, source, destination));
        //     result.ShouldBeOfType(destination);
        // }
        
        [Fact]
        public void ShouldMapPaymentToPaymentDto()
        {
            var entity = new Payment();
        
            var result = _mapper.Map<PaymentDto>(entity);
        
            result.ShouldNotBeNull();
            result.ShouldBeOfType<PaymentDto>();
        }
    }
}
