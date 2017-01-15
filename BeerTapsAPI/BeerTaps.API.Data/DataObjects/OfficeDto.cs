using BeerTapsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTapsAPI.Data
{
    public class OfficeDto

    {
        public OfficeDto()
        {

        }
        /// <summary>
        /// Identifier
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name of the office
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Indicator of how much of tap is left
        /// </summary>
        public IList<TapDto> Taps { get; set; }
    }
}
