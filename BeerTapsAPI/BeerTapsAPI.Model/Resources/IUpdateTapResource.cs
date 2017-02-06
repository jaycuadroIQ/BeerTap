using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapsAPI.Model
{
    /// <summary>
    ///  For updating Keg
    /// </summary>
    public interface IUpdateTapResource : IStatelessResource, IIdentifiable<int>
    {
        /// <summary>
        /// Tap ID
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Office ID
        /// </summary>
        int OfficeId { get; set; }
        /// <summary>
        /// remaining
        /// </summary>
        int Remaining { get; set; }
    }
}
