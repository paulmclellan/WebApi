using System;
using System.Net;
using System.Web.Http;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    public class StudentGroupsController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IStudentCore _studentCore;

        public StudentGroupsController(ILogger logger, IStudentCore studentCore)
        {
            _logger = logger;
            _studentCore = studentCore;
        }

        public String[,] Post(String[,] request)
        {
            if (request == null || request.Length == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                var students = _studentCore.GetStudents(request);
                _studentCore.SetStudentGroups(students);
                var outputResults = _studentCore.GetOutput(students);

                _logger.LogInformation($"Student Groups Generated - {DateTime.Now} ");

                return outputResults;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
