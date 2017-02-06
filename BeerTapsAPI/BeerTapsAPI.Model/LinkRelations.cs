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
        /// Replacing Keg
        /// </summary>
        public static class UpdateTap
        {
            /// <summary>
            /// replace
            /// </summary>
            public const string Replace = "iq.UpdateTap.Replace";
            /// <summary>
            /// Add
            /// </summary>
            public const string Add = "iq.UpdateTap.Add";
            /// <summary>
            /// Remove
            /// </summary>
            public const string Remove = "iq.UpdateTap.Remove";
            /// <summary>
            /// Take beer from keg
            /// </summary>
            public const string TakeBeer = "iq.UpdateTap.TakeBeer";
        }
    }
}
