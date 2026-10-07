using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
public class DegreeProgram 
{
    public string Name { get; set; }
    public List<IClass> Courses { get; set; }
    public DegreeProgram(string name)
    {
        Name = name;
        Courses= new List<IClass>();
    }
}