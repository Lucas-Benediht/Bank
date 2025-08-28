namespace Bank.Core.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string HolderName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        
        public Account(int accountNumber, string holderName, decimal balance)
        {
            AccountNumber = accountNumber;
            HolderName = holderName;
            Balance = balance;
        }

        public void Deposit(decimal amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentExcepetion("O valor do depósito deve ser maior que 0");
            }
            Balance += amount;
        }

        public void WithDraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser maior que 0");
            }
            if (amount > Balance)
            {
                throw new InvalidOperationException("Saldo insuficiente para o saque");
            }
            Balance -= amount;
        }

        public void Transfer(decimal amount, Account destinationAccount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("O valor da transferência deve ser maior que 0");
            }
            if (amount > Balance)
            {
                throw new InvalidOperationException("Saldo insuficiente para a transferência");
            }
            Balance -= amount;
            destinationAccount.Balance += amount;
        }

    }
}