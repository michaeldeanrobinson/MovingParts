using System;
using MP.Services.Entities;

namespace MP.Services
{
    public class UserService
    {
        public UserEntity FindByUsernameAndPassword(string username, string password)
        {
            // TODO: add database lookup

            return GetBob();
        }

        public UserEntity FindByKey(Guid userId)
        {
            // TODO: add database lookup

            return GetBob();
        }

        private UserEntity GetBob()
        {
            return new UserEntity
            {
                UserId = new Guid("{0E98094E-960F-409B-9123-7164BDFF0933}"),
                FirstName = "Bob",
                LastName = "Loblaw",
            };
        }
    }
}
