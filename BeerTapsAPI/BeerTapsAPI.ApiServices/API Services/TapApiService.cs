using System;
using System.Collections.Generic;
using System.Linq;
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

        

        public Task<Tap> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            var officeID =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));


            return Task.FromResult(GetTapById(id, officeID));
        }

        public Task<IEnumerable<Tap>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            var officeID = 
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));


            return Task.FromResult(GetAll(officeID));


        }

        public Task<ResourceCreationResult<Tap, int>> CreateAsync(Tap resource, IRequestContext context, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<Tap> UpdateAsync(Tap resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeID =
                context.UriParameters.GetByName<int>("OfficeID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));
            var tapID =
                context.UriParameters.GetByName<int>("ID").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply tap ID in the URI.", System.Net.HttpStatusCode.BadRequest));

            var tap = UpdateTap(tapID, officeID, resource.Remaining);
            

            return Task.FromResult(tap);
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

        private Tap GetTapById(int id, int officeID)
        {
            using (var context = new BeerTapsApiDataModel())
            {
                return context.TapsData.SingleOrDefault(x => x.Id == id && x.OfficeID == officeID);
            }
        }

        private Tap UpdateTap(int id, int officeID, int remaining)
        {
            var tap = new Tap();

            using (var context = new BeerTapsApiDataModel())
            {
                tap = context.TapsData.SingleOrDefault(x => x.Id == id && x.OfficeID == officeID);

                if (tap != null)
                {
                    if (tap.Remaining < remaining)
                        throw new HttpRequestException("There is not enough beer remaining in the keg.");
                    else if(remaining <= 0)
                        throw new HttpRequestException("Invalid amount of beer to take.");

                    tap.Remaining = tap.Remaining - remaining;
                    tap.TapState = GetTransitionState(tap.Remaining);

                    context.SaveChanges();
                }
            }
            return tap;
        }

        private IEnumerable<Tap> GetAll(int officeID)
        {
            using (var context = new BeerTapsApiDataModel())
            {
                return context.TapsData.Where(x => x.OfficeID == officeID).ToList();
            }
        }
    }
}
