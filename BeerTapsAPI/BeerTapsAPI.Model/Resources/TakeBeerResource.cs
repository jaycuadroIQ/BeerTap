using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTapsAPI.Model
{
    /// <summary>
    /// Taking beer from tap
    /// </summary>
    public class TakeBeer : IUpdateTapResource
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// OfficeID
        /// </summary>
        public int OfficeID { get; set; }
        /// <summary>
        /// Remaining
        /// </summary>
        public int Remaining { get; set; }
        /// <summary>
        /// Name of the keg/tap to be added
        /// </summary>
        public string Name { get; set; }
        
    }
}
