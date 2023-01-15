using Lab2_DirectOutput.Interfaces.Services;
using Lab2_DirectOutput.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DirectOutput.Services;

public class FactService : IFactService
{
    public void CheckFact(
        TreeView RSTree,
        Label result,
        FormDataModel formData,
        List<FactModel> checkedFacts,
        FactModel targetFact
    )
    {
        //Stopwatch sw = new Stopwatch();
        //sw.Restart();
        //output.AppendLine($"Проверяем выполнение факта '{targetFact}':");
        //output.AppendLine();
        //bool result = Compute(initialFacts, targetFact, output, out double truth);
        //output.AppendLine();

        //if (result)
        //{
        //    output.AppendLine($"Факт '{targetFact}' подтверждается с достоверностью {truth}!");
        //}
        //else
        //{
        //    output.AppendLine($"Факт '{targetFact}' не подтверждается!");
        //}
        this.Compute(
            RSTree,
            result,
            formData,
            checkedFacts,
            targetFact
            );
    }

    private void Compute(
        TreeView RSTree,
        Label result,
        FormDataModel formData,
        List<FactModel> checkedFacts,
        FactModel targetFact
    )
    {
        RSTree.Nodes.Clear();

        if (checkedFacts.Count <= 0 || targetFact == null )
        {
            result.Text = "не будет выполнятся";
        }
        else
        {
            DateTime now = DateTime.Now;
            ArrayList work_facts = new ArrayList();
            ArrayList work_truths = new ArrayList();
            foreach (FactModel fact in checkedFacts)
            {
                work_facts.Add(fact.ToString());
                work_truths.Add(fact.Truth);
            }
            RSTree.BeginUpdate();   // Отключаю перерисовку TreeView
            TreeNode node1 = new TreeNode("Проверяем, будет ли:");
            RSTree.Nodes.Add(node1);
            node1.Nodes.Add(new TreeNode(targetFact.ToString()));
            formData.Rules = new List<RuleModel>(formData.RulesCopy);

            GetDirectWorkIterations(
                work_facts,
                work_truths,
                RSTree,
                formData,
                checkedFacts,
                targetFact
                );

            TimeSpan timeSpan = DateTime.Now - now;
            string format = "Затрачено времени: {0} мсек";
            int num1 = (int)timeSpan.TotalMilliseconds;
            string str = num1.ToString();
            TreeNode node2 = new TreeNode(string.Format(format, str));
            TreeNode treeNode1 = node2;
            TreeNode node3 = new TreeNode("Результат");
            TreeNode treeNode2 = node3;
            RSTree.Nodes.Add(node3);
            TreeNode node4 = new TreeNode();
            int index = work_facts.IndexOf(targetFact.ToString());
            if (index == -1)
            {
                result.Text = "не будет";
                node4.Text = "Не будет";
                TreeNode treeNode3 = node4;
            }
            else
            {
                result.Text = "будет с достоверностью " + work_truths[index];
                node4.Text = "Будет с достоверностью " + work_truths[index];
                TreeNode treeNode3 = node4;
            }
            node3.Nodes.Add(node2);
            node3.Nodes.Add(node4);
            RSTree.ExpandAll();
            RSTree.EndUpdate();
        }
    }

    private FactModel GetFactByID(int factId, List<FactModel> facts)
    {
        foreach (FactModel fact in facts)
        {
            if (fact.Id == factId)
                return fact;
        }
        return null;
    }

    private bool CheckExpression(string exp)
    {
        Stack stack = new Stack();
        char ch1 = ' ';
        for (int index = 0; index < exp.Length; ++index)
        {

            char ch2 = exp[index];
            if (ch2 == ')')
            {
                string exp1 = "";
                while (stack.Count > 0 && (ch1 = (char)stack.Pop()) != '(')
                    exp1 += ch1.ToString();
                if (ch1 != '(')
                    return false;
                stack.Push((CheckExpression(exp1) ? '1' : '0'));
            }
            else
                stack.Push(ch2);
        }
        string str1 = "";
        while (stack.Count > 0)
            str1 += Convert.ToChar(stack.Pop()).ToString();
        string str2 = "";
        string str3 = "";
        int index1;
        for (index1 = 0; index1 < str1.Length; ++index1)
        {
            ch1 = str1[index1];
            switch (ch1)
            {
                case '|':
                case '&':
                    goto breakCicle;
                default:
                    str2 += str1[index1].ToString();
                    continue;
            }
        }

    breakCicle:
        string str4 = str2.Trim();
        if (index1 == str1.Length)
            return str4 == "1";
        char ch3 = ch1;
        for (int index2 = index1 + 1; index2 < str1.Length; ++index2)
        {
            char ch2 = str1[index2];
            switch (ch2)
            {
                case '|':
                case '&':
                    ch3 = ch2;
                    switch (ch2)
                    {
                        case '&':
                            str4 = !(str4 == "1") || !(str3 == "1") ? "0" : "1";
                            break;
                        case '|':
                            str4 = !(str4 == "0") || !(str3 == "0") ? "1" : "0";
                            break;
                    }
                    str3 = "";
                    break;
                default:
                    str3 += ch2.ToString();
                    break;
            }
        }
        string str5 = str3.Trim();
        switch (ch3)
        {
            case '&':
                return str4 == "1" && str5 == "1";
            case '|':
                return !(str4 == "0") || !(str5 == "0");
            default:
                return false;
        }
    }

