using System;

namespace Bank
{
    public class BankAccount
    {
        private readonly string m_customerName;
        private double m_balance;

        private BankAccount() { }

        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
        }

        //Возвращаем имся покупателя
        public string CustomerName
        {
            get { return m_customerName; }
        }


        //возвращаем текущий баланс
        public double Balance
        {
            get { return m_balance; }
        }

        #region Version 1
        //Метод который используется, когда денежные средства снимаются со счета
        //Будем тестировать его
        public void Debit(double amount)
        {
            if (amount > m_balance)
            {
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountExceedsBalanceMessage);
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage);
            }

            //m_balance += amount; // намерено не правильный код
            m_balance -= amount; // правильный код
        }
        #endregion



        //Выдача кредита
        public void Credit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            m_balance += amount;
        }

        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr. Bryan Walton", 11.99);

            ba.Credit(1000);
            ba.Debit(500);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
        }

        #region for Test 2,3,4
        public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";
        public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";
        #endregion
    }
}
