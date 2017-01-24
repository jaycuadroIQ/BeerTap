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
            var officeId =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", HttpStatusCode.BadRequest));
            var tapId =
                context.UriParameters.GetByName<int>("ID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply tap ID in the URI.", HttpStatusCode.BadRequest));

            if (resource.Remaining <= 0)
            {
                throw context.CreateHttpResponseException<Tap>("Invalid amount of beer to replace the keg.",
                        HttpStatusCode.BadRequest);
            }

            var tap = TapApiService.GetTapById(tapId, officeId);

            if (tap.HasValue)
            {
                if (tap.Value.Remaining > resource.Remaining)
                {
                    throw context.CreateHttpResponseException<Tap>("The replacement keg should have more content than the existing one.",
                        HttpStatusCode.BadRequest);
                }
                var newTap = tap.Value;
                newTap.Remaining = resource.Remaining;
                newTap.Name = !string.IsNullOrEmpty(resource.Name) ? resource.Name : newTap.Name;
                newTap.TapState = TapApiService.GetTransitionState(newTap.Remaining);

                newTap = UpdateTap(newTap);

                return Task.FromResult
                (
                    new ReplaceTap()
                    {
                        Id = newTap.Id,
                        Name = newTap.Name,
                        OfficeId = newTap.OfficeId,
                        Remaining = newTap.Remaining
                    }
                );

            }
            else
                throw context.CreateHttpResponseException<Tap>("Resource tap not found.", HttpStatusCode.NotFound);



            //return Task.FromResult(UpdateTap(tapId, officeId, resource.Name, resource.Remaining));
                
        }


        private ReplaceTap UpdateTap(int id, int officeId, string newName, int replacementAmount)
        {
            ReplaceTap replacementTap = null;
            
            using (var context = new BeerTapsApiDataModel())
            {
                var tap = context.TapsData.SingleOrDefault(x => x.Id == id && x.OfficeId == officeId);

                if (tap != null)
                {
                    tap.Remaining = replacementAmount;
                    tap.TapState = TapApiService.GetTransitionState(tap.Remaining);
                    if (!string.IsNullOrEmpty(newName))
                    {
                        tap.Name = newName;
                    }
                    
                    context.SaveChanges();

                    replacementTap = new ReplaceTap();
                    replacementTap.Name = tap.Name;
                    replacementTap.Id = tap.Id;
                    replacementTap.OfficeId = tap.OfficeId;
                    replacementTap.Remaining = tap.Remaining;
                }

            }
            return replacementTap;
        }

        private Tap UpdateTap(Tap tap)
        {
            using (var context = new BeerTapsApiDataModel())
            {
                var tapToUpdate = context.TapsData.SingleOrDefault(x => x.Id == tap.Id && x.OfficeId == tap.OfficeId);
                if (tapToUpdate == null)
                    return null;
                
                tapToUpdate.Name = tap.Name;
                tapToUpdate.Remaining = tap.Remaining;
                tapToUpdate.TapState = tap.TapState;
                context.SaveChanges();

                return tapToUpdate;
            }
        }

    }
}
