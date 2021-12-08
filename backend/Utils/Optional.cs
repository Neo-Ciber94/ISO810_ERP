
using System;

namespace ISO810_ERP.Utils;

public readonly struct Optional<T> : IEquatable<Optional<T>>
{
    private readonly T value;
    private readonly bool hasValue;

    public Optional(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        this.value = value;
        hasValue = true;
    }

    public bool HasValue => hasValue;

    public T Value
    {
        get
        {
            if (!hasValue)
            {
                throw new InvalidOperationException("Optional has no value");
            }

            return value;
        }
    }

    public T GetValueOrDefault(T defaultValue = default!)
    {
        return hasValue ? value : defaultValue;
    }

    public static implicit operator Optional<T>(Optional<Empty> _)
    {
        return new Optional<T>();
    }

    public static implicit operator Optional<T>(T value)
    {
        return new Optional<T>(value);
    }

    public static implicit operator T(Optional<T> optional)
    {
        return optional.Value;
    }

    public static bool operator ==(Optional<T> left, Optional<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Optional<T> left, Optional<T> right)
    {
        return !left.Equals(right);
    }

    public override string ToString()
    {
        return value?.ToString() ?? "";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Optional<T> optional)
        {
            return Equals(optional);
        }

        return false;
    }

    public bool Equals(Optional<T> other)
    {
        if (hasValue && other.hasValue)
        {
            return value!.Equals(other.value);
        }

        return hasValue == other.hasValue;
    }

    public override int GetHashCode()
    {
        if (hasValue)
        {
            return value!.GetHashCode();
        }

        return 0;
    }
}

public static class Optional
{
    public static Optional<Empty> None => new();

    public static Optional<T> Some<T>(T value)
    {
        return new Optional<T>(value);
    }
}