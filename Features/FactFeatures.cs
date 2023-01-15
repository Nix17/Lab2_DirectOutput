using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Lab2_DirectOutput.Models;

namespace Lab2_DirectOutput.Features;

public class FactFeatures: IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public FactModel ReadFromFile(BinaryReader br)
    {
        var model = new FactModel();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Encoding encoding = Encoding.GetEncoding("windows-1251");
        model.Id = br.ReadInt32();

        int length1 = br.ReadInt32();
        if (length1 > 0 && length1 < 1024)
        {
            byte[] numArray = new byte[length1];
            br.Read(numArray, 0, numArray.Length);
            model.Object = encoding.GetString(numArray, 0, numArray.Length);
        }
        
        int length2 = br.ReadInt32();
        if (length2 > 0 && length2 < 1024)
        {
            byte[] numArray = new byte[length2];
            br.Read(numArray, 0, numArray.Length);
            model.Attribute = encoding.GetString(numArray, 0, numArray.Length);
        }
        
        int length3 = br.ReadInt32();
        if (length3 > 0 && length3 < 1024)
        {
            byte[] numArray = new byte[length3];
            br.Read(numArray, 0, numArray.Length);
            model.Value = encoding.GetString(numArray, 0, numArray.Length);
        }
        
        model.Truth = br.ReadDouble();
        model.Type = (FactType)br.ReadInt32();

        return model;
    }

    public void ReadFromFile(BinaryReader br, FactModel model)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Encoding encoding = Encoding.GetEncoding("windows-1251");
        model.Id = br.ReadInt32();

        int length1 = br.ReadInt32();
        if (length1 > 0 && length1 < 1024)
        {
            byte[] numArray = new byte[length1];
            br.Read(numArray, 0, numArray.Length);
            model.Object = encoding.GetString(numArray, 0, numArray.Length);
        }

        int length2 = br.ReadInt32();
        if (length2 > 0 && length2 < 1024)
        {
            byte[] numArray = new byte[length2];
            br.Read(numArray, 0, numArray.Length);
            model.Attribute = encoding.GetString(numArray, 0, numArray.Length);
        }

        int length3 = br.ReadInt32();
        if (length3 > 0 && length3 < 1024)
        {
            byte[] numArray = new byte[length3];
            br.Read(numArray, 0, numArray.Length);
            model.Value = encoding.GetString(numArray, 0, numArray.Length);
        }

        model.Truth = br.ReadDouble();
        model.Type = (FactType)br.ReadInt32();
    }
}
