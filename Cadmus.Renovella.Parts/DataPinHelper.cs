using Cadmus.Core;

namespace Cadmus.Renovella.Parts;

internal static class DataPinHelper
{
    private static IDataPinTextFilter? _filter;

    public static IDataPinTextFilter Filter
    {
        get
        {
            return _filter ??= new StandardDataPinTextFilter();
        }
    }
}
