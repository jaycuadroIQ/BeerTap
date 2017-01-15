using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerTapsAPI.Model
{
    public interface IStatefulTap
    {
        TapState TapState { get; }

        string Name { get; set; }

        int Remaining { get; set; }

        string OfficeID { get; set; }

    }
}