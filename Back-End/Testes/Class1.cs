using System;
using api.Models;
using api.Repository;
using api.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace api.Testes
{
    public class Class1  
    {
       private readonly HttpClient _client;
       public Class1()
       {
           var server  = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            _client = server.CreateClient();
       }

        [Theory]
        [InlineData("GET")]
        public ActionResult<RetornoView<Motorista>> MotoristaGetAllTest(string method)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api");

            //Act
            var response = _client.SendAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
