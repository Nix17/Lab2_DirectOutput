using Lab2_DirectOutput.Features;
using Lab2_DirectOutput.Interfaces.Services;
using Lab2_DirectOutput.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DirectOutput.Services;

public class DialogService : IDialogService
{
    public bool OpenLoadDataDialog(OpenFileDialog dialog, FormDataModel data)
    {
        dialog.Title = "Select a file with basic information";
        dialog.Filter = "Knowledge bases (*.kdb)|*.kdb|Все файлы (*.*)|*.*";
        if (dialog.ShowDialog() != DialogResult.OK) return false;

        data.FileName = dialog.FileName;
        
        using (FileStream fileStream = new FileStream(dialog.FileName, FileMode.Open))
        {
            if (fileStream == null) return false;

            try
            {
                BinaryReader br = new BinaryReader(fileStream);
                if (br.ReadInt32() != 2000590686) throw new Exception("Wrong title");

                if (br.ReadInt32() != 268435456) throw new Exception("Incorrect knowledge base version");

                int num1 = br.ReadInt32();
                data.Facts.Clear();
                for (int index = 0; index < num1; ++index)
                {
                    FactModel fact = new FactModel(0, "", "", "", 0.0, FactType.Intermediate);
                    using (FactFeatures features = new FactFeatures())
                    {
                        features.ReadFromFile(br, fact);
                        data.Facts.Add(fact);
                    }
                }

                int num2 = br.ReadInt32();
                data.Rules.Clear();
                for (int index = 0; index < num2; ++index)
                {
                    RuleModel rule = new RuleModel(0);
                    using (RuleFeatures features = new RuleFeatures())
                    {
                        features.ReadFromFile(br, rule);
                        data.Rules.Add(rule);
                    }
                }

                data.FileIsEdited = false;
                data.RulesCopy = new List<RuleModel>(data.Rules);

            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("File upload error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            fileStream.Close();
        }
        return true;
    }

    public void ShowMessage(string message)
    {
        MessageBox.Show(message);
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public void ShowSuccess(string message)
    {
        MessageBox.Show(message, "Success!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public void ShowWarning(string message)
    {
        MessageBox.Show(message, "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
