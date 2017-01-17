using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQ.Platform.Framework.WebApi.Services.Security;
using BeerTapsAPI.ApiServices.Security;
using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi;
using BeerTapsAPI.Data;
using Castle.Components.DictionaryAdapter;
using Castle.Core.Internal;

namespace BeerTapsAPI.ApiServices
{
    public class OfficeApiService : IOfficeApiService 
    {

        readonly IApiUserProvider<BeerTapsAPIApiUser> _userProvider;
        
        public OfficeApiService(IApiUserProvider<BeerTapsAPIApiUser> userProvider)
        {
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;

        }


        public Task<Office> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            return Task.FromResult(GetOfficeByID(id));
        }

        public Task<IEnumerable<Office>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            
            return Task.FromResult(GetAll());
        }
        
        private Office GetOfficeByID(int id)
        {
            Office office = new Office();

            using (var context = new BeerTapsApiDataModel())
            {
                office = context.OfficesData.SingleOrDefault(x => x.Id == id);
                if (office != null)
                {
                    List<Tap> taps = context.TapsData.Where(x => x.OfficeID == id).ToList();
                    office.Taps = taps;
                }
            }
            return office;
        }

        private IEnumerable<Office> GetAll()
        {
            List<Office> updatedOfficeTapsReferenced = new List<Office>();
            using (var context = new BeerTapsApiDataModel())
            {
                foreach (Office office in context.OfficesData)
                {
                    updatedOfficeTapsReferenced.Add(GetOfficeByID(office.Id));   
                }

            }
            return updatedOfficeTapsReferenced;
        }

    }
}
