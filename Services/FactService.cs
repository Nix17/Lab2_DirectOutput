using Lab2_DirectOutput.Interfaces.Services;
using Lab2_DirectOutput.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DirectOutput.Services;

public class FactService : IFactService
{
    public void CheckFact(List<FactModel> initialFacts, FactModel targetFact, StringBuilder output)
    {
        Stopwatch sw = new Stopwatch();
        sw.Restart();
        output.AppendLine($"Проверяем выполнение факта '{ targetFact }':");
        output.AppendLine();
        bool result = Compute(initialFacts, targetFact, output, out double truth);
        output.AppendLine();

        if (result)
        {
            output.AppendLine($"Факт '{ targetFact }' подтверждается с достоверностью { truth }!");
        }
        else
        {
            output.AppendLine($"Факт '{ targetFact }' не подтверждается!");
        }
    }

    private bool Compute(
        List<FactModel> initialFacts,
        FactModel targetFact,
        StringBuilder output,
        out double truth,
        int level = 1
    )
    {
        throw new NotImplementedException();
    }
}
