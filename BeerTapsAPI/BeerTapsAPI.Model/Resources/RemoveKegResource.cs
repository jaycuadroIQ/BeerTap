﻿using System;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapsAPI.Model
{
    /// <summary>
    /// Replacing keg
    /// </summary>
    public class RemoveKeg :  IUpdateKegResource
    {
        /// <summary>
        /// pending
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Office ID
        /// </summary>
        public int OfficeID { get; set; }
        /// <summary>
        /// remaining
        /// </summary>
        public int Remaining { get; set; }
        
    }
}