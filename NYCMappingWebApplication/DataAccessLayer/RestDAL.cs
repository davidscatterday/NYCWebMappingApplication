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
    {
        public static async Task InvokeRequestResponseService()
        {
            var handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
            using (var client = new HttpClient(handler))
            {
                var scoreRequest = new Dictionary<string, List<Dictionary<string, string>>>()
                {
                    {
                        "data",
                        new List<Dictionary<string, string>>()
                        {
                            new Dictionary<string, string>()
                            {
                                {
                                    "BBL", "0"
                                },
                                {
                                    "Address", "example_value"
                                },
                                {
                                    "ZoneDist1", "example_value"
                                },
                                {
                                    "BldgClass", "example_value"
                                },
                                {
                                    "OwnerName", "example_value"
                                },
                                {
                                    "ResArea", "0"
                                },
                                {
                                    "NumFloors", "0"
                                },
                                {
                                    "UnitsRes", "0"
                                },
                                {
                                    "AssessTot", "0"
                                },
                                {
                                    "YearBuilt", "0"
                                },
                                {
                                    "EVICTION_ADDRESS", "example_value"
                                },
                                {
                                    "EVICTION_APT_NUM", "example_value"
                                },
                                {
                                    "EXECUTED_DATE", "2000-01-01 00:00:00,000000"
                                },
                                {
                                    "RESIDENTIAL_COMMERCIAL_IND", "example_value"
                                },
                                {
                                    "BOROUGH", "example_value"
                                },
                                {
                                    "EVICTION_ZIP", "0"
                                },
                            }
                        }
                    },
                };


                const string apiKey = "0m5z931c5UeURe3oUb7upRJizCeerrUU"; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("http://fa19e540-bf9d-4a20-96fe-a780ba24ba3f.eastus.azurecontainer.io/score");

                // WARNING: The 'await' statement below can result in a deadlock
                // if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false)
                // so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                var requestString = JsonConvert.SerializeObject(scoreRequest);
                var content = new StringContent(requestString);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync("", content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp,
                    // which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }

        public static async Task InvokeRequestResponseService1()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"BBL", "Address", "ZoneDist1", "BldgClass", "OwnerName", "ResArea", "NumFloors", "UnitsRes", "AssessTot", "YearBuilt", "EVICTION_ADDRESS", "EVICTION_APT_NUM", "EXECUTED_DATE", "RESIDENTIAL_COMMERCIAL_IND", "BOROUGH", "EVICTION_ZIP", "EVICTION_STATUS"},
                                //Values = new string[,] {  { "3035970006", "2253 STRAUSS STREET", "R6", "S1", "TELFORD, PEARLINE", "2000", "2", "1", "52320", "1930", "2253 STRAUSS    STRE ET A/K/A 47 NEWPORT STREET", "STOREFRONT", "2/9/2021", "Commercial", "BROOKLYN", "11212", "Y" },  }
                                Values = new string[,] {  { "5003390006", "375 MANOR ROAD", "R2", "K4", "375-389 MANOR ROAD LLC", "997", "2", "1", "137250", "1931", "375 MANOR ROAD", "NULL", "1/8/2020", "Residential", "STATEN ISLAND", "10314", "N" },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "BzU58e2W2sFoAdi7UjTuZIOyjFPId4EHPtYNSOvbhHqm1/bhbDYFmrshhYaJBWn0NXYv/I3njmQ/aeigEju5Bg=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/c6a68ac157504f7cb2a77dec84227860/services/15f3ebc42cfd47dd9928392ab8f479e8/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                var requestString = JsonConvert.SerializeObject(scoreRequest);
                var content = new StringContent(requestString);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync("", content).ConfigureAwait(false);
                //HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }

        //public static async Task<EvictionOutput> InvokeRequestResponseEvictionStatus15KProperties(string BBL, string Borough, string ZipCode, string BldgArea, string ComArea, string ResArea, string NumFloors, string UnitsRes, string ZoneDist1, string AssessTot, string YearBuilt, string BldgClass, string LandUse, string EVICTION_STATUS)
        //{
        //    EvictionOutput output = new EvictionOutput();
        //    using (var client = new HttpClient())
        //    {
        //        var scoreRequest = new
        //        {

        //            Inputs = new Dictionary<string, StringTable>() {
        //                {
        //                    "input1",
        //                    new StringTable()
        //                    {
        //                        ColumnNames = new string[] {"BBL", "Borough", "ZipCode", "BldgArea", "ComArea", "ResArea", "NumFloors", "UnitsRes", "ZoneDist1", "AssessTot", "YearBuilt", "BldgClass", "LandUse", "EVICTION_STATUS"},
        //                        Values = new string[,] {  { BBL, Borough, ZipCode, BldgArea, ComArea, ResArea, NumFloors, UnitsRes, ZoneDist1, AssessTot, YearBuilt, BldgClass, LandUse, EVICTION_STATUS },  }
        //                    }
        //                },
        //            },
        //            GlobalParameters = new Dictionary<string, string>()
        //            {
        //            }
        //        };
        //        const string apiKey = "1zaLEbeAymcIq6ddlmE9JJ/aQXOS4bAYSsEnJRjcm+OougOd2jvak/0wTvvHY/flaE1CPk2tj1cvI/Fh8rmUcw=="; // Replace this with the API key for the web service
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        //        client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/c6a68ac157504f7cb2a77dec84227860/services/39c0d44c1b8e4980a7dd0a61f1001459/execute?api-version=2.0&details=true");

        //        // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
        //        // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
        //        // For instance, replace code such as:
        //        //      result = await DoSomeTask()
        //        // with the following:
        //        //      result = await DoSomeTask().ConfigureAwait(false)
                
        //        var requestString = JsonConvert.SerializeObject(scoreRequest);
        //        var content = new StringContent(requestString);

        //        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //        HttpResponseMessage response = await client.PostAsync("", content).ConfigureAwait(false);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            try
        //            {
        //                string result = await response.Content.ReadAsStringAsync();
        //                var lstResults = result.Split('"');
        //                if (lstResults.Count() == 105)
        //                {
        //                    output.Scored_Labels= lstResults[101];
        //                    output.Scored_Probabilities = lstResults[103];
        //                }
        //                else
        //                {

        //                }
        //                Console.WriteLine("Result: {0}", result);
        //            }
        //            catch(Exception ex)
        //            {

        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

        //            // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
        //            Console.WriteLine(response.Headers.ToString());

        //            string responseContent = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine(responseContent);
        //        }
        //    }
        //    return output;
        //}
        public static EvictionOutput InvokeRequestResponseEvictionStatus15KProperties(string BBL, string Borough, string ZipCode, string BldgArea, string ComArea, string ResArea, string NumFloors, string UnitsRes, string ZoneDist1, string AssessTot, string YearBuilt, string BldgClass, string LandUse, string EVICTION_STATUS)
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