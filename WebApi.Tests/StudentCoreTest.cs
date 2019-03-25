using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Tests
{
    [TestClass]
    public class StudentCoreTest
    {

        [TestMethod]
        public void GetStudentsWithSingleGroup()
        {
            // Arrange
            var input = new String[,] { { "", "Paul", "" } };
            var expected = new List<Student> {
                new Student (){
                    GroupId = 0,
                    Name="Paul",
                    TimeIndex = 0,
                    MarkIndex = 1}
            };

            // Act
            var logger = new Logger();
            var studentCore = new StudentCore(logger);
            var actual = studentCore.GetStudents(input);

            // Assert
            CollectionAssert.AreEqual(expected, actual, new StudentComparer());
        }

        [TestMethod]
        public void GetStudentsWithMultipleGroups()
        {
            // Arrange
            var input = new String[,] { { "", "Paul", "" }, { "Fred", "", "" }, { "", "John", "" } };
            var expected = new List<Student> {
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
            var logger = new Logger();
            var studentCore = new StudentCore(logger);
            var actual = studentCore.GetStudents(input);

            // Assert
            CollectionAssert.AreEqual(expected, actual, new StudentComparer());
        }

        [TestMethod]
        public void SetStudentGroupSingleGroup()
        {
            // Arrange
            var actual = new List<Student> {
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
            var expected = new List<Student> {
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
            var logger = new Logger();
            var studentCore = new StudentCore(logger);
            studentCore.SetStudentGroups(actual);

            // Assert
            CollectionAssert.AreEqual(expected, actual, new StudentComparer());
        }

        [TestMethod]
        public void SetStudentGroupMultipleGroup()
        {
            // Arrange
            var actual = new List<Student> {
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
            var expected = new List<Student> {
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
            var logger = new Logger();
            var studentCore = new StudentCore(logger);
            studentCore.SetStudentGroups(actual);

            // Assert
            CollectionAssert.AreEqual(expected, actual, new StudentComparer());
        }

        [TestMethod]
        public void GetOutputSingleGroup()
        {
            // Arrange
            var input = new List<Student> {
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
            var expected = new String[,] { { "Paul", "Fred", "John" } };

            // Act
            var logger = new Logger();
            var studentCore = new StudentCore(logger);
            var actual = studentCore.GetOutput(input);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetOutputMultipleGroup()
        {
            // Arrange
            var input = new List<Student> {
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
            var expected = new String[,] { { "Paul", "Fred" }, { "John", "" } };

            // Act
            var logger = new Logger();
            var studentCore = new StudentCore(logger);
            var actual = studentCore.GetOutput(input);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
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
