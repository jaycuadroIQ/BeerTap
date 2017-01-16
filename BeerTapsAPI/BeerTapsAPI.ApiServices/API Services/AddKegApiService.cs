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
    public class AddKegApiService : IAddKegApiService
    {
 

        public Task<AddKeg> UpdateAsync(AddKeg resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeID =
                context.UriParameters.GetByName<int>("id").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));

            var tap = CreateNewTap(officeID,resource.Name,resource.Remaining);

            return Task.FromResult
            (
                new AddKeg()
                {
                    Id = tap.Id,
                    OfficeID = tap.OfficeID,
                    Remaining = tap.Remaining,
                    Name = tap.Name
                }
            );
        }
        
        private Tap CreateNewTap(int officeID, string tapName, int remaining)
        {
            Tap newTap = new Tap();

            using (var context = new BeerTapsApiDataModel())
            {
                int newRemaining = remaining > 0 ? remaining : 5;
                newTap = new Tap()
                {
                    Name = tapName,
                    OfficeID = officeID,
                    Remaining = newRemaining,
                    TapState = TapApiService.GetTransitionState(newRemaining)
                };

                context.TapsData.Add(newTap);
                context.SaveChanges();
            }

            return newTap;
        }
        
    }
}
