using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DistrictSales.Api.Domain.Exceptions;

public sealed class ObjectNotFoundException : Exception
{
    public ObjectNotFoundException(string? message) : base(message)
    {
    }
}
