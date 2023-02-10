using AutoMapper;
using UniversityPortal.Entity;
using UniversityPortal.Models;

namespace UniversityPortal.Mappings
{
    public class UniversityStaffProfile : Profile
    {
        public UniversityStaffProfile()
        {
            CreateMap<UniversityStaffViewModel, UniversityStaff>()
                   .ReverseMap();

            CreateMap<UniversityStaffViewModel, UniversityViewModel>()
                   .ReverseMap();
        }
    }
}
