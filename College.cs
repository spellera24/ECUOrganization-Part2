using System;
using System.Collections.Generic;
using System.ComponentModel;

public class College
{
    public String Name { get; set; }

    public List<Department> Departments { get; set; }
    public College(string name)
    {
        Name = name;
        Departments = new List<Department>();
    }

}