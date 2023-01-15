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
    public List<FactModel> Facts { get; set; } = new List<FactModel>();
    public List<RuleModel> Rules { get; set; } = new List<RuleModel>();
    public List<RuleModel> RulesCopy { get; set; } = new List<RuleModel>();
    public ArrayList Questions { get; set; } = new ArrayList();

    public FormDataModel()
    {
    }
}
