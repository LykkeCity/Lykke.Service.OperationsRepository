namespace Lykke.Service.OperationsRepository.Contract
{
    public sealed class PaymentSystem
    {
        public PaymentSystem(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(PaymentSystem paymentSystem)
        {
            return paymentSystem.ToString();
        }
    }
}