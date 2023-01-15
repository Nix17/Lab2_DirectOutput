using Lab2_DirectOutput.Interfaces.Services;
using Lab2_DirectOutput.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Lab2_DirectOutput;

public partial class Form1 : Form
{
    private FormDataModel _formData;
    private IDialogService _dialogSrv;
    private IFactService _factSrv;

    public Form1(
        IDialogService dialogSrv,
        IFactService factSrv
    )
    {
        InitializeComponent();
        _dialogSrv = dialogSrv;
        _factSrv = factSrv;
        _formData = new FormDataModel();
        splitContainer1.Panel2.Enabled = false;
    }

    private void UpdateFactsCheckedListBox()
    {
        initialFactsCheckedListBox.Items.Clear();
        targetFactComboBox.Items.Clear();
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

    private void drawConclusionBtn_Click(object sender, EventArgs e)
    {
        try
        {
            if (initialFactsCheckedListBox.CheckedItems.Count == 0) throw new Exception("You need to choose the facts!");

            List<FactModel> initialFacts = new List<FactModel>();
            foreach (FactModel item in initialFactsCheckedListBox.CheckedItems)
            {
                initialFacts.Add(item);
            }
            FactModel targetFact = (FactModel)targetFactComboBox.SelectedItem;

            StringBuilder sb = new StringBuilder();
            treeViewResult.Nodes.Clear();
            this._factSrv.CheckFact(treeViewResult, ResultLabel, _formData, initialFacts, targetFact);
        }
        catch (Exception ex)
        {
            _dialogSrv.ShowError(ex.Message);
        }
    }
}