    private void GetDirectWorkIterations(
        ArrayList work_facts,
        ArrayList work_truths,
        TreeView RSTree,
        FormDataModel formData,
        List<FactModel> checkedFacts,
        FactModel targetFact
    )
    {
        ArrayList arrayList1 = new ArrayList();
        ArrayList arrayList2 = new ArrayList();
        ArrayList arrayList3 = new ArrayList();
        int num1 = 0;
        bool flag1 = true;
        while (flag1)
        {
            TreeNode node1 = new TreeNode("Итерация " + (num1 + 1).ToString());
            RSTree.Nodes.Add(node1);
            TreeNode node2 = new TreeNode("Факты в памяти");
            node1.Nodes.Add(node2);
            for (int index = 0; index < work_facts.Count; ++index)
            {
                TreeNode treeNode = new TreeNode((string)work_facts[index] + " : " + ((double)work_truths[index]).ToString());
                node2.Nodes.Add(treeNode);
            }
            if (work_facts.Contains(targetFact.ToString()))
                break;
            arrayList1.Clear();
            arrayList2.Clear();
            arrayList3.Clear();
            flag1 = false;

            int count_BADDD = -1;

            double[,] truth = new double[formData.Rules.Count, 5];
            foreach (RuleModel rule in formData.Rules)
            {
                int num_work_facts = 0;
                ++count_BADDD;
                string exp = "";
                string str = "";
                for (int index = 0; index < rule.Condition.Length; ++index)
                {
                    char ch = rule.Condition[index];
                    switch (ch)
                    {
                        case '(':
                        case ')':
                        case '&':
                        case '|':
                            if (str != "")
                            {
                                char very_very_Bad = work_facts.Contains(str.Trim()) ? '1' : '0';
                                exp += very_very_Bad.ToString();

                                //for (int strNumber = 0; strNumber < strArray.Length; strNumber++)
                                //  {

                                if (very_very_Bad != '0')
                                {
                                    foreach (FactModel fact in formData.Facts)
                                    {
                                        string very_BAD = string.Concat(fact.Object, "__", fact.Attribute, "__", fact.Value).Trim();
                                        if (string.Compare(very_BAD, str.Trim()) == 0)
                                        {
                                            truth[count_BADDD, num_work_facts] = fact.Truth;
                                            ++num_work_facts;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    truth[count_BADDD, num_work_facts] = 0;
                                    ++num_work_facts;
                                }
                                //   }

                                str = "";
                                truth[count_BADDD, 4] = (int)num_work_facts;



                                /* for (int index2 = 0; index2 < rule.Conclusions.Count; ++index2)
                                 {
                                     Fact factById = GetFactByID((int)rule.Conclusions[index2]);
                                     if (factById != null)
                                     {

                                     }
                                 }*/


                            }
                            exp += ch.ToString();
                            break;
                        default:
                            str += ch.ToString();
                            break;
                    }
                }
                if (str != "")
                {
                    char very_very_Bad = work_facts.Contains(str.Trim()) ? '1' : '0';
                    exp += very_very_Bad.ToString();
                    if (very_very_Bad != '0')
                    {
                        foreach (FactModel fact in formData.Facts)
                        {
                            string very_BAD = string.Concat(fact.Object, "__", fact.Attribute, "__", fact.Value).Trim();
                            if (string.Compare(very_BAD, str.Trim()) == 0)
                            {
                                truth[count_BADDD, num_work_facts] = fact.Truth;
                                ++num_work_facts;
                                break;
                            }
                        }
                    }
                    else
                    {
                        truth[count_BADDD, num_work_facts] = 0;
                        ++num_work_facts;
                    }
                }
                if (CheckExpression(exp))
                {
                    bool flag2 = true;
                    for (int index = 0; index < rule.Conclusions.Count; ++index)
                    {
                        FactModel factById = GetFactByID((int)rule.Conclusions[index], formData.Facts);
                        if (factById != null && !work_facts.Contains(factById.ToString()))
                        {
                            flag2 = false;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        arrayList2.Add(rule);
                        arrayList3.Add(exp);
                    }
                }
            }
            RuleModel rule1 = arrayList2.Count > 0 ? (RuleModel)arrayList2[0] : null;
            string str1 = arrayList3.Count > 0 ? (string)arrayList3[0] : null;
            if (arrayList2.Count > 1)
            {
                TreeNode node3 = new TreeNode("Конфликтное множество");
                node1.Nodes.Add(node3);
                for (int index1 = 0; index1 < arrayList2.Count; ++index1)
                {
                    RuleModel rule2 = (RuleModel)arrayList2[index1];
                    TreeNode node4 = new TreeNode(rule2.Name + " : " + rule2.Truth.ToString() +
                        " [" + (string)arrayList3[index1] + "]");
                    node3.Nodes.Add(node4);
                    node4.Nodes.Add(new TreeNode(rule2.Condition));
                    for (int index2 = 0; index2 < rule2.Conclusions.Count; ++index2)
                    {
                        FactModel factById = GetFactByID((int)rule2.Conclusions[index2], formData.Facts);
                        if (factById != null)
                            node4.Nodes.Add(new TreeNode(factById.ToString()));


                    }

                }
            }
            if (rule1 != null)
            {
                TreeNode node3 = new TreeNode("Сработало правило: " + rule1.Name + " : " + rule1.Truth.ToString() +
                    " [" + str1 + "]");
                node1.Nodes.Add(node3);
                node3.Nodes.Add(new TreeNode(rule1.Condition));
                for (int index = 0; index < rule1.Conclusions.Count; ++index)
                {
                    FactModel factById = GetFactByID((int)rule1.Conclusions[index], formData.Facts);
                    if (factById != null)
                        node3.Nodes.Add(new TreeNode(factById.ToString()));
                }
                foreach (int fact_id in rule1.Conclusions)
                {

                    FactModel factById = GetFactByID(fact_id, formData.Facts);
                    if (factById != null)
                    {
                        FactModel fact = new FactModel(-1, factById.Object, factById.Attribute, factById.Value, factById.Truth, factById.Type);
                        if (arrayList1.IndexOf(fact) == -1)
                        {
                            double VERYbad = rule1.Truth;
                            fact.Truth = 99999; //max 1
                            double factTruth222 = 1;
                            if (truth[rule1.Id - 1, 4] <= 2)
                            {
                                if (truth[rule1.Id - 1, 0] < truth[rule1.Id - 1, 1])
                                    fact.Truth = truth[rule1.Id - 1, 0];
                                else
                                    fact.Truth = truth[rule1.Id - 1, 1];
                            }
                            else
                            {

                                if (truth[rule1.Id - 1, 0] != 0)
                                    fact.Truth = truth[rule1.Id - 1, 0];
                                if (truth[rule1.Id - 1, 1] != 0 &&
                                    truth[rule1.Id - 1, 1] < fact.Truth)
                                {
                                    fact.Truth = truth[rule1.Id - 1, 1];
                                }

                                if (truth[rule1.Id - 1, 2] != 0 &&
                                    truth[rule1.Id - 1, 2] < fact.Truth)
                                {
                                    fact.Truth = truth[rule1.Id - 1, 2];
                                }
                                if (truth[rule1.Id - 1, 3] != 0 &&
                                    truth[rule1.Id - 1, 3] < fact.Truth)
                                {
                                    fact.Truth = truth[rule1.Id - 1, 3];
                                }

                            }

                            if (fact.Truth > VERYbad)
                                fact.Truth = VERYbad;

                            factById.Truth = fact.Truth;
                            formData.Facts[factById.Id - 1] = factById;
                            arrayList1.Add(fact);
                        }
                    }

                }
            }
            foreach (FactModel fact in arrayList1)
            {
                int index = work_facts.IndexOf(fact.ToString());
                if (index == -1)
                {
                    work_facts.Add(fact.ToString());
                    work_truths.Add(fact.Truth);
                    flag1 = true;
                }
                else
                {

                }
            }
            ++num1;
        }
    }
}
