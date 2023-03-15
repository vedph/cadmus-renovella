using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.General.Parts;
using Cadmus.Seed.General.Parts;
using Fusi.Microsoft.Extensions.Configuration.InMemoryJson;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Cadmus.Seed.Renovella.Parts.Test;

static internal class TestHelper
{
    private static readonly JsonSerializerOptions _options =
        new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

    static public Stream GetResourceStream(string name)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        return Assembly.GetExecutingAssembly().GetManifestResourceStream(
                $"Cadmus.Seed.Renovella.Parts.Test.Assets.{name}")!;
    }

    static public string LoadResourceText(string name)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        using (StreamReader reader = new StreamReader(
            GetResourceStream(name),
            Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
    }

    private static IHost GetHost(string config)
    {
        // map
        TagAttributeToTypeMap map = new();
        map.Add(new[]
        {
            // TODO: your parts assemblies here
            // Cadmus.Core
            typeof(StandardItemSortKeyBuilder).Assembly,
            // Cadmus.General.Parts
            typeof(NotePart).Assembly
        });

        return new HostBuilder().ConfigureServices((hostContext, services) =>
        {
            PartSeederFactory.ConfigureServices(services,
                new StandardPartTypeProvider(map),
                    typeof(NotePartSeeder).Assembly);
            })
            // extension method from Fusi library
            .AddInMemoryJson(config)
            .Build();
    }

    static public PartSeederFactory GetFactory()
    {
        return new PartSeederFactory(GetHost(LoadResourceText("SeedConfig.json")));
    }

    static public void AssertPartMetadata(IPart part)
    {
        Assert.NotNull(part.Id);
        Assert.NotNull(part.ItemId);
        Assert.NotNull(part.UserId);
        Assert.NotNull(part.CreatorId);
    }

    public static string SerializePart(IPart part)
    {
        if (part == null)
            throw new ArgumentNullException(nameof(part));

        return JsonSerializer.Serialize(part, part.GetType(), _options);
    }

    public static T? DeserializePart<T>(string json)
        where T : class, IPart, new()
    {
        if (json == null)
            throw new ArgumentNullException(nameof(json));

        return JsonSerializer.Deserialize<T>(json, _options);
    }

    public static void AssertPinIds(IPart part, DataPin pin)
    {
        Assert.Equal(part.ItemId, pin.ItemId);
        Assert.Equal(part.Id, pin.PartId);
        Assert.Equal(part.RoleId, pin.RoleId);
    }
}
