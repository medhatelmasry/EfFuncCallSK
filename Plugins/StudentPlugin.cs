using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EfFuncCallSK.Models;
using Microsoft.SemanticKernel;

namespace EfFuncCallSK.Plugins;

public class StudentPlugin
{
    [KernelFunction, Description("Get student details by first name and last name")]
    public static string? GetStudentDetails(
        [Description("student first name, e.g. Kim")]
      string firstName,
        [Description("student last name, e.g. Ash")]
      string lastName
    )
    {
        var db = Utils.GetDbContext();
        var studentDetails = db.Students
          .Where(s => s.FirstName == firstName && s.LastName == lastName).FirstOrDefault();
        if (studentDetails == null)
            return null;
        return studentDetails.ToString();
    }

    [KernelFunction, Description("Get students in a school given the school name")]
    public static string? GetStudentsBySchool(
      [Description("The school name, e.g. Nursing")]
    string school
    )
    {
        var studentsBySchool = Utils.GetDbContext().Students
          .Where(s => s.School == school).ToList();
        if (studentsBySchool.Count == 0)
            return null;
        return JsonSerializer.Serialize(studentsBySchool);
    }


    [KernelFunction, Description("Get the school with most or least students. Takes boolean argument with true for most and false for least.")]
    static public string? GetSchoolWithMostOrLeastStudents(
      [Description("isMost is a boolean argument with true for most and false for least. Default is true.")]
    bool isMost = true
    )
    {
        var students = Utils.GetDbContext().Students.ToList();
        IGrouping<string, Student>? schoolGroup = null;
        if (isMost)
            schoolGroup = students.GroupBy(s => s.School)
                .OrderByDescending(g => g.Count()).FirstOrDefault()!;
        else
            schoolGroup = students.GroupBy(s => s.School)
                .OrderBy(g => g.Count()).FirstOrDefault()!;
        if (schoolGroup != null)
            return $"{schoolGroup.Key} has {schoolGroup.Count()} students";
        else
            return null;
    }

    [KernelFunction, Description("Get students grouped by school.")]
    static public string? GetStudentsInSchool()
    {
        var students = Utils.GetDbContext().Students.ToList().GroupBy(s => s.School)
          .OrderByDescending(g => g.Count());
        if (students == null)
            return null;
        else
            return JsonSerializer.Serialize(students);
    }
}