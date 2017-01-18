using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using System.Collections.Generic;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class RemoveTapSpec : SingleStateResourceSpec<RemoveTap, int>
    {
        public static ResourceUriTemplate UriRemove = ResourceUriTemplate.Create("Offices({OfficeID})/Taps({id})/Remove");

        protected override IEnumerable<ResourceLinkTemplate<RemoveTap>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriRemove, c => c.OfficeID, c => c.Id);
        }

        public override IResourceStateSpec<RemoveTap, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<RemoveTap, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Office, TapSpec.UriTapsAtOffice, r => r.OfficeID, r => r.Id)
                        },
                        Operations = new StateSpecOperationsSource<RemoveTap, int>()
                        {
                            InitialPost = ServiceOperations.Create,
                            Post = ServiceOperations.Update
                            
                        },
                    };
            }
        }
    }
}