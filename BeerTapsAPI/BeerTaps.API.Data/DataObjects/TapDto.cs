using BeerTapsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTapsAPI.Data
{
    public class TapDto
    {
        public string Id { get; set; }

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
        public string OfficeID { get; set; }
    }
}
