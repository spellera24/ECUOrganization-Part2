using System;

public class Course : IClass
{
    public String Name { get; set; }
    public Course(string name)
    {
        Name = name;
    }
}