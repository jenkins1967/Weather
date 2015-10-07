using System;

namespace JimJenkins.Weather.WeatherGov.DataService.Attributes
{
    internal class QueryParamAttribute:Attribute
    {
        public QueryParamAttribute(string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                throw new ArgumentException("param");
            }
            Parameter = param;
        }

        public string Parameter { get; private set; }
    }
}
