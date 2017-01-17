using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using IQ.Platform.Framework.WebApi.Services.Security;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;

using BeerTapsAPI.ApiServices.Security;
using BeerTapsAPI.Model;
using BeerTapsAPI.Data;

namespace BeerTapsAPI.ApiServices
{
    public class ReplaceKegApiService : IReplaceKegApiService
    {
        readonly IApiUserProvider<BeerTapsAPIApiUser> _userProvider;
        public ReplaceKegApiService(IApiUserProvider<BeerTapsAPIApiUser> userProvider)
        {

            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;
        }


        public Task<ReplaceKeg> UpdateAsync(ReplaceKeg resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeID =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));
            var tapID =
                context.UriParameters.GetByName<int>("ID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply tap ID in the URI.", System.Net.HttpStatusCode.BadRequest));
            
            
            var tap = UpdateTap(tapID, officeID, resource.Remaining, resource.Name);

            return Task.FromResult
                (
                    new ReplaceKeg()
                    {
                        Id = tap.Id,
                        OfficeID = tap.OfficeID,
                        Remaining = tap.Remaining,
                        Name = tap.Name
                        
                    }
                );
        }


        private Tap UpdateTap(int id, int officeID, int remaining, string newName)
        {
            Tap tap = new Tap();

            using (var context = new BeerTapsApiDataModel())
            {
                tap = context.TapsData.SingleOrDefault(x => x.Id == id && x.OfficeID == officeID);

                if (tap != null)
                {
                    if (tap.Remaining > remaining)
                        throw new Exception("The replacement keg should have more content than the existing one.");

                    tap.Remaining = remaining;
                    tap.TapState = TapApiService.GetTransitionState(remaining);
                    if (!string.IsNullOrEmpty(newName))
                    {
                        tap.Name = newName;
                    }
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Can't find the specific tap to replace.");
                }
            }
          return tap;
        }

        
        
    }
}
