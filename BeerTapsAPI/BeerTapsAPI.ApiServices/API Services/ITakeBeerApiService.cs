using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTapsAPI.ApiServices
{
    public interface ITakeBeerApiService :
        IUpdateAResourceAsync<TakeBeer, int>
    {
    }
}
