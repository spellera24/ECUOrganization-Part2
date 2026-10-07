using System;
using System.Collections.Generic;
using System.ComponentModel;

public class ECU
{
    public string Name { get; set; }
    public List<College> Colleges { get; set; }
    public ECU()
    {
        Name = "ECU";
        Colleges= new List <College>();
    }
}
