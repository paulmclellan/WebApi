using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IStudentCore
    {
        List<Student> GetStudents(String[,] request);

        void SetStudentGroups(List<Student> students);

        string[,] GetOutput(List<Student> students);
    }
}