using Newtonsoft.Json.Converters;

namespace Ecocell.Communication.Util;

public class CustomDateTimeConverter : IsoDateTimeConverter
{
    public CustomDateTimeConverter()
    {
        DateTimeFormat = "yyyy-MM-dd";
    }
}