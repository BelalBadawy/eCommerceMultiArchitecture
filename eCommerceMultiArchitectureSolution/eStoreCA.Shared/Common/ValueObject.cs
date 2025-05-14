namespace eStoreCA.Shared.Common;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (left is null ^ right is null) return false;

        return left?.Equals(right!) != false;
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    /// <summary>
    ///     Gets the components used to determine equality for this value object.
    /// </summary>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    ///     Determines whether the specified object is equal to the current value object.
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    ///     Serves as a hash function for the value object.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var component in GetEqualityComponents()) hash.Add(component);

        return hash.ToHashCode();
    }

    /// <summary>
    ///     Returns a string that represents the current value object.
    /// </summary>
    public override string ToString()
    {
        return $"{{{string.Join(", ", GetEqualityComponents().Select((c, i) => $"Prop{i} = {c}"))}}}";
    }


    #region Custom

    /// <example>
    /// Example of a derived class:
    /// public class MyValueObject : ValueObject
    /// {
    ///     public int Property1 { get; }
    ///     public string Property2 { get; }
    /// 
    ///     protected override IEnumerable<object> GetEqualityComponents()
    ///     {
    ///         yield return Property1;
    ///         yield return Property2;
    ///     }
    /// }
    /// </example>

    #endregion Custom
}