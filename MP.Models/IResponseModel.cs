using System;
using System.Runtime.Serialization;
using MP.Models.Enums;
using MP.Models.Rest;

namespace MP.Models
{
    public interface IResponseModel
    {
        Guid UserId { get; set; }
        ModelTypes ModelType { get; }

        [DataMember(Order = 900000000)]
        bool Success { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 900000001)]
        Error Error { get; set; }
    }
}
