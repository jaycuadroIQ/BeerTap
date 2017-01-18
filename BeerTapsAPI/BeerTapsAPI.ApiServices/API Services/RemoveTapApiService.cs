using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class RemoveTapApiService : IRemvoveTapApiService
    {
        readonly IApiUserProvider<BeerTapsAPIApiUser> _userProvider;
        public RemoveTapApiService(IApiUserProvider<BeerTapsAPIApiUser> userProvider)
        {
            
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;

           
        }

        
        public Task<RemoveTap> UpdateAsync(RemoveTap resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeID =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));
            var tapID =
                context.UriParameters.GetByName<int>("ID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply tap ID in the URI.", System.Net.HttpStatusCode.BadRequest));

            var tap = GetTapById(tapID, officeID);
            if (!tap.HasValue)
                throw context.CreateHttpResponseException<RemoveTap>("Beer not found.", HttpStatusCode.NotFound);

            return Task.FromResult(RemoveTap(tapID, officeID));
        }

      
      
        private RemoveTap RemoveTap(int id, int officeID)
        {
            RemoveTap tapToRemove = new RemoveTap();
            using (var context = new BeerTapsApiDataModel())
            {
                var tap = context.TapsData.SingleOrDefault(x => x.Id == id && x.OfficeID == officeID);

                if (tap != null)
                {

                    context.TapsData.Remove(tap);
                    context.SaveChanges();

                    tapToRemove.Id = tap.Id;
                    tapToRemove.OfficeID = tap.OfficeID;
                    tapToRemove.Remaining = tap.Remaining;
                }
                
            }

            return tapToRemove;
        }
        private Option<Tap> GetTapById(int id, int officeId)
        {
            using (var context = new BeerTapsApiDataModel())
            {
                return context.TapsData.SingleOrDefaultAsOption(x => x.Id == id && x.OfficeID == officeId);
            }
        }

    }
}
