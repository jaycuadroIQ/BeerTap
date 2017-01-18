using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using System.Collections.Generic;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class ReplaceTapSpec : SingleStateResourceSpec<ReplaceTap, int>
    {
        public static ResourceUriTemplate UriReplace = ResourceUriTemplate.Create("Offices({OfficeID})/Taps({id})/Replace");

        protected override IEnumerable<ResourceLinkTemplate<ReplaceTap>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriReplace, c => c.OfficeID, c => c.Id);
        }

        public override IResourceStateSpec<ReplaceTap, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<ReplaceTap, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Tap, TapSpec.UriTapsAtOffice, r => r.OfficeID, r => r.Id)
                        },
                        Operations = new StateSpecOperationsSource<ReplaceTap, int>()
                        {
                            InitialPost = ServiceOperations.Create,
                            Post = ServiceOperations.Update
                            
                        },
                    };
            }
        }
    }
}