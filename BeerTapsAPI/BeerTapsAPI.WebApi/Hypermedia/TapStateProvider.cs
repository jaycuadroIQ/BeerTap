using System.Collections.Generic;

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
                    TapState.Full, new[]
                    {
                        TapState.HalfEmpty,
                        TapState.AlmostEmpty,
                        TapState.Empty
                    }
                },
                {
                    TapState.HalfEmpty, new[]
                    {
                        TapState.AlmostEmpty,
                        TapState.Empty,
                        TapState.Full
                        
                    }
                },
                {
                    TapState.AlmostEmpty, new[]
                    {
                        TapState.Empty,
                        TapState.Full, 
                        TapState.HalfEmpty
                        
                    }
                },
                {
                    TapState.Empty, new[]
                    {
                        TapState.Full,
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