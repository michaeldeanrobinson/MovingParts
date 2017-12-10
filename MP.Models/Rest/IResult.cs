using System;

namespace MP.Models.Rest
{
    public interface IResult
    {
        TimeSpan ExecutionTime { get; set; }
        string ExecutionTimeString { get; set; }
        Error Error { get; set; }
        bool Success { get; set; }
        Guid ProcessTag { get; set; }
        bool SetValue(object value);
        object GetValue();
        string ToString();
    }
}
