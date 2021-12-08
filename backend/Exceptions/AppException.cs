using System;

namespace ISO810_ERP.Exceptions;

/// <summary>
/// A class that represents a posible client side error, like not found or invalid data.
/// </summary>
public class AppException : Exception
{
    public AppException(string message) : base(message) { }

    public AppException(string message, System.Exception innerException) : base(message, innerException) { }
}
