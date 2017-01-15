using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using System.Collections.Generic;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class AddKegSpec : SingleStateResourceSpec<AddKeg, int>
    {
        public static ResourceUriTemplate UriAdd = ResourceUriTemplate.Create("Offices({id})/Add");

        protected override IEnumerable<ResourceLinkTemplate<AddKeg>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriAdd, c => c.OfficeID);
        }

        public override IResourceStateSpec<AddKeg, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<AddKeg, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Office, OfficeSpec.Uri, r => r.OfficeID)
                        },
                        Operations = new StateSpecOperationsSource<AddKeg, int>()
                        {
                            InitialPost = ServiceOperations.Create,
                            Post = ServiceOperations.Update
                            
                        },
                    };
            }
        }
    }
}