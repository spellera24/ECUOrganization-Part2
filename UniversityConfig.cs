using System.Collections.Generic;

public sealed class UniversityConfig
{
    public string Name { get; set; } = "";
    public List<CollegeConfig> Colleges { get; set; } = new();
}

public sealed class CollegeConfig
{
    public string Name { get; set; } = "";
    public List<DepartmentConfig> Departments { get; set; } = new();
}

public sealed class DepartmentConfig
{
    public string Name { get; set; } = "";
    public List<DegreeProgramConfig> DegreePrograms { get; set; } = new();
}

public sealed class DegreeProgramConfig
{
    public string Name { get; set; } = "";
    public List<CourseConfig> Courses { get; set; } = new();
}

public sealed class CourseConfig
{
    public string Name { get; set; } = "";
}