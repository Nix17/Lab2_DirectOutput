using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DirectOutput.Models;

public class FormDataModel
{
    public string FileName { get; set; } = string.Empty;
    public bool FileIsEdited { get; set; } = false;
    public ArrayList Facts { get; set; } = new ArrayList();
    public ArrayList Rules { get; set; } = new ArrayList();
    public ArrayList RulesCopy { get; set; } = new ArrayList();
    public ArrayList Questions { get; set; } = new ArrayList();

    public FormDataModel()
    {
    }
}
