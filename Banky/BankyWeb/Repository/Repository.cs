using BankyWeb.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankyWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;
        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        //-------------GetAsync - method will hit API and get one info from DB,
        //-------------the request will work asynchronously and will return a async json response
        //-------------that will be converted to one obj data of generic T type
        public async Task<T> GetAsync(string url, int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url+Id); //constructing the request to be sent

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request); //request will be sent/processed asynchronously and await for response

            if (response.StatusCode == System.Net.HttpStatusCode.OK) //if status code is matched
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString); //the returned async response will be converted back to Ienumerable json to show
            }
            else
            {
                return null;
            }
        }


        //-------------GetAllAsync - method will hit API and get all info from DB,
        //-------------the request will work asynchronously and will return a async json response
        //-------------that will be converted to Ienumerable obj data of generic T type
        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url); //constructing the request to be sent

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request); //request will be sent/processed asynchronously and await for response

            if (response.StatusCode == System.Net.HttpStatusCode.OK) //if status code is matched
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString); //the returned async response will be converted back to Ienumerable json to show
            }
            else
            {
                return null;
            }
        }


        //-------------CreateAsync - method will get all info from view and send
        //-------------a json  data to the BANKYAPI Post method and wait for response asynchronously
        public async Task<bool> CreateAsync(string url, T objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url); //constructing the request to be sen

            if (objToCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json"); //convert the obj from view to json
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();//Clientfactory will Create a Client

            HttpResponseMessage response = await client.SendAsync(request); //request will be sent asynchronously and await for response

            if(response.StatusCode == System.Net.HttpStatusCode.Created) //if status code is 201-created
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //-------------DeleteAsync - same as createAsync
        public async Task<bool> DeleteAsync(string url, int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url+Id); //constructing the request to be sent

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request); //request will be sent/processed synchronously and await for response

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) //if status code is 204-No content to return as everything is successfully done
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //-------------UpdateAsync - same as createAsync
        public async Task<bool> UpdateAsync(string url, T objToUpdate)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url); //constructing the request to be sen

            if (objToUpdate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToUpdate), Encoding.UTF8, "application/json"); //convert the obj from view to json
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();//Clientfactory will Create a Client

            HttpResponseMessage response = await client.SendAsync(request); //request will be sent asynchronously and await for response

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) //if status code is 204-NoContent
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
