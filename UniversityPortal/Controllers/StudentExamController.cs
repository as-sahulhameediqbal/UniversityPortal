using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Services;

namespace UniversityPortal.Controllers
{
    [Authorize(Roles = UserRoles.Student)]
    public class StudentExamController : Controller
    {
        private IStudentExamService _studentExamService;

        public StudentExamController(IStudentExamService studentExamService)
        {
            _studentExamService= studentExamService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int sem, int year, bool result)
        {
            var response = await _studentExamService.GetStudentExam(sem, year, result);
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> ViewHallTicket(int sem, int year)
        {
            var response = await _studentExamService.HallTicketExportToPDF(sem, year);
            return response;
        }

        [HttpGet]
        public async Task<IActionResult> ViewDegreeCertificate()
        {
            var response = await _studentExamService.DegreeCertificateExportToPDF();
            return response;
        }

        [HttpGet]
        public async Task<IActionResult> ViewProvisionalCertificate()
        {
            var response = await _studentExamService.ProvisionalCertificateExportToPDF();
            return response;
        }
    }
}
