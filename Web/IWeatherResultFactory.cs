using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using JimJenkins.Weather.WeatherGov.Entities;
using Web.Models;

namespace Web
{
    public interface IWeatherResultFactory
    {
        WeatherRequestResult Build(WeatherData data);
    }

    public class WeatherResultFactory : IWeatherResultFactory
    {
        public WeatherRequestResult Build(WeatherData data)
        {
            var source = data.Head.Source;
            var result = new WeatherRequestResult();
            result.CreditUrl = source.Credit;
            result.CreditLogUrl = source.CreditLogo;
            result.CreatedOnUtc = data.Head.Product.CreationDate;

            result.Temperatures = ProcessTemperatures(data);
            result.Precipitation = ProcessPrecipitations(data);
            result.PrecipitationProbability = ProcessPrecipProbabilities(data);
            result.Weather = ProcessWeatherText(data);

         //   result.Weather = ProcessWeathers(data);

            return result;
        }

        private WeatherText ProcessWeatherText(WeatherData data)
        {
            var values = data.Data.Parameters.First().Weathers;
            var timeLayout = data.Data.TimeLayouts.FirstOrDefault(x => x.Key.Equals(values[0].TimeLayoutKey));

            var list = new List<WeatherData<string>>();
            if (values != null && timeLayout != null)
            {
                    for (var index = 0; index < values.Length; index++)
                    {
                        var sb = new StringBuilder();
                        foreach (var condition in values[0].WeatherConditions)
                        {
                            //condition.Values[0].Coverage.ToInitialCap();
                        }
                        var startTime = timeLayout.StartTimes[index];
                        var endTime = new DateTime?();
                        if (timeLayout.EndTimes != null)
                        {
                            endTime = timeLayout.EndTimes[index];
                        }
                        list.Add(new WeatherData<int>
                        {
                            Value = (int)data.Values[index],
                            Start = startTime,
                            End = endTime
                        });
                    }
                
            }
            return list;
        }

        private WeatherDataPrecipitationProbability ProcessPrecipProbabilities(WeatherData data)
        {
            var values = data.Data.Parameters.First().ProbabilityOfPrecipitations;
            var container = new WeatherDataPrecipitationProbability();
            container.Percents = ProcessValues(values.FirstOrDefault(t => t.Type.Equals("12 hour")),
                data.Data.TimeLayouts);
            return container;
        }

        private WeatherDataPrecipitation ProcessPrecipitations(WeatherData data)
        {
            var values = data.Data.Parameters.First().Temperatures;
            var snow = values.FirstOrDefault(t => t.Type.Equals("snow"));
            var ice = values.FirstOrDefault(t => t.Type.Equals("ice"));
            var liquid = values.FirstOrDefault(t => t.Type.Equals("liquid"));
           

            var precipData = new WeatherDataPrecipitation();
            precipData.Snow = ProcessValues(snow, data.Data.TimeLayouts);
            precipData.Ice = ProcessValues(ice, data.Data.TimeLayouts);
            precipData.Liquid = ProcessValues(liquid, data.Data.TimeLayouts);
            
            return precipData;
        }
        private WeatherDataTemperature ProcessTemperatures (WeatherData data)
        {
            var values = data.Data.Parameters.First().Precipitations;
            var apparentTemps = values.FirstOrDefault(t => t.Type.Equals("apparent"));
            var maxTemps = values.FirstOrDefault(t => t.Type.Equals("maximum"));
            var minTemps = values.FirstOrDefault(t => t.Type.Equals("minimum"));
            var hourly = values.FirstOrDefault(t => t.Type.Equals("hourly"));
            var dewPoint = values.FirstOrDefault(t => t.Type.Equals("dew point"));

            var tempData = new WeatherDataTemperature();
            tempData.DailyMaximum = ProcessValues(maxTemps, data.Data.TimeLayouts);
            tempData.DailyMinimum = ProcessValues(minTemps, data.Data.TimeLayouts);
            tempData.Apparent = ProcessValues(apparentTemps, data.Data.TimeLayouts);
            tempData.DewPoint = ProcessValues(dewPoint, data.Data.TimeLayouts);
            tempData.Hourly = ProcessValues(hourly, data.Data.TimeLayouts);

            return tempData;
        }

        private IEnumerable<WeatherData<Int32>> ProcessValues(WeatherValue<float> data, IEnumerable<TimeLayout> timeLayouts)
        {
            var list = new List<WeatherData<Int32>>();
            if (data != null)
            {
                var timeLayout = timeLayouts.FirstOrDefault(t => t.Key.Equals(data.TimeLayoutKey));
                if (timeLayout != null)
                {
                    for (var index = 0; index < data.Values.Length; index++)
                    {
                        var startTime = timeLayout.StartTimes[index];
                        var endTime = new DateTime?();
                        if (timeLayout.EndTimes != null)
                        {
                            endTime = timeLayout.EndTimes[index];
                        }
                        list.Add(new WeatherData<int>
                        {
                            Value = (int)data.Values[index],
                            Start = startTime,
                            End = endTime
                        });
                    }
                }
            }
            return list;
        }

     

    }

}
