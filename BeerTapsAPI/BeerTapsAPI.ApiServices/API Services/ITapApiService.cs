using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTapsAPI.ApiServices
{
    public interface ITapApiService :
        IGetAResourceAsync<Tap, int>,
        IGetManyOfAResourceAsync<Tap, int>,
        IUpdateAResourceAsync<Tap, int>,
        ICreateAResourceAsync<Tap,int>
    {
    }
}
