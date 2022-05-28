using CosmosStorage.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace CosmosStorage.Dao
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Microsoft.Azure.Cosmos.Container container;

        public CosmosDbService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task CreatePersonAsync(Person person)
            => await container.CreateItemAsync(person, new PartitionKey(person.Id));

        public async Task DeletePersonAsync(Person person)
            => await container.DeleteItemAsync<Person>(person.Id, new PartitionKey(person.Id));


        public async Task<IEnumerable<Person>> GetPeopleAsync(string queryString)
        {
            List<Person> people = new List<Person>();

            var query = container.GetItemQueryIterator<Person>(new QueryDefinition(queryString));

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                people.AddRange(response.ToList());
            }

            return people;
        }

        public async Task<Person> GetPersonAsync(string id)
        {
            try
            {
                return await container.ReadItemAsync<Person>(id, new PartitionKey(id));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdatePersonAsync(Person person)
            => await container.UpsertItemAsync(person, new PartitionKey(person.Id));
            
    }
}