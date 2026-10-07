using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        UniversityConfig config = builder.Configuration
            .GetSection("University")
            .Get<UniversityConfig>()
            ?? throw new InvalidOperationException("University configuration is missing.");

        foreach (var college in config.Colleges)
        foreach (var department in college.Departments)
        foreach (var program in department.DegreePrograms)
        foreach (var course in program.Courses)
        {
            string courseName = course.Name;

            builder.Services.AddKeyedSingleton<IClass>(
                courseName,
                (sp, key) =>
                    ActivatorUtilities.CreateInstance<Course>(sp, courseName));
        }

        foreach (var college in config.Colleges)
        foreach (var department in college.Departments)
        foreach (var program in department.DegreePrograms)
        {
            DegreeProgramConfig programConfig = program;

            builder.Services.AddKeyedSingleton<DegreeProgram>(
                programConfig.Name,
                (sp, key) =>
                {
                    var degreeProgram =
                        ActivatorUtilities.CreateInstance<DegreeProgram>(
                            sp,
                            programConfig.Name);

                    foreach (var course in programConfig.Courses)
                    {
                        degreeProgram.Courses.Add(
                            sp.GetRequiredKeyedService<IClass>(course.Name));
                    }

                    return degreeProgram;
                });
        }

        foreach (var college in config.Colleges)
        foreach (var department in college.Departments)
        {
            DepartmentConfig departmentConfig = department;

            builder.Services.AddKeyedSingleton<Department>(
                departmentConfig.Name,
                (sp, key) =>
                {
                    var departmentObject =
                        ActivatorUtilities.CreateInstance<Department>(
                            sp,
                            departmentConfig.Name);

                    foreach (var program in departmentConfig.DegreePrograms)
                    {
                        departmentObject.DegreePrograms.Add(
                            sp.GetRequiredKeyedService<DegreeProgram>(
                                program.Name));
                    }

                    return departmentObject;
                });
        }

        foreach (var college in config.Colleges)
        {
            CollegeConfig collegeConfig = college;

            builder.Services.AddKeyedSingleton<College>(
                collegeConfig.Name,
                (sp, key) =>
                {
                    var collegeObject =
                        ActivatorUtilities.CreateInstance<College>(
                            sp,
                            collegeConfig.Name);

                    foreach (var department in collegeConfig.Departments)
                    {
                        collegeObject.Departments.Add(
                            sp.GetRequiredKeyedService<Department>(
                                department.Name));
                    }

                    return collegeObject;
                });
        }

        builder.Services.AddSingleton<ECU>(sp =>
        {
            var ecu = ActivatorUtilities.CreateInstance<ECU>(sp);

            ecu.Name = config.Name;

            foreach (var college in config.Colleges)
            {
                ecu.Colleges.Add(
                    sp.GetRequiredKeyedService<College>(college.Name));
            }

            return ecu;
        });

        using IHost host = builder.Build();

        var ecu = host.Services.GetRequiredService<ECU>();

        IClass myClass = ecu.Colleges
            .Where(c => c.Name == "EngineeringAndTechCollege").First()
            .Departments
            .Where(d => d.Name == "ComputerScienceDepartment").First()
            .DegreePrograms
            .Where(p => p.Name == "SoftwareEngineeringProgram").First()
            .Courses
            .Where(c => c.Name == "SoftwareArchitectureClass").First();

        Console.WriteLine(myClass.Name);
    }
}