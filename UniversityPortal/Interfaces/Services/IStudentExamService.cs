using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IStudentExamService
    {
        Task<IEnumerable<StudentSemesterViewModel>> GetAllStudentSemester(bool isResult = false);
        Task<StudentExamViewModel> GetStudentExam(int sem, int year, bool isResult = false);
        Task<StudentExamViewModel> GetStudentSemesterExam(Guid studentId, Guid universityId, int sem, int year);
        Task UpdatePaidExamFee(int sem, int year);
        Task<IActionResult> HallTicketExportToPDF(int sem, int year);
        Task<IActionResult> ProvisionalCertificateExportToPDF();
        Task<IActionResult> DegreeCertificateExportToPDF();

    }
}
