using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using System.Collections.Generic;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class ReplaceKegSpec : SingleStateResourceSpec<ReplaceKeg, int>
    {
        public static ResourceUriTemplate UriReplace = ResourceUriTemplate.Create("Offices({OfficeID})/Taps({id})/Replace");

        protected override IEnumerable<ResourceLinkTemplate<ReplaceKeg>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriReplace, c => c.OfficeID, c => c.Id);
        }

        public override IResourceStateSpec<ReplaceKeg, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<ReplaceKeg, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Tap, TapSpec.UriTapsAtOffice, r => r.OfficeID, r => r.Id)
                        },
                        Operations = new StateSpecOperationsSource<ReplaceKeg, int>()
                        {
                            InitialPost = ServiceOperations.Create,
                            Post = ServiceOperations.Update
                            
                        },
                    };
            }
        }
    }
}