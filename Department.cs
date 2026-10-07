using System;
using System.Collections.Generic;

public class Department
{
    public string Name { get; set; }
    public List <DegreeProgram> DegreePrograms { get ; set; }

    public Department(string name)
    {
        Name = name;
        DegreePrograms = new List<DegreeProgram>();
    }
}

