using AZ_TableStorage.Models;

namespace AZ_TableStorage.TableStorageService
{
    public interface ITableStorageServiceProvider
    {
        Task<PersonEntity> AddEntity(PersonEntity entity);
        Task<PersonEntity> UpdateEntity(PersonEntity entity, string partitionKey, string rowKey);
        Task DeleteEntity(string partitionKey, string rowKey);
        Task<List<PersonEntity>> GetEntities(string partitionKey);
    }
}
