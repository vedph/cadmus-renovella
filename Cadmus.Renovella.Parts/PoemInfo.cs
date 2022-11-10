namespace Cadmus.Renovella.Parts
{
    /// <summary>
    /// Minimal information about a poem in a tale/collection of tales.
    /// </summary>
    public class PoemInfo
    {
        /// <summary>
        /// Gets or sets the metre.
        /// </summary>
        public string? Metre { get; set; }

        /// <summary>
        /// Gets or sets the incipit.
        /// </summary>
        public string? Incipit { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{Metre}] {Incipit}";
        }
    }
}
