using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityPortal.Common;
using UniversityPortal.Data;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services.Base;

namespace UniversityPortal.Services
{
    public class StudentService : BaseService, IStudentService
    {
        public List<SelectListItem> Genders = new()
            {
                new SelectListItem { Value = "Male", Text = "Male" },
                new SelectListItem { Value = "Female", Text = "Female" },
                new SelectListItem { Value = "Transgender ", Text = "Transgender " }
            };

        private readonly IUserService _userService;
        private readonly IUniversityStaffService _universityStaffService;
        private readonly IUniversityService _universityService;

        public StudentService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IDateTimeProvider dateTimeProvider,
                              ICurrentUserService currentUserService,
                              IUserService userService,
                              IUniversityStaffService universityStaffService,
                              IUniversityService universityService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _userService = userService;
            _universityStaffService = universityStaffService;
            _universityService = universityService;
        }

        public async Task<List<SelectListItem>> GetAllGender()
        {
            return Genders;
        }

        public async Task<List<SelectListItem>> GetAllUniversities()
        {
            var universities = new List<SelectListItem>();
            var newitem = new SelectListItem
            {
                Value = " ",
                Text = "-- Select --"
            };
            universities.Add(newitem);

            var universityList = await _universityService.GetAll();
            if (universityList == null | universityList.Count() == 0)
            {
                return universities;
            }
            
            foreach (var university in universityList)
            {
                var item = new SelectListItem
                {
                    Value = university.Name,
                    Text = university.Name
                };
                universities.Add(item);
            }
            return universities;
        }

        public async Task<StudentViewModel> Get(Guid id)
        {
            var result = await UnitOfWork.StudentRepository.Get(id);
            var student = Mapper.Map<StudentViewModel>(result);
            student.Password = nameof(student.Password);
            return student;

        }
        public async Task<IEnumerable<StudentViewModel>> GetAll()
        {
            var universityId = await _universityStaffService.GetUniversityId();
            var result = await UnitOfWork.StudentRepository.FindAll(x => x.UniversityId == universityId);
            var student = Mapper.Map<IEnumerable<StudentViewModel>>(result);
            foreach (var item in student)
            {
                item.Role = CurrentUserService.Role;
            }
            return student;
        }

        public async Task<Guid> GetUniversityId()
        {
            var result = await UnitOfWork.StudentRepository.FindAsync(x => x.IsActive
                                                                         && x.Email == CurrentUserService.Email);

            return result.UniversityId;
        }

        public async Task<StudentViewModel> GetStudentProfile()
        {
            var id = await GetStudentId();
            var response = await Get(id);
            return response;
        }

        public async Task<Guid> GetStudentId()
        {
            var result = await UnitOfWork.StudentRepository.FindAsync(x => x.IsActive
                                                                         && x.Email == CurrentUserService.Email);

            return result.Id;
        }

        public async Task<string> GetStudentName()
        {
            var result = await UnitOfWork.StudentRepository.FindAsync(x => x.IsActive
                                                                         && x.Email == CurrentUserService.Email);

            return result.Name;
        }

        public async Task<string> GetStudentName(Guid id)
        {
            var result = await UnitOfWork.StudentRepository.Get(id);
            return result.Name;
        }

        public async Task<AppResponse> Save(StudentViewModel model)
        {
            var result = await IsStudentExists(model);
            if (!result.Success)
            {
                return result;
            }

            if (model.Id == Guid.Empty)
            {
                model.UniversityId = await _universityStaffService.GetUniversityId();
                result = await _userService.Create(model.Email, model.Password, UserRoles.Student);
                if (!result.Success)
                {
                    return result;
                }
                await Add(model);
            }
            else
            {
                await Update(model);
            }

            await UnitOfWork.Save();

            return result;
        }

        private async Task Add(StudentViewModel model)
        {
            var student = Mapper.Map<Student>(model);

            student.Id = Guid.NewGuid();
            student.UserId = await _userService.GetUserId(model.Email);
            student.CreatedBy = CurrentUserService.UserId;
            student.CreatedDate = DateTimeProvider.DateTimeOffsetNow;
            student.ModifiedBy = CurrentUserService.UserId;
            student.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            await UnitOfWork.StudentRepository.Add(student);
        }

