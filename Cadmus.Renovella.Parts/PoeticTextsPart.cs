using Cadmus.Core;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Renovella.Parts;

/// <summary>
/// List of poetic texts linked to a tale or tales collection.
/// Tag: <c>it.vedph.renovella.poetic-texts</c>.
/// </summary>
[Tag("it.vedph.renovella.poetic-texts")]
public sealed class PoeticTextsPart : PartBase
{
    /// <summary>
    /// Gets or sets the texts.
    /// </summary>
    public List<PoeticText> Texts { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PoeticTextsPart"/> class.
    /// </summary>
    public PoeticTextsPart()
    {
        Texts = new List<PoeticText>();
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins with
    /// these keys: <c>incipit</c>, <c>metre</c>.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new(DataPinHelper.Filter);

        builder.Set("tot", Texts?.Count ?? 0, false);

        if (Texts?.Count > 0)
        {
            foreach (PoeticText text in Texts)
            {
                builder.AddValue("incipit", text.Incipit, filter: true);
                builder.AddValue("metre", text.Metre);
            }
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
               "The total count of texts."),
            new DataPinDefinition(DataPinValueType.String,
               "incipit",
               "The filtered incipit of each text.",
               "MF"),
            new DataPinDefinition(DataPinValueType.String,
               "metre",
               "The metre of each text.",
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

        sb.Append("[PoeticTexts]");

        if (Texts?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (var entry in Texts)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (Texts.Count > 3)
                sb.Append("...(").Append(Texts.Count).Append(')');
        }

        return sb.ToString();
    }
}

/// <summary>
/// Essential information about a poetic text in <see cref="PoeticTextsPart"/>.
/// </summary>
public class PoeticText
{
    /// <summary>
    /// Gets or sets the incipit.
    /// </summary>
    public string? Incipit { get; set; }

    /// <summary>
    /// Gets or sets the metre.
    /// </summary>
    public string? Metre { get; set; }

    /// <summary>
    /// Gets or sets an optional short note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        return Incipit ?? base.ToString()!;
    }
}
