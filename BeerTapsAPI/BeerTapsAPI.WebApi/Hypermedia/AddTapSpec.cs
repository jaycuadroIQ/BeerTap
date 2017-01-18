using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using System.Collections.Generic;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class AddTapSpec : SingleStateResourceSpec<AddTap, int>
    {
        public static ResourceUriTemplate UriAdd = ResourceUriTemplate.Create("Offices({id})/Add");

        protected override IEnumerable<ResourceLinkTemplate<AddTap>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriAdd, c => c.OfficeID);
        }

        public override IResourceStateSpec<AddTap, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<AddTap, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Office, OfficeSpec.Uri, r => r.OfficeID)
                        },
                        Operations = new StateSpecOperationsSource<AddTap, int>()
                        {
                            InitialPost = ServiceOperations.Create,
                            Post = ServiceOperations.Update
                            
                        },
                    };
            }
        }
    }
}