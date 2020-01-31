namespace Application.Common.Interfaces
{
    public interface IBankClientFactory
    {
        IBankClient Create(string cardNumber);
    }
}