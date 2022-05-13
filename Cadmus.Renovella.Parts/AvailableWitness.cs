using System.Collections.Generic;

namespace Cadmus.Renovella.Parts
{
    /// <summary>
    /// A witness available in a specific context.
    /// </summary>
    public class AvailableWitness
    {
        /// <summary>
        /// Gets or sets the witness identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the coverage
        /// by this witness is only partial.
        /// </summary>
        public bool IsPartial { get; set; }

        /// <summary>
        /// Gets or sets a short note about this witness availability.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets a list of external IDs for the witness.
        /// </summary>
        public List<string> ExternalIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailableWitness"/> class.
        /// </summary>
        public AvailableWitness()
        {
            ExternalIds = new List<string>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Id + (IsPartial ? "*" : "");
        }
    }
}
