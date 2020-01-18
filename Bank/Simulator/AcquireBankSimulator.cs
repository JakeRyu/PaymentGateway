using Application.Common.Interfaces;

namespace Bank.Simulator
{
    public class AcquireBankSimulator : IAcquireBank
    {
        public IBankClient Create(string cardNumber)
        {
            return new BankSimulator();;
        }
    }
}