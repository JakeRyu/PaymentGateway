using System;
using Application.Common.Interfaces;
using Application.Features.Payments.Commands.CreatePayment;
using Persistence;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;
        
        public CommandTestBase()
        {
            Context = ApplicationDbContextFactory.Create();
        }
        
        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Context);
        }
    }
}