using Application.Common.Interfaces;

namespace Bank.ApiClients
{
    public class BankClientFactory : IBankClientFactory
    {
        public IBankClient Create(string cardNumber)
        {
            // Assume that the first 4 digits of card number are used to identify a bank
            IBankClient bankClient = cardNumber.Substring(0, 4) switch
            {
                "1111" => new SantanderClient(),
                "2222" => new BarclayClient(),
                _ => new NullClient()
            };

            return bankClient;
        }
    }
}