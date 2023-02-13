using AutoMapper;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services.Base;

namespace UniversityPortal.Services
{
    public class SemesterExamService : BaseService, ISemesterExamService
    {
        private readonly IUniversityStaffService _universityStaffService;
        public SemesterExamService(IUnitOfWork unitOfWork,
                                   IMapper mapper,
                                   IDateTimeProvider dateTimeProvider,
                                   ICurrentUserService currentUserService,
                                   IUniversityStaffService universityStaffService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _universityStaffService = universityStaffService;
        }

        public async Task<IEnumerable<SemesterExamViewModel>> GetAllSemesterExam()
        {
            var universityId = await _universityStaffService.GetUniversityId();
            var semesterExam = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId);
            if (semesterExam == null || semesterExam.Count() == 0)
            {
                await Create(universityId, 1);
                return await GetAllSemesters(universityId);
            }
            else
            {
                if (!semesterExam.Any(x => x.SemesterYear == DateTimeProvider.DateTimeOffsetNow.Year))
                {
                    await Create(universityId, 1);

                }
                else if (semesterExam.Any(x => x.SemesterYear == DateTimeProvider.DateTimeOffsetNow.Year
                                            && x.IsPublish == false))
                {

                }
                else if (!semesterExam.Any(x => x.SemesterYear == DateTimeProvider.DateTimeOffsetNow.Year
                                            && x.IsPublish == true && x.Semester == 2))
                {
                    await Create(universityId, 2);
                }

                return await GetAllSemesters(universityId);
            }
        }

        private async Task<IEnumerable<SemesterExamViewModel>> GetAllSemesters(Guid universityId)
        {
            var semesterExam = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId);
            var exams = semesterExam
                            .Select(x => new SemesterExamViewModel
                            {
                                SemesterYear = x.SemesterYear,
                                Semester = x.Semester,
                                ExamPublishedDate = x.PublishDate,
                                IsPublish = x.IsPublish
                            })
                            .DistinctBy(x => x.SemesterYear + "-" + x.Semester)
                            .OrderByDescending(x => x.SemesterYear)
                            .ThenByDescending(x => x.Semester)
                            .ToList();

