using System.Text.Json;
using AZ_TableStorage.Models;
using Azure;
using Azure.Data.Tables;

namespace AZ_TableStorage.TableStorageService
{
    public class TableStorageServiceProvider : ITableStorageServiceProvider
    {
        private static string _connectionString = "UseDevelopmentStorage=true";
        private static string _tableName = "Person";

        public async Task<PersonEntity> AddEntity(PersonEntity entity)
        {
            var client = new TableClient(_connectionString, _tableName);

            client.CreateIfNotExists();

            client.AddEntity(entity);

            return entity;
        }

        public async Task DeleteEntity(string partitionKey, string rowKey)
        {
            var client = new TableClient(_connectionString, _tableName);

            client.CreateIfNotExists();

            client.DeleteEntity(partitionKey, rowKey);
        }

        public async Task<List<PersonEntity>> GetEntities(string partitionKey)
        {
            var client = new TableClient(_connectionString, _tableName);

            client.CreateIfNotExists();

            var persionEntities = new List<PersonEntity>();

            // Using TableEntity from Azure.Data.Tables
            Pageable<TableEntity> oDataQueryEntities = client.Query<TableEntity>(filter: TableClient.CreateQueryFilter($"PartitionKey eq {partitionKey}"));
            foreach (TableEntity entity in oDataQueryEntities)
            {
                Console.WriteLine($"TableEntity : {entity.GetString("PartitionKey")}:{entity.GetString("RowKey")}, {entity.GetString("FirstName")}, {entity.GetString("LastName")}");

                var persionEntity = new PersonEntity
                {
                    Age = !string.IsNullOrEmpty(entity.GetString("Age")) ? Convert.ToInt32(entity.GetString("Age")) : 0,
                    Country = entity.GetString("Country"),
                    ETag = !string.IsNullOrEmpty(entity.GetString("ETag")) ? JsonSerializer.Deserialize<ETag>(entity.GetString("ETag")) : new ETag(),
                    FirstName = entity.GetString("FirstName"),
                    LastName = entity.GetString("LastName"),
                    PartitionKey = entity.GetString("PartitionKey"),
                    RowKey = entity.GetString("RowKey"),
                    Timestamp = !string.IsNullOrEmpty(entity.GetString("Timestamp")) ? Convert.ToDateTime(entity.GetString("Timestamp")) : null
                };

                persionEntities.Add(persionEntity);
            }

            // Using custom entity
            Pageable<PersonEntity> oDataQueryEntities2 = client.Query<PersonEntity>(filter: TableClient.CreateQueryFilter($"PartitionKey eq {partitionKey}"));
            foreach (PersonEntity entity in oDataQueryEntities2)
            {
                persionEntities.Add(entity);
            }

            // Using LINQ
            Pageable<PersonEntity> linqEntities = client.Query<PersonEntity>(customer => customer.PartitionKey == "User");
            foreach (PersonEntity entity in linqEntities)
            {
                persionEntities.Add(entity);
            }

            return persionEntities;
        }

        public Task<PersonEntity> UpdateEntity(PersonEntity entity, string partitionKey, string rowKey)
        {
            throw new NotImplementedException();
        }
    }
}
