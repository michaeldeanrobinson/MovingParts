using System.Runtime.Serialization;

namespace MP.Models.Rest
{
    [DataContract(Namespace = "")]
    public class ErrorCode
    {
        [DataMember(Order = 0)]
        public int Number { get; set; }

        [DataMember(Order = 1)]
        public ErrorType Type { get; set; }
    }
}
