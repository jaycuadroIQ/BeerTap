using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using  System.Web;

using IQ.Platform.Framework.WebApi.Services.Security;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;

using BeerTapsAPI.Data;
using BeerTapsAPI.ApiServices.Security;
using BeerTapsAPI.Model;
using IQ.Foundation.WebApi.Exceptions;

namespace BeerTapsAPI.ApiServices
{
    public class TapApiService : ITapApiService
    {

        readonly IApiUserProvider<BeerTapsAPIApiUser> _userProvider;
        List<Tap> Taps = new List<Tap>();
        public TapApiService(IApiUserProvider<BeerTapsAPIApiUser> userProvider)
        {
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;
   }

        

        public async Task<Tap> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            var officeID =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));
            using (var ctx = new BeerTapsApiDataModel())
            {
                var tap =
                    await ctx.TapsData.SingleOrDefaultAsync(x => x.Id == id && x.OfficeID == officeID, cancellation);
                if (tap == null)
                    throw context.CreateNotFoundHttpResponseException<Tap>("Tap resource cannot be found.");
                return tap;
            }
        }

        public Task<IEnumerable<Tap>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            var officeID = 
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));
            return Task.FromResult(GetAll(officeID));


        }

        public async Task<ResourceCreationResult<Tap, int>> CreateAsync(Tap resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeId =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));

            var office = OfficeApiService.GetOfficeById(officeId);

            if (office == null)
                throw context.CreateNotFoundHttpResponseException<Office>("Office resource cannot be found.");

            if (string.IsNullOrEmpty(resource.Name))
                throw context.CreateHttpResponseException<Tap>("Please supply the new tap name in request body.",
                    HttpStatusCode.BadRequest);

            if (resource.Remaining <= 0 || resource.Remaining > 5)
                throw context.CreateHttpResponseException<Tap>(
                    "Invalid amount of beer. We can only have a maximum of 5 liters and minimum of 1 liter.",
                    HttpStatusCode.BadRequest);

            var tap = await CreateTap(officeId, resource.Name, resource.Remaining);
            return new ResourceCreationResult<Tap, int>(tap);

        }

        public Task<Tap> UpdateAsync(Tap resource, IRequestContext context, CancellationToken cancellation)
        {
            
            var officeID =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));
            var tapID =
                context.UriParameters.GetByName<int>("ID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply tap ID in the URI.", System.Net.HttpStatusCode.BadRequest));

            var office = OfficeApiService.GetOfficeById(officeID);

            if (office == null)
                throw context.CreateNotFoundHttpResponseException<Office>("Office resource cannot be found.");

            var tap = TapApiService.GetTapById(tapID, officeID);

            if (resource.Remaining <= 0 || resource.Remaining > 5)
                throw context.CreateHttpResponseException<Tap>(
                    "Invalid amount of beer to take.", HttpStatusCode.BadRequest);

            if (tap.HasValue)
            {
                if (tap.Value.Remaining == 0 ||
                        resource.Remaining > tap.Value.Remaining)
                {
                    throw context.CreateHttpResponseException<Tap>("There is not enough beer remaining in the keg.",
                        HttpStatusCode.BadRequest);
                }
            }
            else
                throw context.CreateNotFoundHttpResponseException<Tap>();


            return Task.FromResult(UpdateTap(tap.Value.Id, tap.Value.OfficeID, resource.Remaining));

        }
        private static async Task<Tap> CreateTap(int officeId, string tapName, int remaining)
        {
            Tap newTap;
            using (var context = new BeerTapsApiDataModel())
            {
                newTap = new Tap()
                {
                    Name = tapName,
                    OfficeID = officeId,
                    Remaining = remaining,
                    TapState = TapApiService.GetTransitionState(remaining)
                };

                context.TapsData.Add(newTap);
                await context.SaveChangesAsync();
            }

            return newTap;
        }

        public async Task DeleteAsync(ResourceOrIdentifier<Tap, int> input, IRequestContext context,
            CancellationToken cancellation)
        {
            using (var ctx = new BeerTapsApiDataModel())
            {
                Tap tap;
                if (input.HasResource)
                    tap = input.Resource;
                else
                {
                    var officeID =
                        context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                            () =>
                                context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.",
                                    System.Net.HttpStatusCode.BadRequest));

                    var office = OfficeApiService.GetOfficeById(officeID);

                    if (office == null)
                        throw context.CreateNotFoundHttpResponseException<Office>("Office resource cannot be found.");

                    tap =
                        await
                            ctx.TapsData.SingleOrDefaultAsync(x => x.Id == input.Id && x.OfficeID == officeID,
                                cancellation);
                    if (tap == null)
                        context.CreateNotFoundHttpResponseException<Tap>();


                }
                ctx.TapsData.Remove(tap);
                await ctx.SaveChangesAsync(cancellation);
            }
        }

        public static TapState GetTransitionState(int remaining)
        {
            TapState newState = TapState.Full;

            if (remaining >= 5)
                newState = TapState.Full;
            else if (remaining < 5 && remaining >= 3)
                newState = TapState.HalfEmpty;
            else if (remaining < 3 && remaining > 0)
                newState = TapState.AlmostEmpty;
            else
                newState = TapState.Empty;

            return newState;

        }

        private Tap UpdateTap(int id, int officeId, int remaining)
        {
            Tap updatedTap = new Tap();
            using (var context = new BeerTapsApiDataModel())
            {
                updatedTap = context.TapsData.SingleOrDefault(x => x.Id == id && x.OfficeID == officeId);
                updatedTap.Remaining = updatedTap.Remaining - remaining;
                updatedTap.TapState = GetTransitionState(updatedTap.Remaining);
                
                context.SaveChanges();
            }
            return updatedTap;
        }

        private IEnumerable<Tap> GetAll(int officeID)
        {
            using (var context = new BeerTapsApiDataModel())
            {
                return context.TapsData.Where(x => x.OfficeID == officeID).ToList();
            }
        }

        public static Option<Tap> GetTapById(int id, int officeId)
        {
            using (var context = new BeerTapsApiDataModel())
            {
                return context.TapsData.SingleOrDefaultAsOption(x => x.Id == id && x.OfficeID == officeId);
            }
        }
    }
}
