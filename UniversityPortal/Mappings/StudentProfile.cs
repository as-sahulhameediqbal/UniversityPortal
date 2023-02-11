using AutoMapper;
using UniversityPortal.Entity;
using UniversityPortal.Models;

namespace UniversityPortal.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentViewModel, Student>()
                      .ReverseMap();
        }

    }
}
