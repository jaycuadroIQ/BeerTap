using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTapsAPI.ApiServices
{
    public interface IRemvoveKegApiService :
        IUpdateAResourceAsync<RemoveKeg, int>
    {
    }
}
