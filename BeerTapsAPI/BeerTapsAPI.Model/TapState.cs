using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTapsAPI.Model
{
    /// <summary>
    /// Keg State
    /// </summary>
    public enum TapState
    {
        /// <summary>
        /// Null, empty or unknown
        /// </summary>
        //Unknown,
        /// <summary>
        /// Keg is full yay!
        /// </summary>
        Full,
        /// <summary>
        /// Half empty
        /// </summary>
        HalfEmpty,
        /// <summary>
        /// Almost gone gg
        /// </summary>
        AlmostEmpty,
        /// <summary>
        /// Uh-oh :(
        /// </summary>
        Empty
    }
}
