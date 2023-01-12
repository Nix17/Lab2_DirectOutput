using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DirectOutput.Models;

public enum FactType
{
    Intermediate,
    ForQuestion,
    ForResult,
}

public class FactModel
{
    public int Id { get; set; }
    public string Object { get; set; }
    public string Attribute { get; set; }
    public string Value { get; set; }
    public double Truth { get; set; }
    public FactType Type { get; set; }

    public FactModel()
    {
    }

    public FactModel(
        int _id,
        string _object,
        string _attribute,
        string _value,
        double _truth,
        FactType _type
    )
    {
        Id = _id;
        Object = _object;
        Attribute = _attribute;
        Value = _value;
        Truth = _truth;
        Type = _type;
    }

    public override string ToString()
    {
        return Object + "__" + Attribute + "__" + Value;
    }
}
