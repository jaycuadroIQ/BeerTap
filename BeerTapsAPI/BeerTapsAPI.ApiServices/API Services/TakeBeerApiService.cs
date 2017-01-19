using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using IQ.Platform.Framework.WebApi.Services.Security;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;

using BeerTapsAPI.ApiServices.Security;
using BeerTapsAPI.Model;
using BeerTapsAPI.Data;
using System.Net;

namespace BeerTapsAPI.ApiServices
{
    public class TakeGlassApiService : ITakeBeerApiService
    {
        readonly IApiUserProvider<BeerTapsAPIApiUser> _userProvider;
        public TakeGlassApiService(IApiUserProvider<BeerTapsAPIApiUser> userProvider)
        {

            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;
        }


        public Task<TakeBeer> UpdateAsync(TakeBeer resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeID =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));
            var tapID =
                context.UriParameters.GetByName<int>("ID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply tap ID in the URI.", System.Net.HttpStatusCode.BadRequest));

            
            var tap = TapApiService.GetTapById(tapID, officeID);

            if (tap.HasValue)
            {
                if (tap.Value.Remaining == 0)
                {
                    throw context.CreateHttpResponseException<Tap>("There is not enough beer remaining in the keg.",
                        HttpStatusCode.BadRequest);
                }
            }
            else
                context.CreateHttpResponseException<Tap>("Beer not found.", HttpStatusCode.NotFound);


            return Task.FromResult(UpdateTap(tap.Value.Id, tap.Value.OfficeID));

        }


        private TakeBeer UpdateTap(int id, int officeID)
        {
            TakeBeer updatedTap = new TakeBeer();
            
            using (var context = new BeerTapsApiDataModel())
            {
                var tap = context.TapsData.SingleOrDefault(x => x.Id == id && x.OfficeID == officeID);

                if (tap != null)
                {
                    tap.Remaining = tap.Remaining -= 1;
                    tap.TapState = TapApiService.GetTransitionState(tap.Remaining);
                    
                    context.SaveChanges();

                    updatedTap.Id = tap.Id;
                    updatedTap.Remaining = tap.Remaining;
                    updatedTap.OfficeID = tap.OfficeID;
                    updatedTap.Name = tap.Name;
                }
                
            }
            return updatedTap;
        }

        

    }
}
