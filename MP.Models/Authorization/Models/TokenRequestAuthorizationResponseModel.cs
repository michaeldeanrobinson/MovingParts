using System;
using System.Runtime.Serialization;
using MP.Models.Enums;
using MP.Models.Rest;

namespace MP.Models.Authorization.Models
{
    [DataContract(Namespace = "")]
    public class TokenRequestAuthorizationResponseModel : IResponseModel
    {
        [DataMember(Order = 0)] // Label ProcessTag as DocumentId in xml output to support Lee County
        public Guid ProcessTag { get; set; }

        public Guid UserId { get; set; }
        public ModelTypes ModelType { get { return ModelTypes.Authorization; } }

        [DataMember(Order = 1)]
        public bool Success
        {
            get { return Error == null; }
            set { /* This property is readonly. Empty braces are needed for Protobuffer support */ }
        }

        [DataMember(Order = 2, EmitDefaultValue = false)]
        public Error Error { get; set; }

        [DataMember(Order = 3)]
        public string AuthenticationToken { get; set; }

        [DataMember(Order = 4)]
        public DateTime Expiration { get; set; }
    }
}