            return exams.AsEnumerable();
        }

        public async Task Create(Guid universityId, int sem)
        {
            var semesterExams = new List<SemesterExam>();

            var papers = await UnitOfWork.PaperRepository.FindAll(x => x.Semester == sem);

            foreach (var paper in papers)
            {
                var semster = AddSemesterExam(paper, sem, universityId, false);
                semesterExams.Add(semster);
            }
            if (sem == 2)
            {
                var arrearPapers = await UnitOfWork.PaperRepository.FindAll(x => x.Semester == 1);

                foreach (var paper in arrearPapers)
                {
                    var semster = AddSemesterExam(paper, sem, universityId, true);
                    semesterExams.Add(semster);
                }
            }

            await UnitOfWork.SemesterExamRepository.AddRange(semesterExams);
            await UnitOfWork.Save();
        }

        private SemesterExam AddSemesterExam(Paper paper, int sem, Guid universityId, bool arrear)
        {
            var semster = new SemesterExam()
            {
                Id = Guid.NewGuid(),
                UniversityId = universityId,
                PaperId = paper.Id,
                Semester = sem,
                SemesterYear = DateTimeProvider.DateTimeOffsetNow.Year,
                ExamDate = DateTimeProvider.DateTimeOffsetNow,
                IsArrear = arrear,
                IsPublish = false,
                IsActive = true,
                CreatedBy = CurrentUserService.UserId,
                CreatedDate = DateTimeProvider.DateTimeOffsetNow,
                ModifiedBy = CurrentUserService.UserId,
                ModifiedDate = DateTimeProvider.DateTimeOffsetNow
            };

            return semster;
        }

        public async Task<SemesterExamPaperViewModel> GetAllSemesterExamPaper(int sem, int year)
        {
            SemesterExamPaperViewModel exam = new SemesterExamPaperViewModel();
            var universityId = await _universityStaffService.GetUniversityId();

            var semesterExam = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId
                                                                               && x.Semester == sem
                                                                               && x.SemesterYear == year);

            if (semesterExam == null || semesterExam.Count() == 0)
            {
                exam.Semester = await GetSemesterPaper(sem);
                if (sem == 2)
                {
                    exam.Arrear = await GetSemesterPaper(1);
                }
                exam.IsPublish = false;
            }
            else
            {
                var currentSemester = semesterExam.Where(x => x.IsArrear == false).ToList();
                if (currentSemester != null && currentSemester.Count() > 0)
                {
                    exam.Semester = await GetSemesterPaper(currentSemester);
                }
                else
                {
                    exam.Semester = new List<SemesterExamModel>();
                }
                var arrearSemester = semesterExam.Where(x => x.IsArrear == true).ToList();
                if (arrearSemester != null && arrearSemester.Count() > 0)
                {
                    exam.Arrear = await GetSemesterPaper(arrearSemester);
                }
                else
                {
                    exam.Arrear = new List<SemesterExamModel>();
                }
                exam.IsPublish = semesterExam.Any(x => x.IsPublish);
            }

            return exam;
        }

        private async Task<List<SemesterExamModel>> GetSemesterPaper(List<SemesterExam> semesterExams)
        {
            var semExams = new List<SemesterExamModel>();
            var papers = await UnitOfWork.PaperRepository.GetAll();

            foreach (var semesterExam in semesterExams)
            {
                var paper = papers.FirstOrDefault(x => x.Id == semesterExam.PaperId);
                var current = new SemesterExamModel()
                {
                    Semester = semesterExam.Semester,
                    BaseSemester = paper.Semester,
                    Subject = paper.Name,
                    SemesterYear = semesterExam.SemesterYear,
                    ExamDate = semesterExam.ExamDate,
                };
                semExams.Add(current);
            }
            return semExams;
        }

        private async Task<List<SemesterExamModel>> GetSemesterPaper(int sem)
        {
            var semExams = new List<SemesterExamModel>();
            var papers = await UnitOfWork.PaperRepository.GetAll();
            var currentSempapers = papers.Where(x => x.Semester == sem).ToList();
            foreach (var currentSempaper in currentSempapers)
            {
                var current = new SemesterExamModel()
                {
                    Semester = currentSempaper.Semester,
                    Subject = currentSempaper.Name,
                    SemesterYear = DateTimeProvider.DateTimeOffsetNow.Year
                };
                semExams.Add(current);
            }
            return semExams;
        }


        public async Task PublishExam(int sem, int year)
        {
            var universityId = await _universityStaffService.GetUniversityId();

            var semesterExam = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId
                                                                               && x.Semester == sem
                                                                               && x.SemesterYear == year);

            if (semesterExam == null || semesterExam.Count() == 0)
            {
                return;
            }

            var studentExams = new List<StudentExam>();
            var currentSemester = semesterExam.Where(x => x.IsArrear == false).ToList();
            var arrearSemester = semesterExam.Where(x => x.IsArrear == true).ToList();
            var arrearStudentExams = await UnitOfWork.StudentExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                        && x.IsPass == false);

            var students = await UnitOfWork.StudentRepository.FindAll(x => x.UniversityId == universityId
                                                                            && x.IsCompleted == false
                                                                            && x.IsActive == true);

            foreach (var student in students)
            {
                foreach (var current in currentSemester)
                {
                    var studentExam = AddStudentExam(student, current, universityId, 1);
                    studentExams.Add(studentExam);

                    current.IsPublish = true;
                    current.PublishDate = DateTimeProvider.DateTimeOffsetNow;

                    UnitOfWork.SemesterExamRepository.Update(current);
                }

                foreach (var arrear in arrearSemester)
                {
                    var attempt = arrearStudentExams.Count(x => x.StudentId == student.Id
                                                                && x.PaperId == arrear.PaperId);
                    if (attempt > 0)
                    {
                        var studentExam = AddStudentExam(student, arrear, universityId, attempt + 1);
                        studentExams.Add(studentExam);
                    }

                    arrear.IsPublish = true;
                    arrear.PublishDate = DateTimeProvider.DateTimeOffsetNow;

                    UnitOfWork.SemesterExamRepository.Update(arrear);
                }

            }

            await UnitOfWork.StudentExamRepository.AddRange(studentExams);
            await UnitOfWork.Save();
        }

        private StudentExam AddStudentExam(Student student, SemesterExam semesterExam, Guid universityId, int attempt)
        {
            var studentExam = new StudentExam()
            {
                Id = Guid.NewGuid(),
                UniversityId = universityId,
                StudentId = student.Id,
                SemesterExamId = semesterExam.Id,
                PaperId = semesterExam.PaperId,
                Attempt = attempt,
                IsActive = true,
                CreatedBy = CurrentUserService.UserId,
                CreatedDate = DateTimeProvider.DateTimeOffsetNow,
                ModifiedBy = CurrentUserService.UserId,
                ModifiedDate = DateTimeProvider.DateTimeOffsetNow
            };

            return studentExam;
        }
    }
}
