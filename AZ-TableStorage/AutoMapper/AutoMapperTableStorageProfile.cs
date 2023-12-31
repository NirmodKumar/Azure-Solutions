using AutoMapper;
using AZ_TableStorage.Models;
using Microsoft.Extensions.Azure;

namespace AZ_TableStorage.AutoMapper
{
    public class AutoMapperTableStorageProfile : Profile
    {
        public AutoMapperTableStorageProfile()
        {
            CreateMap<PersonEntity, PersonModel>()
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.PartitionKey))
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.RowKey))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ReverseMap();
        }
    }
}
