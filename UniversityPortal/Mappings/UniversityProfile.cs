using AutoMapper;
using UniversityPortal.Entity;
using UniversityPortal.Models;

namespace UniversityPortal.Mappings
{
    public class UniversityProfile : Profile
    {
        public UniversityProfile()
        {
            CreateMap<UniversityViewModel, University>()
                   .ReverseMap();
        }
    }
}
