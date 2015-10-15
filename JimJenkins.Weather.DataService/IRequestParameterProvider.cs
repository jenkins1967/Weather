using System.Collections.Generic;
using System.Reflection;
using System.Text;
using JimJenkins.Weather.WeatherGov.DataService.Attributes;

namespace JimJenkins.Weather.WeatherGov.DataService
{
    public interface IRequestElementsProvider
    {
        bool MaximumTemperature { get; }	//maxt
        bool MinimumTemperature { get; }	//mint
        bool ThreeHourlyTemperature { get; }	//temp
        bool DewpointTemperature { get; }	//dew
        bool ApparentTemperature { get; } //	appt
        bool TwelveHourProbabilityPrecipitation { get; }	//pop12
        bool LiquidPrecipitationAmount { get; }	//qpf
        bool SnowfallAmount { get; }	//snow
        bool CloudCoverAmount { get; }	//sky
        bool RelativeHumidity { get; }	//rh
        bool WindSpeed { get; }//wspd
        bool WindDirection { get; }	//wdir
        bool Weather { get; }//wx
        bool WeatherIcons { get; }//	icons
        bool WaveHeight { get; }	//waveh
        bool ProbabilisticTropicalCycloneWindSpeedAbove34Knots { get; } //cumw34
        bool ProbabilisticTropicalCycloneWindSpeedAbove50Knots { get; } //	cumw50
        bool ProbabilisticTropicalCycloneWindSpeedAbove64Knots { get; } //cumw64
        bool WindGust { get; }	//wgust
        bool FireWeatherfromWind { get; } //	critfireo
        bool FireWeatherfromDry { get; } //dryfireo
        bool ProbabilityofTornadoes { get; }	//ptornado
        bool ProbabilityofHail { get; }	//phail
        bool ProbabilityofDamagingThunderstormWinds { get; } //	ptstmwinds
        bool ProbabilityofExtremeTornadoes { get; } //	pxtornado
        bool ProbabilityofExtremeHail { get; }	//pxhail
        bool ProbabilityofExtremeThunderstormWinds { get; }	//pxtstmwinds
        bool ProbabilityofSevereThunderstorms { get; }	//ptotsvrtstm
        bool ProbabilityofExtremeSevereThunderstorms { get; }	//pxtotsvrtstm
        //Probability of 8- To 14-Day Average Temperature Above Normal	tmpabv14d
        //Probability of 8- To 14-Day Average Temperature Below Normal	tmpblw14d
        //Probability of One-Month Average Temperature Above Normal	tmpabv30d
        //Probability of One-Month Average Temperature Below Normal	tmpblw30d
        //Probability of Three-Month Average Temperature Above Normal	tmpabv90d
        //Probability of Three-Month Average Temperature Below Normal	tmpblw90d
        //Probability of 8- To 14-Day Total Precipitation Above Median	prcpabv14d
        //Probability of 8- To 14-Day Total Precipitation Below Median	prcpblw14d
        //Probability of One-Month Total Precipitation Above Median	prcpabv30d
        //Probability of One-Month Total Precipitation Below Median	prcpblw30d
        //Probability of Three-Month Total Precipitation Above Median	prcpabv90d
        //Probability of Three-Month Total Precipitation Below Median	prcpblw90d
        //Real-time Mesoscale Analysis Precipitation	precipa_r
        //Real-time Mesoscale Analysis GOES Effective Cloud Amount	sky_r
        //Real-time Mesoscale Analysis Dewpoint Temperature	td_r
        //Real-time Mesoscale Analysis Temperature	temp_r
        //Real-time Mesoscale Analysis Wind Direction	wdir_r
        //Real-time Mesoscale Analysis Wind Speed	wspd_r
        bool WatchesWarningsandAdvisories { get; }//	wwa
        bool IceAccumulation { get; }	//iceaccum
        //Maximum Relative Humidity	maxrh
        //Minimum Relative Humidity

        IList<string> Build();
    }

