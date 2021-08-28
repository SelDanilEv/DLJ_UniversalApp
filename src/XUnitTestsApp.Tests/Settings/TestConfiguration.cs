using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestsApp.Tests
{
    public static class TestConfiguration
    {
        private static IConfiguration configuration = InitConfiguration();

        public static IConfiguration InitConfiguration()
        {
            var task = Task.Run(() => {
                using (var file = File.OpenText("Properties\\launchSettings.json"))
                {
                    var reader = new JsonTextReader(file);
                    var jObject = JObject.Load(reader);

                    var variables = jObject
                        .GetValue("profiles")
                        .SelectMany(profiles => profiles.Children())
                        .SelectMany(profile => profile.Children<JProperty>())
                        .Where(prop => prop.Name == "environmentVariables")
                        .SelectMany(prop => prop.Value.Children<JProperty>())
                        .ToList();

                    foreach (var variable in variables)
                    {
                        Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                    }
                }
            });
            task.Wait();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
                .Build();
            return config;
        }

        public static IOptions<MongoOptions> GetMongoOption()
        {
            var section = configuration.GetSection(MongoOptions.Name);

            var opt = Options.Create(section.Get<MongoOptions>());

            return opt;
        }
    }
}
