using AutoMapper;
using DAL.Entity;
using Service.Model;

namespace Service
{
    public sealed class StudentsMappingProfile : Profile
    {
        public StudentsMappingProfile()
        {
            CreateMap<Student, StudentEntity>();
            CreateMap<UpdateStudentRequest, Student>();
        }
    }
}
