namespace Application.Common.Interfaces
{
    public interface IAcquireBank
    {
        IBankClient Create(string cardNumber);
    }
}