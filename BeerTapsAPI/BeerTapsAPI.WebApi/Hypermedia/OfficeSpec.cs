using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class OfficeSpec : SingleStateResourceSpec<Office, int>
    {

        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({id})");

        public override string EntrypointRelation => LinkRelations.Office;

        public override IResourceStateSpec<Office, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<Office, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Tap, TapSpec.UriTapsAtOffice.Many, r => r.Id)
                            

                        },
                        Operations = new StateSpecOperationsSource<Office, int>
                        {
                            Get = ServiceOperations.Get,
                        },
                    };
            }
        }

    }
}