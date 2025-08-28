namespace Bank.Core.Controller
{
    public class AccountController
    {
        private readonly IAccountRepositories _repository;

        public AccountController(IAccountRepositories repository)
        {
            _repository = repository;
        }

        public void CreateAccount(int accountNumber, string holderName, decimal balance)
        {
            var account = new Account(accountNumber, holderName, balance);
            _repository.Add(account);  
        }

        public Account? GetAccountByNumber(int accountNumber)
        {
            return _repository.GetByNumber(accountNumber);
        }

        public void Deposit(int accountNumber, decimal amount)
        {
            var account = _repository.GetByNumber(accountNumber);
            if (account == null)
            {
                throw new ArgumentException("Conta não encontrada");
            }
            account.Deposit(amount);
            _repository.Update(account);
        }

        public void Withdraw(int accountNumber, decimal amount)
        {
            var account = _repository.GetByNumber(accountNumber);
            if (account == null)
            {
                throw new ArgumentException("Conta não encontrada");
            }
            account.WithDraw(amount);
            _repository.Update(account);
        }

        public void Transfer(int sourceAccountNumber, int destinationAccountNumber, decimal amount)
        {
            var sourceAccount = _repository.GetByNumber(sourceAccountNumber);
            var destinationAccount = _repository.GetByNumber(destinationAccountNumber);
            if (sourceAccount == null || destinationAccount == null)
            {
                throw new ArgumentException("Conta não encontrada");
            }
            sourceAccount.Transfer(amount, destinationAccount);
            _repository.Update(sourceAccount);
            _repository.Update(destinationAccount);
        }

        public IEnumerable<Account> GetAllAccounts()      
        {
            return _repository.GetAll();
        }

        public void UpdateAccount(int accountNumber, string holderName, decimal balance)
        {
            var account = _repository.GetByNumber(accountNumber);
            if (account == null)
            {
                throw new ArgumentException("Conta não encontrada");
            }
            account.HolderName = holderName;
            account.Balance = balance;
            _repository.Update(account);
        }
    }

}