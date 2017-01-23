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
    public class ReplaceTapApiService : IReplaceTapApiService
    {
        readonly IApiUserProvider<BeerTapsAPIApiUser> _userProvider;
        public ReplaceTapApiService(IApiUserProvider<BeerTapsAPIApiUser> userProvider)
        {

            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;
        }


        public Task<ReplaceTap> UpdateAsync(ReplaceTap resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeID =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));
            var tapID =
                context.UriParameters.GetByName<int>("ID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply tap ID in the URI.", System.Net.HttpStatusCode.BadRequest));

            if (resource.Remaining <= 0)
            {
                throw context.CreateHttpResponseException<Tap>("Invalid amount of beer to replace the keg.",
                        HttpStatusCode.BadRequest);
            }

            var tap = TapApiService.GetTapById(tapID, officeID);

            if (tap.HasValue)
            {
                if (tap.Value.Remaining > resource.Remaining)
                {
                    throw context.CreateHttpResponseException<Tap>("The replacement keg should have more content than the existing one.",
                        HttpStatusCode.BadRequest);
                }
            }
            else
                throw context.CreateHttpResponseException<Tap>("Resource beer not found.", HttpStatusCode.NotFound);



            return Task.FromResult(UpdateTap(tapID, officeID, resource.Name, resource.Remaining));
                
        }


        private ReplaceTap UpdateTap(int id, int officeID, string newName, int replacementAmount)
        {
            ReplaceTap replacementTap = null;
            const int defaultTapContent = 5;

            using (var context = new BeerTapsApiDataModel())
            {
                var tap = context.TapsData.SingleOrDefault(x => x.Id == id && x.OfficeID == officeID);

                if (tap != null)
                {
                    tap.Remaining = replacementAmount;
                    tap.TapState = TapState.Full;
                    if (!string.IsNullOrEmpty(newName))
                    {
                        tap.Name = newName;
                    }
                    
                    context.SaveChanges();

                    replacementTap = new ReplaceTap();
                    replacementTap.Name = tap.Name;
                    replacementTap.Id = tap.Id;
                    replacementTap.OfficeID = tap.OfficeID;
                    replacementTap.Remaining = tap.Remaining;
                }

            }
            return replacementTap;
        }

        

    }
}
