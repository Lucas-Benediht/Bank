namespace Bank.Core.Interface
{
    public interface IAccountRepositories
    {
         void Add(Account account);
         Account? GetByNumber(int accountNumber);
         IEnumerable<Account> GetAll();
         void Update(Account account);
    }
}