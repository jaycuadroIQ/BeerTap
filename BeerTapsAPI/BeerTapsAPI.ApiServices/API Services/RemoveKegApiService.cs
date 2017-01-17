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
    public class RemoveKegApiService : IRemvoveKegApiService
    {
        readonly IApiUserProvider<BeerTapsAPIApiUser> _userProvider;
        public RemoveKegApiService(IApiUserProvider<BeerTapsAPIApiUser> userProvider)
        {
            
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;

           
        }

        
        public Task<RemoveKeg> UpdateAsync(RemoveKeg resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeID =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));
            var tapID =
                context.UriParameters.GetByName<int>("ID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply tap ID in the URI.", System.Net.HttpStatusCode.BadRequest));

            var tap = RemoveTap(tapID, officeID);

            return Task.FromResult
                (
                    new RemoveKeg()
                    {
                        Id = tap.Id,
                        OfficeID = tap.OfficeID,
                        Remaining = tap.Remaining,
                    }
                );
        }

      
      
        private Tap RemoveTap(int id, int officeID)
        {
            Tap tap = new Tap();

            using (var context = new BeerTapsApiDataModel())
            {
                tap = context.TapsData.SingleOrDefault(x => x.Id == id && x.OfficeID == officeID);

                if (tap != null)
                {

                    context.TapsData.Remove(tap);
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
