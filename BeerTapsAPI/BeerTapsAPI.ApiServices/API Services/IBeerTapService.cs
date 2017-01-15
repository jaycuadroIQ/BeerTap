using BeerTapsAPI.Model;

namespace BeerTapsAPI.ApiServices
{
    public interface IBeerTapAPIService
    {
        Tap GetTapById(int id, int officeID);
    }
}

    

