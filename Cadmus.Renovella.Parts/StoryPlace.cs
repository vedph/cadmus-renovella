namespace Cadmus.Renovella.Parts
{
    /// <summary>
    /// The place of a story, used by <see cref="TaleStoryPart"/>.
    /// </summary>
    public class StoryPlace
    {
        /// <summary>
        /// Gets or sets the place's type (e.g. city, country, etc.).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the place's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an optional location inside the place (e.g. "Chiesa
        /// di S.Anna").
        /// </summary>
        public string Location { get; set; }

        public override string ToString()
        {
            return $"[{Type}] {Name}" +
                (string.IsNullOrEmpty(Location)? "" : ", " + Location);
        }
    }
}
