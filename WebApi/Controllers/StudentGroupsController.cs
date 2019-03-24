using System;
using System.Net;
using System.Web.Http;
using WebApi.Core;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    public class StudentGroupsController : ApiController
    {
        private readonly ILogger _logger;

        #region Constructors
        public StudentGroupsController()
        {
        }

        public StudentGroupsController(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region Methods
        public String[,] Post(String[,] request)
        {
            if (request == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            if (request.Length == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                var core = new StudentCore();

                var students = core.GetStudents(request);
                core.SetStudentGroups(students);
                var outputResults = core.GetOutput(students);

                _logger.LogInformation($"Student Groups Generated - {DateTime.Now} ");

                return outputResults;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}
