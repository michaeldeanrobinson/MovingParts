using System;
using System.Runtime.Serialization;
using MP.Models.Enums;
using MP.Models.Rest;

namespace MP.Models.Authorization.Models
{
    [DataContract(Namespace = "")]
    public class AuthorizationTokenResponseModel : IResponseModel
    {
        public Guid UserId { get; set; }
        public ModelTypes ModelType { get { return ModelTypes.AuthorizationToken; } }

        [DataMember(Order = 0)]
        public bool Success
        {
            get { return Error == null; }
            set { /* This property is readonly. Empty braces are needed for Protobuffer support */ }
        }

        [DataMember(Order = 1, EmitDefaultValue = false)]
        public Error Error { get; set; }

        [DataMember(Order = 2)]
        public string AuthenticationToken { get; set; }

        [DataMember(Order = 3)]
        public DateTime Expiration { get; set; }
    }
}
