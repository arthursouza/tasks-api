using AutoMapper;
using Domain.Entities;
using Domain.Models;
namespace Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WorkItemModel, WorkItem>().ReverseMap();
        }
    }
}
