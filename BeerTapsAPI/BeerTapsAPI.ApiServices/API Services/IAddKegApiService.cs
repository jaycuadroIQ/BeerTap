﻿using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTapsAPI.ApiServices
{
    public interface IAddKegApiService :
        IUpdateAResourceAsync<AddKeg, int>
    {
    }
}
