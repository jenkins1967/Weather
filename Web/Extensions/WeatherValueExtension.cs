using System;
using System.Linq;
using JimJenkins.Weather.WeatherGov.Entities;

namespace Web.Extensions
{
    public static class WeatherValueExtension
    {
        public static WeatherValue<Int32> ToInt32(this WeatherValue<float> data)
        {
            var newVal = new WeatherValue<Int32>();
            MapData(data, newVal);
            newVal.Values = data.Values.ToList().ConvertAll(Convert.ToInt32).ToArray();

            return newVal;
        }
        public static WeatherValue<Int32?> ToInt32(this WeatherValue<float?> data)
        {
            var newVal = new WeatherValue<Int32?>();
            MapData(data, newVal);
            newVal.Values = data.Values.ToList().ConvertAll(v => new Int32?(Convert.ToInt32(v))).ToArray();
            
            return newVal;
        }

        public static void MapData<TFromType, TToType>(WeatherValue<TFromType> from, WeatherValue<TToType> to)
        {
                to.Name = from.Name ;
                to.TimeLayoutKey = from.TimeLayoutKey ;
                to.Type = from.Type;
                to.Units = from.Units;
        }
    }
}