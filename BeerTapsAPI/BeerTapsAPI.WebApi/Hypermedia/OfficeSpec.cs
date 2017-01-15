using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class OfficeSpec : SingleStateResourceSpec<Office, int>
    {

        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Office; }
        }

        public override IResourceStateSpec<Office, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<Office, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Tap, TapSpec.UriTapsAtOffice.Many, r => r.Id),
                            CreateLinkTemplate(LinkRelations.UpdateKeg.Add, AddKegSpec.UriAdd, r => r.Id)

                        },
                        Operations = new StateSpecOperationsSource<Office, int>
                        {
                            Get = ServiceOperations.Get,
                            InitialPost = ServiceOperations.Create,
                            Post = ServiceOperations.Update,
                            Delete = ServiceOperations.Delete,
                        },
                    };
            }
        }

        // IResourceStateSpec is not required but can be overridden to define custom operations and links.
        // See example below...
        //
        //public override IResourceStateSpec<SampleResource, NullState, int> StateSpec
        //{
        //    get
        //    {
        //        return
        //            new SingleStateSpec<SampleResource, int>
        //            {
        //                Links =
        //                {
        //                    CreateLinkTemplate(LinkRelations.SampleResource2, OrganizationSpec2.Uri, r => r.Id),
        //                },

        //                Operations =
        //                {
        //                    Get = ServiceOperations.Get,
        //                    InitialPost = ServiceOperations.Create,
        //                    Post = ServiceOperations.Update,
        //                    Delete = ServiceOperations.Delete,
        //                },
        //            };
        //    }
        //}

    }
}