using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;

namespace BankTest
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        public void Test1_Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Иван Иванов", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Снятие с аккаунта не корректно");
        }

        [TestMethod]
        public void Test2_Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange1()
        {
            // Arrange
            double beginningBalance = -11.99;
            double debitAmount = 100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
        }

        [TestMethod]
        public void Test3_Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange2()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                //Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }
            Assert.Fail("Ожидался вызов исключения о том, что снятие больше чем баланс, но этого не произошло.");
        }
    }
}
