namespace BeerTapsAPI.Model
{
    /// <summary>
    /// iQmetrix link relation names
    /// </summary>
    public static class LinkRelations
    {
        /// <summary>
        /// link relation to describe the Identity resource.
        /// </summary>
        public const string Tap = "iq:Tap";
        /// <summary>
        /// office name
        /// </summary>
        public const string Office = "iq:Office";

        /// <summary>
        /// For taps
        /// </summary>
        public static class Taps
        {
            /// <summary>
            /// add
            /// </summary>
            public const string Full = "iq:Taps.Full";
            /// <summary>
            /// remove
            /// </summary>
            public const string HalfEmpty = "iq:Taps.HalfEmpty";
            /// <summary>
            /// For almost empty state
            /// </summary>
            public const string AlmostEmpty = "iq:Taps.AlmostEmpty";
            /// <summary>
            /// For empty state
            /// </summary>
            public const string Empty = "iq:Taps.Empty";
            /// <summary>
            /// remove
            /// </summary>
            public const string Remove = "iq:Taps.Remove";
            /// <summary>
            /// replace keg
            /// </summary>
            public const string Replace = "iq:Taps.Replace";


        }
        /// <summary>
        /// Replacing Keg
        /// </summary>
        public static class UpdateKeg
        {
            /// <summary>
            /// replace
            /// </summary>
            public const string Replace = "iq.ReplaceKeg.Replace";
            /// <summary>
            /// Add
            /// </summary>
            public const string Add = "iq.ReplaceKeg.Add";
            /// <summary>
            /// Remove
            /// </summary>
            public const string Remove = "iq.ReplaceKeg.Remove";

        }
    }
}
