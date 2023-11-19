namespace DistrictSales.Api.Domain.Exceptions;

public sealed class DuplicateEntryException : Exception
{
    public DuplicateEntryException(string? message) : base(message)
    {
    }
}
