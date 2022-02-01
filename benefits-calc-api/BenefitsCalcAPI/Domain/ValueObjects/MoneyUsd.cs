namespace Domain.ValueObjects;

public readonly struct MoneyUsd : IEquatable<MoneyUsd>
{
    public MoneyUsd(decimal amount)
    {
        Amount = amount;
    }
    public bool Equals(MoneyUsd other)
    {
        return Amount == other.Amount;
    }

    public override bool Equals(object? obj)
    {
        return obj is MoneyUsd other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Amount.GetHashCode();
    }

    public decimal Amount { get; }

    public MoneyUsd DivideBy(decimal denominator, out MoneyUsd remainder)
    {
        remainder = ((Amount * 100m) % denominator) / 100m;
        var newAmount = (Amount - remainder) / denominator;
        return newAmount;
    }

    #region Operator Overloads
    //equality
    public static bool operator ==(MoneyUsd lhs, MoneyUsd rhs) => lhs.Amount == rhs.Amount;
    public static bool operator !=(MoneyUsd lhs, MoneyUsd rhs) => !(lhs == rhs);
    public static bool operator <(MoneyUsd lhs, MoneyUsd rhs) => lhs.Amount < rhs.Amount;
    public static bool operator >(MoneyUsd lhs, MoneyUsd rhs) => lhs.Amount > rhs.Amount;
    public static bool operator <=(MoneyUsd lhs, MoneyUsd rhs) => lhs.Amount <= rhs.Amount;
    public static bool operator >=(MoneyUsd lhs, MoneyUsd rhs) => lhs.Amount >= rhs.Amount;

    //casting
    public static implicit operator MoneyUsd(decimal amount) => new(amount);
    
    public static implicit operator decimal(MoneyUsd money) => money.Amount;
    

    //arithmetic
    public static MoneyUsd operator +(MoneyUsd lhs, MoneyUsd rhs) =>
        new MoneyUsd(lhs.Amount + rhs.Amount);

    public static MoneyUsd operator -(MoneyUsd lhs, MoneyUsd rhs) =>
        new(lhs.Amount - rhs.Amount);

    public static MoneyUsd operator /(MoneyUsd lhs, MoneyUsd rhs) =>
        rhs.Amount is 0m ? throw new DivideByZeroException() : new MoneyUsd(lhs.Amount / rhs.Amount);

    public static MoneyUsd operator *(MoneyUsd lhs, MoneyUsd rhs) => new(lhs.Amount * rhs.Amount);

    #endregion

}