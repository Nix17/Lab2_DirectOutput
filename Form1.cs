using Lab2_DirectOutput.Interfaces.Services;
using Lab2_DirectOutput.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

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
        splitContainer1.Panel2.Enabled = false;
    }

    private void UpdateFactsCheckedListBox()
    {
        initialFactsCheckedListBox.Items.Clear();
        foreach (var fact in _formData.Facts)
        {
            if (fact.Type == FactType.ForQuestion) initialFactsCheckedListBox.Items.Add(fact, true);
            else if (fact.Type == FactType.ForResult) targetFactComboBox.Items.Add(fact);
        }

        if (targetFactComboBox.Items.Count > 0) targetFactComboBox.SelectedIndex = 0;
    }

    private void btnUploadFile_Click(object sender, EventArgs e)
    {
        if (_dialogSrv.OpenLoadDataDialog(openFileDialog1, _formData))
        {
            splitContainer1.Panel2.Enabled = true;

            this.UpdateFactsCheckedListBox();
        }
    }

    private void checkAll_Click(object sender, EventArgs e)
    {
        for(int i = 0; i < initialFactsCheckedListBox.Items.Count; ++i) initialFactsCheckedListBox.SetItemChecked(i, true);
    }

    private void uncheckAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < initialFactsCheckedListBox.Items.Count; ++i) initialFactsCheckedListBox.SetItemChecked(i, false);
    }
}