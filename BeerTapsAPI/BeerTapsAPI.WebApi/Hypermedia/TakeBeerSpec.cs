using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using System.Collections.Generic;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class TakeBeerSpec : SingleStateResourceSpec<TakeBeer, int>
    {
        public static ResourceUriTemplate UriTake = ResourceUriTemplate.Create("Offices({OfficeID})/Taps({id})/TakeBeer");

        protected override IEnumerable<ResourceLinkTemplate<TakeBeer>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriTake, c => c.OfficeID, c=> c.Id);
        }

        public override IResourceStateSpec<TakeBeer, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<TakeBeer, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Office, OfficeSpec.Uri, r => r.OfficeID)
                        },
                        Operations = new StateSpecOperationsSource<TakeBeer, int>()
                        {
                            Post = ServiceOperations.Update,
                            Put = ServiceOperations.Update
                            
                        },
                    };
            }
        }
    }
}