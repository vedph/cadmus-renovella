using System.Linq;
using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Renovella.Parts
{
    /// <summary>
    /// Available witnesses part.
    /// Tag: <c>it.vedph.renovella.available-witnesses</c>.
    /// </summary>
    [Tag("it.vedph.renovella.available-witnesses")]
    public sealed class AvailableWitnessesPart : PartBase
    {
        /// <summary>
        /// Gets or sets the witnesses.
        /// </summary>
        public List<AvailableWitness> Witnesses { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailableWitnessesPart"/> class.
        /// </summary>
        public AvailableWitnessesPart()
        {
            Witnesses = new List<AvailableWitness>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: <c>witness</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item = null)
        {
            DataPinBuilder builder = new();

            builder.Set("tot", Witnesses?.Count ?? 0, false);

            if (Witnesses?.Count > 0)
                builder.AddValues("witness", from w in Witnesses select w.Id);

            return builder.Build(this);
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                new DataPinDefinition(DataPinValueType.Integer,
                   "tot-count",
                   "The total count of entries."),
                new DataPinDefinition(DataPinValueType.String,
                   "witness",
                   "The list of witnesses IDs.",
                   "M")
            });
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append("[AvailableWitnesses]");

            if (Witnesses?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Witnesses)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Witnesses.Count > 3)
                    sb.Append("...(").Append(Witnesses.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
