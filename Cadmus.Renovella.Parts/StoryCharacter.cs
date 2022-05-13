using System.Text;

namespace Cadmus.Renovella.Parts
{
    /// <summary>
    /// A character in a tale, used by <see cref="TaleStoryPart"/>.
    /// </summary>
    public class StoryCharacter
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sex (M, F, or -).
        /// </summary>
        public char Sex { get; set; }

        /// <summary>
        /// Gets or sets the role of the character in the story.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this character is rather
        /// a generic designation for a group of undetermined persons, e.g.
        /// "facchini".
        /// </summary>
        public bool IsGroup { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new();
            if (IsGroup) sb.Append('*');
            sb.Append(Name);
            if (Sex != '\0') sb.Append(" (").Append(Sex).Append(')');
            if (!string.IsNullOrEmpty(Role)) sb.Append(", ").Append(Role);

            return sb.ToString();
        }
    }
}
