using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DirectOutput.Models;
public class RuleModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Condition { get; set; }
    public ArrayList Conclusions { get; set; }
    public double Truth { get; set; }

    public RuleModel(int _id)
    {
        this.Id = _id;
        Name = "";
        Condition = "";
        Conclusions = new ArrayList();
        Truth = 1.0;
    }

    public RuleModel()
    {
    }

    public override string ToString()
    {
        return Name;
    }
}
