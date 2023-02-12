using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface ISemesterExamService
    {
        Task<IEnumerable<SemesterExamViewModel>> GetAllSemesterExam();
        Task<SemesterExamPaperViewModel> GetAllSemesterExamPaper(int sem, int year);
        Task PublishExam(int sem, int year);
    }
}
