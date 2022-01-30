namespace Domain.ValueObjects;

public class SsnVO
{
    private const int SsnLength = 9;
    public SsnVO(ReadOnlySpan<char> ssn)
    {
        if (!CheckValid(ssn))
            throw new ArgumentException($"The value provided is not a valid ssn", nameof(ssn));

        Ssn = ssn.ToString();
    }

    private SsnVO(string ssn)
    {
        Ssn = ssn;
    }

    public static bool CheckValid(ReadOnlySpan<char> value) => !value.IsEmpty && value.Length is SsnLength && IsNumeric(value);
    public string Ssn { get; }

    public override string ToString() => Ssn;

    public string ToString(Func<SsnVO, string> formatterFunc) => formatterFunc(this);

    public static bool TryParse(ReadOnlySpan<char> value, out SsnVO? ssnVo)
    {
        ssnVo = CheckValid(value) ? new(value.ToString()) : null;
        return ssnVo is not null;
    }
    private static bool IsNumeric(ReadOnlySpan<char> value)
    {
        foreach (var c in value)
        {
            if (!char.IsNumber(c))
                return false;
        }

        return true;
    }

    public static implicit operator string(SsnVO ssn) => ssn.Ssn;
    public static implicit operator SsnVO(string ssn) => new(ssn);

}

