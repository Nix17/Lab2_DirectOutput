using Lab2_DirectOutput.Interfaces.Services;
using Lab2_DirectOutput.Models;

namespace Lab2_DirectOutput;

public partial class Form1 : Form
{
    private FormDataModel _formData;
    private IDialogService _dialogSrv;

    public Form1(IDialogService dialogSrv)
    {
        InitializeComponent();
        _formData = new FormDataModel();
        _dialogSrv = dialogSrv;
    }
}