using Lab2_DirectOutput.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DirectOutput.Interfaces.Services;

public interface IDialogService
{
    void ShowMessage(string message);
    void ShowError(string message);
    void ShowSuccess(string message);
    void ShowWarning(string message);
    bool OpenLoadDataDialog(OpenFileDialog dialog, FormDataModel data);
}
