using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

using System.Collections.Generic;

namespace BeerTapsAPI.Model
{
    /// <summary>
    /// Represents IQ office
    /// </summary>
    public class Office : BeerTapResource, IStatelessResource, IIdentifiable<int>
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the office
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Indicator of how much of tap is left
        /// </summary>
        public List<Tap> Taps { get; set; }
    }
}
