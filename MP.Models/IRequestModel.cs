using System;
using MP.Models.Enums;

namespace MP.Models
{
    public interface IRequestModel
    {
        Guid ProcessTag { get; set; }
        Guid UserId { get; set; }
        ModelTypes ModelType { get; }
    }
}
