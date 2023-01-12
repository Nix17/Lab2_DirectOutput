using Lab2_DirectOutput.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab2_DirectOutput.Features;

public class RuleFeatures
{
    public RuleModel ReadFromFile(BinaryReader br)
    {
        var rule = new RuleModel();

        Encoding encoding = Encoding.GetEncoding("windows-1251");
        rule.Id = br.ReadInt32();
        int length1 = br.ReadInt32();
        if (length1 > 0 && length1 < 1024)
        {
            byte[] numArray = new byte[length1];
            br.Read(numArray, 0, numArray.Length);
            rule.Name = encoding.GetString(numArray, 0, numArray.Length);
        }
        int length2 = br.ReadInt32();
        if (length2 > 0 && length2 < 1024)
        {
            byte[] numArray = new byte[length2];
            br.Read(numArray, 0, numArray.Length);
            rule.Condition = encoding.GetString(numArray, 0, numArray.Length);
        }
        
        rule.Conclusions.Clear();

        int num = br.ReadInt32();
        for (int index = 0; index < num; ++index) rule.Conclusions.Add(br.ReadInt32());
        rule.Truth = br.ReadDouble();
        
        return rule;
    }

    public void ReadFromFile(BinaryReader br, RuleModel rule)
    {
        Encoding encoding = Encoding.GetEncoding("windows-1251");
        rule.Id = br.ReadInt32();
        int length1 = br.ReadInt32();
        if (length1 > 0 && length1 < 1024)
        {
            byte[] numArray = new byte[length1];
            br.Read(numArray, 0, numArray.Length);
            rule.Name = encoding.GetString(numArray, 0, numArray.Length);
        }
        int length2 = br.ReadInt32();
        if (length2 > 0 && length2 < 1024)
        {
            byte[] numArray = new byte[length2];
            br.Read(numArray, 0, numArray.Length);
            rule.Condition = encoding.GetString(numArray, 0, numArray.Length);
        }

        rule.Conclusions.Clear();

        int num = br.ReadInt32();
        for (int index = 0; index < num; ++index) rule.Conclusions.Add(br.ReadInt32());
        rule.Truth = br.ReadDouble();
    }
}
