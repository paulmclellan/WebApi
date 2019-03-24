using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApi.Core;
using WebApi.Models;

namespace WebApi.Tests
{
    [TestClass]
    public class StudentCoreTest
    {

        [TestMethod]
        public void GetStudentsWithSingleGroup()
        {
            // Arrange
            String[,] input = new String[,] { { "", "Paul", "" } };
            List<Student> expected = new List<Student> {
                new Student (){
                    GroupId = 0,
                    Name="Paul",
                    TimeIndex = 0,
                    MarkIndex = 1}
            };

            // Act
            StudentCore core = new StudentCore();
            var output = core.GetStudents(input);

            // Assert
            CollectionAssert.AreEqual(expected, output, new StudentComparer());
        }

        [TestMethod]
        public void GetStudentsWithMultipleGroups()
        {
            // Arrange
            String[,] input = new String[,] { { "", "Paul", "" }, { "Fred", "", "" }, { "", "John", "" } };
            List<Student> expected = new List<Student> {
                new Student (){
                    GroupId = 0,
                    Name="Paul",
                    TimeIndex = 0,
                    MarkIndex = 1},
                new Student() {
                    GroupId = 0,
                    Name ="Fred",
                    TimeIndex = 1,
                    MarkIndex = 0},
                new Student() {
                    GroupId = 0,
                    Name="John",
                    TimeIndex = 2,
                    MarkIndex = 1}
            };

            // Act
            StudentCore core = new StudentCore();
            var output = core.GetStudents(input);

            // Assert
            CollectionAssert.AreEqual(expected, output, new StudentComparer());
        }

        [TestMethod]
        public void SetStudentGroupSingleGroup()
        {
            // Arrange
            List<Student> input = new List<Student> {
                new Student (){
                    GroupId = 0,
                    Name="Paul",
                    TimeIndex = 0,
                    MarkIndex = 1},
                new Student() {
                    GroupId = 0,
                    Name ="Fred",
                    TimeIndex = 1,
                    MarkIndex = 0},
                new Student() {
                    GroupId = 0,
                    Name="John",
                    TimeIndex = 2,
                    MarkIndex = 1}
            };
            List<Student> expected = new List<Student> {
                new Student (){
                    GroupId = 1,
                    Name="Paul",
                    TimeIndex = 0,
                    MarkIndex = 1},
                new Student() {
                    GroupId = 1,
                    Name ="Fred",
                    TimeIndex = 1,
                    MarkIndex = 0},
                new Student() {
                    GroupId = 1,
                    Name="John",
                    TimeIndex = 2,
                    MarkIndex = 1}
            };

            // Act
            StudentCore core = new StudentCore();
            core.SetStudentGroups(input);

            // Assert
            CollectionAssert.AreEqual(expected, input, new StudentComparer());
        }

        [TestMethod]
        public void SetStudentGroupMultipleGroup()
        {
            // Arrange
            List<Student> input = new List<Student> {
                new Student (){
                    GroupId = 0,
                    Name="Paul",
                    TimeIndex = 0,
                    MarkIndex = 1},
                new Student() {
                    GroupId = 0,
                    Name ="Fred",
                    TimeIndex = 1,
                    MarkIndex = 0},
                new Student() {
                    GroupId = 0,
                    Name="John",
                    TimeIndex = 2,
                    MarkIndex = 2}
            };
            List<Student> expected = new List<Student> {
                new Student (){
                    GroupId = 1,
                    Name="Paul",
                    TimeIndex = 0,
                    MarkIndex = 1},
                new Student() {
                    GroupId = 1,
                    Name ="Fred",
                    TimeIndex = 1,
                    MarkIndex = 0},
                new Student() {
                    GroupId = 2,
                    Name="John",
                    TimeIndex = 2,
                    MarkIndex = 2}
            };

            // Act
            StudentCore core = new StudentCore();
            core.SetStudentGroups(input);

            // Assert
            CollectionAssert.AreEqual(expected, input, new StudentComparer());
        }

        [TestMethod]
        public void GetOutputSingleGroup()
        {
            // Arrange
            List<Student> input = new List<Student> {
                new Student (){
                    GroupId = 1,
                    Name="Paul",
                    TimeIndex = 0,
                    MarkIndex = 1},
                new Student() {
                    GroupId = 1,
                    Name ="Fred",
                    TimeIndex = 1,
                    MarkIndex = 0},
                new Student() {
                    GroupId = 1,
                    Name="John",
                    TimeIndex = 2,
                    MarkIndex = 1}
            };
            String[,] expected = new String[,] { { "Paul", "Fred", "John" } };

            // Act
            StudentCore core = new StudentCore();
            var output = core.GetOutput(input);

            // Assert
            CollectionAssert.AreEqual(expected, output);
        }

        [TestMethod]
        public void GetOutputMultipleGroup()
        {
            // Arrange
            List<Student> input = new List<Student> {
                new Student (){
                    GroupId = 1,
                    Name="Paul",
                    TimeIndex = 0,
                    MarkIndex = 1},
                new Student() {
                    GroupId = 1,
                    Name ="Fred",
                    TimeIndex = 1,
                    MarkIndex = 0},
                new Student() {
                    GroupId = 2,
                    Name="John",
                    TimeIndex = 2,
                    MarkIndex = 2}
            };
            String[,] expected = new String[,] { { "Paul", "Fred" }, { "John", "" } };

            // Act
            StudentCore core = new StudentCore();
            var output = core.GetOutput(input);

            // Assert
            CollectionAssert.AreEqual(expected, output);
        }



        private class StudentComparer : Comparer<Student>
        {
            public override int Compare(Student x, Student y)
            {
                int result;

                if (x.Name.CompareTo(y.Name) == 0
                    && x.GroupId.CompareTo(y.GroupId) == 0
                    && x.TimeIndex.CompareTo(y.TimeIndex) == 0
                    && x.MarkIndex.CompareTo(y.MarkIndex) == 0)
                {
                    result = 0;
                }
                else
                {
                    result = 1;
                }
                return result;
            }
        }
    }
}
