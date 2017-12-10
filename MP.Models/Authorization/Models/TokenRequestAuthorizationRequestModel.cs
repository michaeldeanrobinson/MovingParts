using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using FluentValidation.Attributes;
using MP.Models.Authorization.Validators;
using MP.Models.Enums;

namespace MP.Models.Authorization.Models
{
    [DataContract(Namespace = "", Name = "TokenRequest")]
    [Validator(typeof(TokenRequestAuthorizationRequestModelValidator))]
    public class TokenRequestAuthorizationRequestModel : IRequestModel
    {
        public Guid ProcessTag { get; set; }
        public Guid UserId { get; set; }
        public ModelTypes ModelType {  get { return ModelTypes.Authorization; } }

        [DataMember(Order = 0)]
        [Required]
        public string Username { get; set; }

        [DataMember(Order = 1)]
        [Required]
        public string Password { get; set; }

        public string UserAgent { get; set; }
        public string ClientIp { get; set; }
    }
}
