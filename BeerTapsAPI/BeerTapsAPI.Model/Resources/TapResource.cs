using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapsAPI.Model
{
    /// <summary>
    /// This represents beer tap
    /// </summary>
    public class Tap : IStatefulResource<TapState>, IIdentifiable<int>
    {
        /// <summary>
        /// Unique Identifier for the Tap
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Office name for this tap where it belongs
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// How much is remaining in liters
        /// </summary>
        public int Remaining { get; set; }

        /// <summary>
        /// How much beer is remaining on this tap on 
        /// </summary>
        public TapState TapState { get; set; }
        /// <summary>
        /// Id where this particular tap belongs
        /// </summary>
        public int OfficeID { get; set; }
        
    }
}
