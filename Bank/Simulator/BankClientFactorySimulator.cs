using Application.Common.Interfaces;

namespace Bank.Simulator
{
    public class BankClientFactorySimulator : IBankClientFactory
    {
        public IBankClient Create(string cardNumber)
        {
            return new BankSimulator();;
        }
    }
}