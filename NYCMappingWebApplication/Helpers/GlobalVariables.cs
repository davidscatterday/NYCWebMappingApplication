using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCMappingWebApp.Helpers
{
    public static class GlobalVariables
    {
        public const string InitPassword = "Password12345?";
        public const string AdminRoleName = "Admin";
        public const int ViewerRoleID = 2;
        public const string ConsumerProfilesVariables = "DP05_0001E,DP05_0002E,DP05_0002PE,DP05_0003E,DP05_0003PE,DP05_0004E,DP05_0009E,DP05_0010E,DP05_0011E,DP05_0012E,DP05_0013E,DP05_0014E,DP05_0015E,DP05_0016E,DP05_0036E,DP05_0037E,DP05_0038E,DP05_0039E,DP05_0044E,DP05_0045E,DP05_0046E,DP05_0047E,DP05_0048E,DP05_0049E,DP05_0050E,DP05_0052E,DP05_0071E" +
            ",DP02_0001E,DP02_0001PE,DP02_0002E,DP02_0002PE,DP02_0003E,DP02_0003PE,DP02_0006E,DP02_0006PE,DP02_0008E,DP02_0008PE,DP02_0010E,DP02_0010PE,DP02_0011E,DP02_0011PE,DP02_0012E,DP02_0012PE,DP02_0024E,DP02_0024PE,DP02_0025E,DP02_0025PE,DP02_0026E,DP02_0026PE,DP02_0027E,DP02_0027PE,DP02_0028E,DP02_0028PE,DP02_0029E,DP02_0029PE,DP02_0030E,DP02_0030PE,DP02_0031E,DP02_0031PE,DP02_0032E,DP02_0032PE,DP02_0033E,DP02_0033PE,DP02_0034E,DP02_0034PE,DP02_0035E,DP02_0035PE,DP02_0052E,DP02_0052PE,DP02_0053E,DP02_0053PE,DP02_0054E,DP02_0054PE,DP02_0055E,DP02_0055PE,DP02_0056E,DP02_0056PE,DP02_0057E,DP02_0057PE,DP02_0058E,DP02_0058PE,DP02_0059E,DP02_0059PE,DP02_0060E,DP02_0060PE,DP02_0061E,DP02_0061PE,DP02_0062E,DP02_0062PE,DP02_0063E,DP02_0063PE,DP02_0064E,DP02_0064PE,DP02_0070E,DP02_0070PE,DP02_0071E,DP02_0071PE,DP02_0074E,DP02_0074PE,DP02_0075E,DP02_0075PE,DP02_0076E,DP02_0076PE,DP02_0078E,DP02_0078PE,DP02_0079E,DP02_0079PE,DP02_0080E,DP02_0080PE,DP02_0081E,DP02_0081PE,DP02_0082E,DP02_0082PE,DP02_0083E,DP02_0083PE,DP02_0084E,DP02_0084PE,DP02_0150E,DP02_0150PE,DP02_0151E,DP02_0151PE,DP02_0152E,DP02_0152PE" +
            ",DP03_0001E,DP03_0001PE,DP03_0003E,DP03_0003PE,DP03_0004E,DP03_0004PE,DP03_0005E,DP03_0005PE,DP03_0007E,DP03_0007PE,DP03_0009E,DP03_0009PE,DP03_0010E,DP03_0010PE,DP03_0012E,DP03_0012PE,DP03_0013E,DP03_0013PE,DP03_0051E,DP03_0051PE,DP03_0052E,DP03_0052PE,DP03_0053E,DP03_0053PE,DP03_0054E,DP03_0054PE,DP03_0055E,DP03_0055PE,DP03_0056E,DP03_0056PE,DP03_0057E,DP03_0057PE,DP03_0058E,DP03_0058PE,DP03_0059E,DP03_0059PE,DP03_0060E,DP03_0060PE,DP03_0061E,DP03_0061PE,DP03_0063E,DP03_0063PE,DP03_0066E,DP03_0066PE,DP03_0068E,DP03_0068PE,DP03_0069E,DP03_0069PE,DP03_0070E,DP03_0070PE,DP03_0071E,DP03_0071PE,DP03_0072E,DP03_0072PE,DP03_0073E,DP03_0073PE,DP03_0095E,DP03_0095PE,DP03_0096E,DP03_0096PE,DP03_0097E,DP03_0097PE,DP03_0098E,DP03_0098PE,DP03_0099E,DP03_0099PE,DP03_0102E,DP03_0102PE,DP03_0103E,DP03_0103PE,DP03_0104E,DP03_0104PE,DP03_0105E,DP03_0105PE,DP03_0106E,DP03_0106PE,DP03_0107E,DP03_0107PE,DP03_0108E,DP03_0108PE,DP03_0109E,DP03_0109PE,DP03_0110E,DP03_0110PE,DP03_0111E,DP03_0111PE,DP03_0112E,DP03_0112PE,DP03_0113E,DP03_0113PE,DP03_0133E,DP03_0133PE,DP03_0134E,DP03_0134PE,DP03_0135E,DP03_0135PE" +
            ",DP04_0006E,DP04_0006PE,DP04_0007E,DP04_0007PE,DP04_0008E,DP04_0008PE,DP04_0009E,DP04_0009PE,DP04_0010E,DP04_0010PE,DP04_0011E,DP04_0011PE,DP04_0012E,DP04_0012PE,DP04_0013E,DP04_0013PE,DP04_0027E,DP04_0027PE,DP04_0028E,DP04_0028PE,DP04_0029E,DP04_0029PE,DP04_0030E,DP04_0030PE,DP04_0031E,DP04_0031PE,DP04_0032E,DP04_0032PE,DP04_0033E,DP04_0033PE,DP04_0034E,DP04_0034PE,DP04_0035E,DP04_0035PE,DP04_0036E,DP04_0036PE,DP04_0037E,DP04_0037PE,DP04_0038E,DP04_0038PE,DP04_0039E,DP04_0039PE,DP04_0040E,DP04_0040PE,DP04_0041E,DP04_0041PE,DP04_0042E,DP04_0042PE,DP04_0043E,DP04_0043PE,DP04_0044E,DP04_0044PE,DP04_0045E,DP04_0045PE,DP04_0046E,DP04_0046PE,DP04_0047E,DP04_0047PE,DP04_0050E,DP04_0050PE,DP04_0051E,DP04_0051PE,DP04_0052E,DP04_0052PE,DP04_0053E,DP04_0053PE,DP04_0054E,DP04_0054PE,DP04_0055E,DP04_0055PE,DP04_0056E,DP04_0056PE,DP04_0076E,DP04_0076PE,DP04_0077E,DP04_0077PE,DP04_0078E,DP04_0078PE,DP04_0079E,DP04_0079PE,DP04_0090E,DP04_0090PE,DP04_0091E,DP04_0091PE,DP04_0092E,DP04_0092PE,DP04_0093E,DP04_0093PE,DP04_0094E,DP04_0094PE,DP04_0095E,DP04_0095PE,DP04_0096E,DP04_0096PE,DP04_0097E,DP04_0097PE,DP04_0098E,DP04_0098PE,DP04_0099E,DP04_0099PE,DP04_0100E,DP04_0100PE,DP04_0101E,DP04_0101PE,DP04_0110E,DP04_0110PE,DP04_0111E,DP04_0111PE,DP04_0112E,DP04_0112PE,DP04_0113E,DP04_0113PE,DP04_0114E,DP04_0114PE,DP04_0115E,DP04_0115PE,DP04_0126E,DP04_0126PE,DP04_0127E,DP04_0127PE,DP04_0128E,DP04_0128PE,DP04_0129E,DP04_0129PE,DP04_0130E,DP04_0130PE,DP04_0131E,DP04_0131PE,DP04_0132E,DP04_0132PE,DP04_0133E,DP04_0133PE,DP04_0134E,DP04_0134PE,DP04_0136E,DP04_0136PE,DP04_0137E,DP04_0137PE,DP04_0138E,DP04_0138PE,DP04_0139E,DP04_0139PE,DP04_0140E,DP04_0140PE,DP04_0141E,DP04_0141PE,DP04_0142E,DP04_0142PE";


        public static DateTime? ToNullableDateTime(string stringDate)
        {
            DateTime date;
            return DateTime.TryParse(stringDate, out date) ? date : (DateTime?)null;
        }

        public static int? ToNullableInt(string stringNumber)
        {
            int number;
            return int.TryParse(stringNumber, out number) ? number : (int?)null;
        }
        /// Stores a value in a user Cookie, creating it if it doesn't exists yet.
        /// </summary>
        public static void StoreInCookie(
            string cookieName,
            string cookieDomain,
            string keyName,
            string value,
            DateTime? expirationDate,
            bool httpOnly = false)
        {
            // NOTE: we have to look first in the response, and then in the request.
            // This is required when we update multiple keys inside the cookie.
            HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName]
                ?? HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null) cookie = new HttpCookie(cookieName);
            if (!String.IsNullOrEmpty(keyName)) cookie.Values.Set(keyName, value);
            else cookie.Value = value;
            if (expirationDate.HasValue) cookie.Expires = expirationDate.Value;
            if (!String.IsNullOrEmpty(cookieDomain)) cookie.Domain = cookieDomain;
            if (httpOnly) cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// Retrieves a single value from Request.Cookies
        /// </summary>
        public static string GetFromCookie(string cookieName, string keyName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(2);
                HttpContext.Current.Response.Cookies.Set(cookie);
                string val = (!String.IsNullOrEmpty(keyName)) ? cookie[keyName] : cookie.Value;
                if (!String.IsNullOrEmpty(val)) return Uri.UnescapeDataString(val);
            }
            return null;
        }

        /// <summary>
        /// Removes a single value from a cookie or the whole cookie (if keyName is null)
        /// </summary>
        public static void RemoveCookie(string cookieName, string keyName, string domain = null)
        {
            if (String.IsNullOrEmpty(keyName))
            {
                if (HttpContext.Current.Request.Cookies[cookieName] != null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                    cookie.Expires = DateTime.UtcNow.AddYears(-1);
                    if (!String.IsNullOrEmpty(domain)) cookie.Domain = domain;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Request.Cookies.Remove(cookieName);
                }
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                cookie.Values.Remove(keyName);
                if (!String.IsNullOrEmpty(domain)) cookie.Domain = domain;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// Checks if a cookie / key exists in the current HttpContext.
        /// </summary>
        public static bool CookieExist(string cookieName, string keyName)
        {
            HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
            return (String.IsNullOrEmpty(keyName))
                ? cookies[cookieName] != null
                : cookies[cookieName] != null && cookies[cookieName][keyName] != null;
        }

    }
}