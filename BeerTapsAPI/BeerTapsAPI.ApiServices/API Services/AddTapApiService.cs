using System.Threading.Tasks;
using System.Threading;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;
using BeerTapsAPI.Model;
using BeerTapsAPI.Data;


namespace BeerTapsAPI.ApiServices
{
    public class AddTapApiService : IAddTapApiService
    {
 

        public Task<AddTap> UpdateAsync(AddTap resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeId =
                context.UriParameters.GetByName<int>("id").EnsureValue(
                    () => context.CreateHttpResponseException<Tap>("Please supply office ID in the URI.", System.Net.HttpStatusCode.BadRequest));

            var tap = CreateNewTap(officeId,resource.Name,resource.Remaining);

            return Task.FromResult
            (
                new AddTap()
                {
                    Id = tap.Id,
                    OfficeID = tap.OfficeID,
                    Remaining = tap.Remaining,
                    Name = tap.Name
                }
            );
        }
        
        private Tap CreateNewTap(int officeId, string tapName, int remaining)
        {
            Tap newTap;

            using (var context = new BeerTapsApiDataModel())
            {
                int newRemaining = remaining > 0 ? remaining : 5;
                newTap = new Tap()
                {
                    Name = tapName,
                    OfficeID = officeId,
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
