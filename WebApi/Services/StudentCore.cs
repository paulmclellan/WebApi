using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class StudentCore : IStudentCore
    {
        private readonly ILogger _logger;

        public StudentCore(ILogger logger)
        {
            _logger = logger;
        }

        public List<Student> GetStudents(string[,] request)
        {
            try
            {
                var timeDimension = request.GetUpperBound(0);
                var markDimension = request.GetUpperBound(1);
                var students = new List<Student>();

                for (int time = 0; time <= timeDimension; time++)
                {
                    for (int mark = 0; mark <= markDimension; mark++)
                    {
                        if (!String.IsNullOrEmpty(request[time, mark]))
                        {
                            var student = new Student()
                            {
                                Name = request[time, mark],
                                TimeIndex = time,
                                MarkIndex = mark
                            };

                            students.Add(student);
                        }
                    }
                }
                return students;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void SetStudentGroups(List<Student> students)
        {
            try
            {
                int groupId = 0;

                foreach (var student in students)
                {
                    if (student.GroupId == 0)
                    {
                        student.GroupId = ++groupId;
                    }
                    var connectedStudents = students.Where(r => Math.Abs(r.MarkIndex - student.MarkIndex) <= 1
                                                     && Math.Abs(r.TimeIndex - student.TimeIndex) <= 1
                                                     && r.GroupId == 0).ToList();

                    foreach (var connectedStudent in students.Where(r => connectedStudents.Any(n => r.Name == n.Name)))
                    {
                        connectedStudent.GroupId = student.GroupId;
                    }
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public string[,] GetOutput(List<Student> students)
        {
            try
            {
                var groups = students.GroupBy(r => r.GroupId).Select(group => new { groupId = group.Key, count = group.Count() }).ToList();
                var groupDimension = groups.Count();
                var studentDimension = groupDimension > 0 ? groups.Max(g => g.count) : 0;

                var outputStudents = new String[groupDimension, studentDimension];

                students = students.OrderBy(r => r.GroupId).ToList();

                var groupIndex = 0;

                foreach (var group in groups)
                {
                    var studentIndex = 0;
                    foreach (var student in students.Where(r => r.GroupId == group.groupId))
                    {
                        outputStudents[groupIndex, studentIndex++] = student.Name;

                    }
                    while (studentIndex < studentDimension)
                    {
                        outputStudents[groupIndex, studentIndex++] = string.Empty;
                    }

                    groupIndex++;
                }
                return outputStudents;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}