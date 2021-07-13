using Newtonsoft.Json;
using NYCMappingWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace NYCMappingWebApp.DataAccessLayer
{
    public class RestDAL
    {public static EvictionOutput InvokeRequestResponseEvictionStatus15KProperties(string BBL, string Borough, string ZipCode, string BldgArea, string ComArea, string ResArea, string NumFloors, string UnitsRes, string ZoneDist1, string AssessTot, string YearBuilt, string BldgClass, string LandUse, string EVICTION_STATUS)
        {
            EvictionOutput returnResult = new EvictionOutput();
            var serializer = new JavaScriptSerializer();
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"BBL", "Borough", "ZipCode", "BldgArea", "ComArea", "ResArea", "NumFloors", "UnitsRes", "ZoneDist1", "AssessTot", "YearBuilt", "BldgClass", "LandUse", "EVICTION_STATUS"},
                                Values = new string[,] {  { BBL, Borough, ZipCode, BldgArea, ComArea, ResArea, NumFloors, UnitsRes, ZoneDist1, AssessTot, YearBuilt, BldgClass, LandUse, EVICTION_STATUS },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "1zaLEbeAymcIq6ddlmE9JJ/aQXOS4bAYSsEnJRjcm+OougOd2jvak/0wTvvHY/flaE1CPk2tj1cvI/Fh8rmUcw=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/c6a68ac157504f7cb2a77dec84227860/services/39c0d44c1b8e4980a7dd0a61f1001459/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                var requestString = JsonConvert.SerializeObject(scoreRequest);
                var content = new StringContent(requestString);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = client.PostAsync("", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        var lstResults = result.Split('"');
                        if (lstResults.Count() == 105)
                        {
                            returnResult.Scored_Labels = lstResults[101];
                            returnResult.Scored_Probabilities = lstResults[103];
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    //Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    //// Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    //Console.WriteLine(response.Headers.ToString());

                    //string responseContent = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseContent);
                }
            }
            return returnResult;
        }
    }
}