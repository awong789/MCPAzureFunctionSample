namespace MyLegacyApp.Core
{
    public class LoanCalculator
    {
        public LoanCalculator()
        {
            // constructor can be used for initialization if needed
        }

        public decimal CalculateMonthlyPayment(decimal principal, decimal annualInterestRate, int numberOfPayments)
        {
            if (annualInterestRate <= 0 || numberOfPayments <= 0)
            {
                throw new ArgumentException("Interest rate and number of payments must be greater than zero.");
            }
            decimal monthlyInterestRate = annualInterestRate / 12 / 100;
            decimal monthlyPayment = principal * monthlyInterestRate / (1 - (decimal)Math.Pow((double)(1 + monthlyInterestRate), -numberOfPayments));
            return Math.Round(monthlyPayment, 2);
        }
    }
}
