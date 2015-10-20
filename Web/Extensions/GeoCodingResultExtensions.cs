using System.Text;
using JimJenkins.GeoCoding.Services;

namespace Web.Extensions
{
    public static class GeoCodingResultExtensions
    {
        public static string GetDescription(this GeoCodingResult result)
        {
            var sb = new StringBuilder();
            if(!string.IsNullOrEmpty(result.City))
            {
                sb.Append(result.City);
                if (!string.IsNullOrEmpty(result.State))
                {
                    sb.Append(", ");
                }
            }

            sb.Append(result.State);

            return sb.ToString();

        }

        
    }
}