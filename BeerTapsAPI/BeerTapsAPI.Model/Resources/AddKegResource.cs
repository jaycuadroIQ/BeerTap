﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTapsAPI.Model
{
    /// <summary>
    /// Adding new Keg to tap
    /// </summary>
    public class AddKeg : IUpdateKegResource
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get; set;
        }
        /// <summary>
        /// OfficeID
        /// </summary>
        public int OfficeID
        {
            get; set;
        }
        /// <summary>
        /// Remaining
        /// </summary>
        public int Remaining
        {
            get; set;
        }

        public string Name { get; set; }
        
    }
}
