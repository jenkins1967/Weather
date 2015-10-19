using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using JimJenkins.Weather.WeatherGov.Entities;
using Web.Extensions;
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
            var result = new WeatherRequestResult
            {
                CreditUrl = source.Credit,
                CreditLogUrl = source.CreditLogo,
                CreatedOnUtc = data.Head.Product.CreationDate,
                Temperatures = ProcessTemperatures(data),
                Precipitation = ProcessPrecipitations(data),
                PrecipitationProbability = ProcessPrecipProbabilities(data),
                Wind = ProcessWind(data),
                CloudCover = ProcessCloudCover(data)
            };

            //  result.Weather = ProcessWeatherText(data);

         //   result.Weather = ProcessWeathers(data);

            return result;
        }

        //private WeatherText ProcessWeatherText(WeatherData data)
        //{
        //    var values = data.Data.Parameters.First().Weathers;
        //    var timeLayout = data.Data.TimeLayouts.FirstOrDefault(x => x.Key.Equals(values[0].TimeLayoutKey));

        //    var list = new List<WeatherData<string>>();
        //    if (values != null && timeLayout != null)
        //    {
        //            for (var index = 0; index < values.Length; index++)
        //            {
        //                var sb = new StringBuilder();
        //                foreach (var condition in values[0].WeatherConditions)
        //                {
        //                    //condition.Values[0].Coverage.ToInitialCap();
        //                }
        //                var startTime = timeLayout.StartTimes[index];
        //                var endTime = new DateTime?();
        //                if (timeLayout.EndTimes != null)
        //                {
        //                    endTime = timeLayout.EndTimes[index];
        //                }
        //                list.Add(new WeatherData<int>
        //                {
        //                    Value = (int)data.Values[index],
        //                    Start = startTime,
        //                    End = endTime
        //                });
        //            }
                
        //    }
        //    return list;
        //}

        private WeatherDataCloudCover ProcessCloudCover(WeatherData data)
        {
            var parameters = data.Data.Parameters.First();
            var coverage = parameters.CloudAmounts.FirstOrDefault();
            return new WeatherDataCloudCover {Percent = ProcessValues(coverage, data.Data.TimeLayouts)};
        }

        private WeatherDataWind ProcessWind(WeatherData data)
        {
            var parameters = data.Data.Parameters.First();
            var speeds = parameters.WindSpeeds;
            var directions = parameters.WindDirections.FirstOrDefault();
            var gusts = speeds.FirstOrDefault(x => x.Type.Equals("gust"));
            var sustained = speeds.FirstOrDefault(x => x.Type.Equals("sustained"));

            var windData = new WeatherDataWind();
            windData.Direction = ProcessValues(directions, data.Data.TimeLayouts);
            windData.Gusts = ProcessValues(gusts, data.Data.TimeLayouts);
            windData.Speed = ProcessValues(sustained, data.Data.TimeLayouts);

            return windData;

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
            var values = data.Data.Parameters.First().Precipitations;
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
            var values = data.Data.Parameters.First().Temperatures;
            var apparentTemps = values.FirstOrDefault(t => t.Type.Equals("apparent"));
            var maxTemps = values.FirstOrDefault(t => t.Type.Equals("maximum"));
            var minTemps = values.FirstOrDefault(t => t.Type.Equals("minimum"));
            var hourly = values.FirstOrDefault(t => t.Type.Equals("hourly"));
            var dewPoint = values.FirstOrDefault(t => t.Type.Equals("dew point"));

            var tempData = new WeatherDataTemperature
            {
                DailyMaximum = ProcessValues(maxTemps, data.Data.TimeLayouts),
                DailyMinimum = ProcessValues(minTemps, data.Data.TimeLayouts),
                Apparent = ProcessValues(apparentTemps, data.Data.TimeLayouts),
                DewPoint = ProcessValues(dewPoint, data.Data.TimeLayouts),
                Hourly = ProcessValues(hourly, data.Data.TimeLayouts)
            };

            return tempData;
        }

        private IEnumerable<WeatherData<Int32?>> ProcessValues(WeatherValue<float?> data,
            IEnumerable<TimeLayout> timeLayouts)
        {
            var newVal = data.ToInt32();
            return ProcessValues(newVal, timeLayouts);
        }

        private IEnumerable<WeatherData<Int32>> ProcessValues(WeatherValue<float> data,
            IEnumerable<TimeLayout> timeLayouts)
        {
            var newVal = data.ToInt32();
            return ProcessValues(newVal, timeLayouts);
        }

        private IEnumerable<WeatherData<Int32?>> ProcessValues(WeatherValue<Int32?> data, IEnumerable<TimeLayout> timeLayouts)
        {
            var list = new List<WeatherData<Int32?>>();
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
                        list.Add(new WeatherData<Int32?>
                        {
                            Value = data.Values[index],
                            Start = startTime,
                            End = endTime
                        });
                    }
                }
            }
            return list;
        }

        private IEnumerable<WeatherData<Int32>> ProcessValues(WeatherValue<Int32> data, IEnumerable<TimeLayout> timeLayouts)
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
                        list.Add(new WeatherData<Int32>
                        {
                            Value = data.Values[index],
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
