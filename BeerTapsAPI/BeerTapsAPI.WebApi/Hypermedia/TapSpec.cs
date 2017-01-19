using System.Collections.Generic;

using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class TapSpec : ResourceSpec<Tap, TapState, int>
    {

        public static ResourceUriTemplate UriTapsAtOffice = ResourceUriTemplate.Create("Offices({OfficeID})/Taps({id})");
        public override string EntrypointRelation
        {
            get { return LinkRelations.Tap; }
        }
        protected override IEnumerable<ResourceLinkTemplate<Tap>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriTapsAtOffice, c => c.OfficeID, c => c.Id);
        }

        protected override IEnumerable<IResourceStateSpec<Tap, TapState, int>> GetStateSpecs()
        {
            yield return new ResourceStateSpec<Tap, TapState, int>(TapState.Unknown)
            {
                Links =
                {

                    CreateLinkTemplate(LinkRelations.Taps.Full, UriTapsAtOffice, c => c.OfficeID, c => c.Id)

                },
                Operations = new StateSpecOperationsSource<Tap, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete


                }
            };

            yield return new ResourceStateSpec<Tap, TapState, int>(TapState.Full)
            {
                Links =
                {
                    
                    CreateLinkTemplate(LinkRelations.Taps.Full, UriTapsAtOffice, c => c.OfficeID, c => c.Id),
                    CreateLinkTemplate(LinkRelations.UpdateKeg.Remove, UriTapsAtOffice, r => r.OfficeID, r => r.Id),
                    CreateLinkTemplate(LinkRelations.UpdateKeg.TakeBeer, UriTapsAtOffice, r => r.OfficeID, r => r.Id)
                },
                Operations = new StateSpecOperationsSource<Tap, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                    

                }
            };

            yield return new ResourceStateSpec<Tap, TapState, int>(TapState.HalfEmpty)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Taps.HalfEmpty, UriTapsAtOffice, c => c.OfficeID, c => c.Id),
                    CreateLinkTemplate(LinkRelations.UpdateKeg.Remove, UriTapsAtOffice, r => r.OfficeID, r => r.Id),
                    CreateLinkTemplate(LinkRelations.UpdateKeg.TakeBeer, UriTapsAtOffice, r => r.OfficeID, r => r.Id)
                },
                Operations = 
                {
                    Get = ServiceOperations.Get,
                    Post = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };

            yield return new ResourceStateSpec<Tap, TapState, int>(TapState.AlmostEmpty)
            {
                Links=
                {
                    CreateLinkTemplate(LinkRelations.Taps.AlmostEmpty, UriTapsAtOffice, c => c.OfficeID, c => c.Id),
                    CreateLinkTemplate(LinkRelations.UpdateKeg.Replace, ReplaceTapSpec.UriReplace, r => r.OfficeID, r => r.Id),
                    CreateLinkTemplate(LinkRelations.UpdateKeg.Remove, UriTapsAtOffice, r => r.OfficeID, r => r.Id),
                    CreateLinkTemplate(LinkRelations.UpdateKeg.TakeBeer, UriTapsAtOffice, r => r.OfficeID, r => r.Id)
                },
                Operations = 
                {
                    Get = ServiceOperations.Get,
                    Post = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
                
            };

            yield return new ResourceStateSpec<Tap, TapState, int>(TapState.Empty)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Taps.Empty, UriTapsAtOffice, c => c.OfficeID, c => c.Id),
                    CreateLinkTemplate(LinkRelations.UpdateKeg.Replace, ReplaceTapSpec.UriReplace, r => r.OfficeID, r => r.Id),
                    CreateLinkTemplate(LinkRelations.UpdateKeg.Remove, UriTapsAtOffice, r => r.OfficeID, r => r.Id)
                },
                Operations = new StateSpecOperationsSource<Tap, int>()
                {
                    Get = ServiceOperations.Get,
                    Post = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };

        }
    }
}