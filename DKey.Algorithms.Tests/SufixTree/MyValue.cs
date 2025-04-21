namespace DKey.Algorithms.Tests.SufixTree;

public class MyValue : IComparable<MyValue>
{
    public int Value { get; }

    public MyValue(int value)
    {
        Value = value;
    }

    public int CompareTo(MyValue? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return (Value % 5).CompareTo(other.Value % 5);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        MyValue other = (MyValue)obj;
        return (Value % 5) == (other.Value % 5);
    }

    public override int GetHashCode()
    {
        return (Value % 5).GetHashCode();
    }
}