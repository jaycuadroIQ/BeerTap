using System;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapsAPI.Model
{
    /// <summary>
    /// Replacing keg
    /// </summary>
    public class ReplaceTap :  IUpdateTapResource
    {
        /// <summary>
        /// Tap ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Office ID
        /// </summary>
        public int OfficeId { get; set; }
        /// <summary>
        /// remaining
        /// </summary>
        public int Remaining { get; set; }
        /// <summary>
        /// Name for this keg
        /// </summary>
        public string Name { get; set; }
        
    }
}