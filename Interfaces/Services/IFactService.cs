using Lab2_DirectOutput.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DirectOutput.Interfaces.Services;

public interface IFactService
{
    //void CheckFact(List<FactModel> initialFacts, FactModel targetFact, StringBuilder output);
    void CheckFact(
        TreeView RSTree,
        Label result,
        FormDataModel formData,
        List<FactModel> checkedFacts,
        FactModel targetFact
    );
}