        private async Task Update(StudentViewModel model)
        {
            var student = await UnitOfWork.StudentRepository.Get(model.Id);

            student = Mapper.Map(model, student);

            student.ModifiedBy = CurrentUserService.UserId;
            student.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            UnitOfWork.StudentRepository.Update(student);
        }

        public async Task UpdatePaidTutionFee()
        {
            var id = await GetStudentId();
            var student = await UnitOfWork.StudentRepository.Get(id);

            student.IsPaid = true;
            student.ModifiedBy = CurrentUserService.UserId;
            student.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            UnitOfWork.StudentRepository.Update(student);
            await UnitOfWork.Save();
        }

        private async Task<AppResponse> IsStudentExists(StudentViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                var result = await UnitOfWork.StudentRepository.FindAny(x => x.IsActive
                                                                          && x.Name == model.Name
                                                                          && x.Email == model.Email);
                if (result)
                {
                    return AppResult.msg(false, "Student already exist");
                }
            }
            else
            {
                var result = await UnitOfWork.StudentRepository.FindAny(x => x.IsActive
                                                                             && x.Name == model.Name
                                                                            && x.Email == model.Email
                                                                            && x.Id != model.Id);
                if (result)
                {
                    return AppResult.msg(false, "Student already exist");
                }
            }

            return AppResult.msg(true, "Student not exist");
        }

        public string GetRole()
        {
            return CurrentUserService.Role;
        }

        public async Task UpdateIsComplete(Guid id)
        {
            var student = await UnitOfWork.StudentRepository.Get(id);

            student.IsCompleted = true;
            student.CompletedDate = DateTimeProvider.DateTimeOffsetNow;
            student.ModifiedBy = CurrentUserService.UserId;
            student.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            UnitOfWork.StudentRepository.Update(student);
            await UnitOfWork.Save();
        }

        public async Task<IEnumerable<StudentCertificateViewModel>> GetStudentCertificateAll()
        {
            var universityId = await _universityStaffService.GetUniversityId();
            var universityName = await _universityService.GetName(universityId);
            var students = await UnitOfWork.StudentRepository.GetAll();

            var newstudents = students.Where(x => x.ExistingUniversityName == universityName);
            if (newstudents == null || newstudents.Count() == 0)
            {
                return Enumerable.Empty<StudentCertificateViewModel>();
            }

            var list = new List<StudentCertificateViewModel>();

            foreach (var newstudent in newstudents)
            {
                var oldStudent = students.FirstOrDefault(x => x.RollNumber == newstudent.ExistingRollNumber);
                if (oldStudent != null)
                {
                    var student = new StudentCertificateViewModel()
                    {
                        Id = newstudent.Id,
                        Name = oldStudent.Name,
                        AdmissionNumber = oldStudent.AdmissionNumber,
                        RollNumber = oldStudent.RollNumber,
                        Email = oldStudent.Email,
                        Program = oldStudent.Program,
                        Department = oldStudent.Department,
                        JoiningDate = oldStudent.JoiningDate,
                        CompletedDate = oldStudent.CompletedDate,
                        University = await _universityService.GetName(newstudent.UniversityId),
                        IsVerifyCertifiate = newstudent.IsVerifyCertifiate,
                        IsRejectCertifiate = newstudent.IsRejectCertifiate,
                        VerifyDate = newstudent.VerifyDate,
                    };
                    list.Add(student);
                }
            }

            var studentlist = list.OrderByDescending(x => x.CompletedDate)
                             .ToList();

            return studentlist.AsEnumerable();
        }

        public async Task VerifyStudentCertificate(Guid id, bool status)
        {
            var student = await UnitOfWork.StudentRepository.Get(id);
            if (student == null)
            {
                return;
            }

            if (status)
            {
                student.IsVerifyCertifiate = true;
            }
            else
            {
                student.IsRejectCertifiate = true;
            }
            student.VerifyDate = DateTimeProvider.DateTimeOffsetNow;

            UnitOfWork.StudentRepository.Update(student);
            await UnitOfWork.Save();
        }
    }
}
