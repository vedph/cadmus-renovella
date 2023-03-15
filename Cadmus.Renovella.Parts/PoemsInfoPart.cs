using Cadmus.Core;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Renovella.Parts;

/// <summary>
/// Poems info part.
/// <para>Tag: <c>it.vedph.renovella.poems-info</c>.</para>
/// </summary>
/// <seealso cref="PartBase" />
[Tag("it.vedph.renovella.poems-info")]
public sealed class PoemsInfoPart : PartBase
{
    /// <summary>
    /// Gets or sets the entries.
    /// </summary>
    public List<PoemInfo> Poems { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PoemsInfoPart"/> class.
    /// </summary>
    public PoemsInfoPart()
    {
        Poems = new List<PoemInfo>();
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins with
    /// these keys: ....</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        builder.Set("tot", Poems?.Count ?? 0, false);

        if (Poems?.Count > 0)
        {
            foreach (PoemInfo poem in Poems)
                builder.AddValue("metre", poem.Metre);
        }

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
               "The total count of poems."),
            new DataPinDefinition(DataPinValueType.String,
               "metre",
               "The poems metres.",
               "M"),
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

        sb.Append("[PoemsInfo]");

        if (Poems?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (var entry in Poems)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (Poems.Count > 3)
                sb.Append("...(").Append(Poems.Count).Append(')');
        }

        return sb.ToString();
    }
}
