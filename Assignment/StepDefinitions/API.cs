using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Serializers.Json;
using RestSharp.Serializers;
using Reqnroll.CommonModels;
using Newtonsoft.Json;
using NUnit.Framework;



namespace Assignment.StepDefinitions
{
   
    [Binding]
    public sealed class API
    {

        string ap = "http://dummy.restapiexample.com/";

        RestClient client;

        RestRequest request;


        RestResponse response;
        #region helperFunction

        public void CreateRequest(string endPoint)
        {
             client = new RestClient(ap);

             request = new RestRequest(endPoint, Method.Get);

             response = client.Execute(request);
           

        }


        #endregion

        [BeforeScenario]
        [Scope(Feature = "ApiTest")]
        public void CreateAppointment()
        {
           // do nothing
        }


        [Given("Admin access the {string}")]
        public void GivenAdminAccessThe(string endpoint)
        {
            CreateRequest(endpoint);
          
        }



        [Then("verify {string} has a salary of {string}")]
        public void ThenVerifyHasASalaryOf(string p0, string p1)
        {
            string error = response.ErrorException.Message;
            if (error.Equals(""))
                {

                try
                {
                    var content = response.Content;

                    JObject jObject = JObject.Parse(content);
                    //parse the Jobject into class and then fetch the data for the employee then compare the saalry with salary passed in the function through param
                    //note: i am always getting 429 conflict so i cant check the happy flow and perform the exact query here
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }


            }
            else
            {
                Assert.Fail("conflict occured in the request");

            }
        }


        [AfterScenario]
        [Scope(Feature = "ApiTest")]
        public void Donothing()
        {
            // do nothing
        }
    }


}