    public class RequestElementsProvider : IRequestElementsProvider
    {
        [QueryParam("maxt")]
        public bool MaximumTemperature { get; set; }
        [QueryParam("mint")]
        public bool MinimumTemperature { get; set; }
        [QueryParam("temp")]
        public bool ThreeHourlyTemperature { get; set; }
        [QueryParam("dew")]
        public bool DewpointTemperature { get; set; }
        [QueryParam("appt")]
        public bool ApparentTemperature { get; set; }
        [QueryParam("pop12")]
        public bool TwelveHourProbabilityPrecipitation { get; set; }
        [QueryParam("qpf")]
        public bool LiquidPrecipitationAmount { get; set; }
        [QueryParam("snow")]
        public bool SnowfallAmount { get; set; }
        [QueryParam("sky")]
        public bool CloudCoverAmount { get; set; }
        [QueryParam("rh")]
        public bool RelativeHumidity { get; set; }
        [QueryParam("wspd")]
        public bool WindSpeed { get; set; }
        [QueryParam("wdir")]
        public bool WindDirection { get; set; }
        [QueryParam("wx")]
        public bool Weather { get; set; }
        [QueryParam("icons")]
        public bool WeatherIcons { get; set; }
        [QueryParam("waveh")]
        public bool WaveHeight { get; set; }

        [QueryParam("cumw34")]
        public bool ProbabilisticTropicalCycloneWindSpeedAbove34Knots { get; set; }

        [QueryParam("cumw50")]
        public bool ProbabilisticTropicalCycloneWindSpeedAbove50Knots { get; set; }

        [QueryParam("cumw64")]
        public bool ProbabilisticTropicalCycloneWindSpeedAbove64Knots { get; set; }

        [QueryParam("wgust")]
        public bool WindGust { get; set; }

        [QueryParam("critfireo")]
        public bool FireWeatherfromWind { get; set; }

        [QueryParam("dryfireo")]
        public bool FireWeatherfromDry { get; set; }
        [QueryParam("ptornado")]
        public bool ProbabilityofTornadoes { get; set; }
        [QueryParam("phail")]
        public bool ProbabilityofHail { get; set; }
        [QueryParam("ptstmwinds")]
        public bool ProbabilityofDamagingThunderstormWinds { get; set; }
        [QueryParam("pxtornado")]
        public bool ProbabilityofExtremeTornadoes { get; set; }
        [QueryParam("pxhail")]
        public bool ProbabilityofExtremeHail { get; set; }
        [QueryParam("pxtstmwinds")]
        public bool ProbabilityofExtremeThunderstormWinds { get; set; }
        [QueryParam("ptotsvrtstm")]
        public bool ProbabilityofSevereThunderstorms { get; set; }
        [QueryParam("pxtotsvrtstm")]
        public bool ProbabilityofExtremeSevereThunderstorms { get; set; }
        [QueryParam("wwa")]
        public bool WatchesWarningsandAdvisories { get; set; }
        [QueryParam("iceaccum")]
        public bool IceAccumulation { get; set; }
        public IList<string> Build()
        {
            var paramCollection = new List<string>();
            var props = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof (bool) && (bool)prop.GetValue(this))
                {
                    var queryParam = prop.GetCustomAttribute<QueryParamAttribute>();
                    paramCollection.Add(queryParam.Parameter);
                }
            }
            return paramCollection;
        }

        public static IRequestElementsProvider AllElements
        {
            get
            {
                var provider = new RequestElementsProvider();
                provider.ApparentTemperature = true;
                provider.CloudCoverAmount = true;
                provider.DewpointTemperature = true;
                provider.FireWeatherfromDry = true;
                provider.FireWeatherfromWind = true;
                provider.IceAccumulation = true;
                provider.LiquidPrecipitationAmount = true;
                provider.MaximumTemperature = true;
                provider.MinimumTemperature = true;
                //provider.ProbabilisticTropicalCycloneWindSpeedAbove34Knots = true;
                //provider.ProbabilisticTropicalCycloneWindSpeedAbove50Knots = true;
                //provider.ProbabilisticTropicalCycloneWindSpeedAbove64Knots = true;
                //provider.ProbabilityofDamagingThunderstormWinds = true;
                provider.ProbabilityofHail = true;
                //provider.ProbabilityofExtremeSevereThunderstorms = true;
                //provider.ProbabilityofExtremeThunderstormWinds = true;
                provider.ProbabilityofTornadoes = true;
                provider.ProbabilityofSevereThunderstorms = true;
                provider.RelativeHumidity = true;
                provider.SnowfallAmount = true;
                provider.TwelveHourProbabilityPrecipitation = true;
                provider.ThreeHourlyTemperature = true;
                provider.WatchesWarningsandAdvisories = true;
                provider.WaveHeight = true;
                provider.Weather = true;
                provider.WindGust = true;
                provider.WindSpeed = true;
                return provider;
            }
        }
    }
}