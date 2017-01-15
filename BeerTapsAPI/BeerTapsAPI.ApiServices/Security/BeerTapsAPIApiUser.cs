using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.AspNet;
using IQ.Platform.Framework.WebApi.Services.Security;

namespace BeerTapsAPI.ApiServices.Security
{

    public class BeerTapsAPIApiUser : ApiUser<UserAuthData>
    {
        public BeerTapsAPIApiUser(string name, Option<UserAuthData> authData)
            : base(authData)
        {
            Name = name;
        }

        public string Name { get; private set; }

    }

    public class BeerTapsAPIUserFactory : ApiUserFactory<BeerTapsAPIApiUser, UserAuthData>
    {
        public BeerTapsAPIUserFactory() :
            base(new HttpRequestDataStore<UserAuthData>())
        {
        }

        protected override BeerTapsAPIApiUser CreateUser(Option<UserAuthData> auth)
        {
            return new BeerTapsAPIApiUser("BeerTapsAPI user", auth);
        }
    }

}
