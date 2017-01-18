﻿using System.Collections.Generic;

using IQ.Platform.Framework.Common;
using BeerTapsAPI.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;

namespace BeerTapsAPI.WebApi.Hypermedia
{
    public class TapStateProvider : ResourceStateProviderBase<Tap, TapState>
    { 
        public override TapState GetFor(Tap resource)
        {
            return resource.TapState;
        }

        protected override IDictionary<TapState, IEnumerable<TapState>> GetTransitions()
        {
            return new Dictionary<TapState, IEnumerable<TapState>>
            {
                {
                    TapState.Unknown, new []
                    {
                        TapState.Full,
                        TapState.HalfEmpty,
                        TapState.AlmostEmpty,
                        TapState.HalfEmpty
                    }
                },
                {
                    TapState.Full, new[]
                    {
                        TapState.HalfEmpty,
                        TapState.Unknown,
                    }
                },
                {
                    TapState.HalfEmpty, new[]
                    {
                        TapState.AlmostEmpty,
                        TapState.Unknown,
                    }
                },
                {
                    TapState.AlmostEmpty, new[]
                    {
                        TapState.Empty,
                        TapState.Unknown,
                    }
                },
                {
                    TapState.Empty, new[]
                    {
                        TapState.Full,
                        TapState.HalfEmpty,
                        TapState.AlmostEmpty,
                        TapState.HalfEmpty

                    }
                }
            };

        }

        public override IEnumerable<TapState> All
        {
            get { return EnumEx.GetValuesFor<TapState>(); }
        }
    }



}