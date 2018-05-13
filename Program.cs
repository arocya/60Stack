using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace _60stack
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public static void Main(string[] args)
        {
            //GetSets().Wait();

            GetSetsStringAsync().Wait();
            GetSetsStreamAsync().Wait();

            BuildWebHost(args).Run();

            /*
             * Normally, you would prefer to await the completion of the task,
             * as in the ProcessRepositories method, but that isn't allowed in
             * the Main method.
             */
            //var repositories = ProcessListRepositories().Result;
            //foreach(var repo in repositories) Debug.WriteLine(repo.Name);


            try
            {
                WriteObject("DataContractExample.xml");
                ReadObject("DataContractExample.xml");
                Debug.WriteLine("Press Enter to end");
            }
            catch (SerializationException se)
            {
                Console.WriteLine
                ("The serialization operation failed. Reason: {0}",
                    se.Message);
                Debug.WriteLine(se.Data);
            }

        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            //var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            //var msg = await stringTask;
            //Console.Write(msg);

            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            var repositories = serializer.ReadObject(await streamTask) as List<Repository>;

            foreach(var repo in repositories) Console.WriteLine(repo.Name);
        }
        private static async Task<List<Repository>> ProcessListRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            var repositories = serializer.ReadObject(await streamTask) as List<Repository>;

            return repositories;
        }

        private static async Task GetSetsStringAsync()
        {
            var stringTask = client.GetStringAsync("https://api.pokemontcg.io/v1/sets");
            var msg = await stringTask;
            Debug.Write(msg);
        }

        private static async Task GetSetsStreamAsync()
        {
            var serializer = new DataContractJsonSerializer(typeof(Set));

            var streamTask = client.GetStreamAsync("https://api.pokemontcg.io/v1/sets");

            var sets = serializer.ReadObject(await streamTask) as List<SetData>;

            foreach(var set in sets)
                Debug.WriteLine(set.Name);
        }

        public static void WriteObject(string path)
        {
            // Create a new instance of the Person class and 
            // serialize it to an XML file.
            Person p1 = new Person("Mary", 1);
            // Create a new instance of a StreamWriter
            // to read and write the data.
            FileStream fs = new FileStream(path,
                FileMode.Create);
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
            DataContractSerializer ser =
                new DataContractSerializer(typeof(Person));
            ser.WriteObject(writer, p1);
            Console.WriteLine("Finished writing object.");
            writer.Close();
            fs.Close();
        }

        public static void ReadObject(string path)
        {
            // Deserialize an instance of the Person class 
            // from an XML file. First create an instance of the 
            // XmlDictionaryReader.
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

            // Create the DataContractSerializer instance.
            DataContractSerializer ser =
                new DataContractSerializer(typeof(Person));

            // Deserialize the data and read it from the instance.
            Person newPerson = (Person)ser.ReadObject(reader);
            Console.WriteLine("Reading this object:");
            Console.WriteLine(String.Format("{0}, ID: {1}",
                newPerson.Name, newPerson.ID));
            fs.Close();
        }

    }
}
