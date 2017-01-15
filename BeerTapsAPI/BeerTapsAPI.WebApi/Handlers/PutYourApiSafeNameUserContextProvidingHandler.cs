using IQ.Platform.Framework.WebApi.AspNet;
using IQ.Platform.Framework.WebApi.AspNet.Handlers;
using IQ.Platform.Framework.WebApi.Services.Security;
using BeerTapsAPI.ApiServices.Security;

namespace BeerTapsAPI.WebApi.Handlers
{
    public class PutYourApiSafeNameUserContextProvidingHandler
            : ApiSecurityContextProvidingHandler<BeerTapsAPIApiUser, NullUserContext>
    {

        public PutYourApiSafeNameUserContextProvidingHandler(
            IStoreDataInHttpRequest<BeerTapsAPIApiUser> apiUserInRequestStore)
            : base(new BeerTapsAPIUserFactory(), CreateContextProvider(), apiUserInRequestStore)
        {
        }

        static ApiUserContextProvider<BeerTapsAPIApiUser, NullUserContext> CreateContextProvider()
        {
            return
                new ApiUserContextProvider<BeerTapsAPIApiUser, NullUserContext>(_ => new NullUserContext());
        }
    }
}