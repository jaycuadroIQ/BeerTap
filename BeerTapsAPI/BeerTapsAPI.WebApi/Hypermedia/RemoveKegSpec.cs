using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using System.Collections.Generic;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class RemoveKegSpec : SingleStateResourceSpec<RemoveKeg, int>
    {
        public static ResourceUriTemplate UriRemove = ResourceUriTemplate.Create("Offices({OfficeID})/Taps({id})/Remove");

        protected override IEnumerable<ResourceLinkTemplate<RemoveKeg>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriRemove, c => c.OfficeID, c => c.Id);
        }

        public override IResourceStateSpec<RemoveKeg, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<RemoveKeg, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Office, TapSpec.UriTapsAtOffice, r => r.OfficeID, r => r.Id)
                        },
                        Operations = new StateSpecOperationsSource<RemoveKeg, int>()
                        {
                            InitialPost = ServiceOperations.Create,
                            Post = ServiceOperations.Update
                            
                        },
                    };
            }
        }
    }
}