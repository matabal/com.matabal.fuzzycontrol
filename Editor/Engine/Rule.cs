using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace FuzzyEngine
{
    using Exceptions;
    public class Rule
    {
        RuleNode root;
        bool thenNOT;
        Literal thenCondition;
        private string ruleString;
        private Literal[] inferenceValues;
        

        public Rule(string ruleString)
        {
            StatementValue[] tokenizedRule = Rule.Tokenize(ruleString);
            int thenIndex = 0;
            if (tokenizedRule[tokenizedRule.Length-2] is NOT)
            {
                thenNOT = true;
                thenIndex = tokenizedRule.Length - 3;
            }
            else
            {
                thenNOT = false;
                thenIndex = tokenizedRule.Length - 2;
            }
            thenCondition = (Literal)tokenizedRule[tokenizedRule.Length - 1];
            StatementValue[] ifClause = tokenizedRule.Skip(1).Take(thenIndex - 1).ToArray();
            StatementValue[] posfix = LexPosfix(ifClause);

            int i = 0;
            Stack<RuleNode> stack = new Stack<RuleNode>();
            stack.Push(new RuleNode(posfix[i]));
            i++;
            while (i < posfix.Length)
            {
                if (posfix[i] is Literal)
                {
                    stack.Push(new RuleNode(posfix[i]));
                }
                else if (posfix[i] is Operator)
                {
                    if (posfix[i] is NOT)
                    {
                        RuleNode right = stack.Pop();
                        RuleNode current = new RuleNode(posfix[i]);
                        current.right = right;
                        stack.Push(current);
                    }
                    else
                    {
                        RuleNode right = stack.Pop();
                        RuleNode left = stack.Pop();
                        RuleNode current = new RuleNode(posfix[i]);
                        current.left = left;
                        current.right = right;
                        stack.Push(current);
                    }
                }
                else
                    throw new InvalidRuleException();

                i++;
            }
            root = stack.Pop();

        }

        public Literal Infer(Literal[] values)
        {
            inferenceValues = values;
            InsertLiterals(root);
            EvaluateTree(root);
            Literal result = new Literal(thenCondition.variable, thenCondition.descriptor);
            if (thenNOT)
                result.SetFuzzyValue(1f - ((Literal)root.value).fuzzyValue);
            else
                result.SetFuzzyValue(((Literal)root.value).fuzzyValue);

            return result;
        }

        public string GetRuleString()
        {
            ruleString = "IF ";
            InOrderPrintTraversal(root);
            if (thenNOT)
                ruleString += "THEN (NOT ";
            else
                ruleString += "THEN (";
            ruleString += thenCondition.ToString() + ")";
            return ruleString;
        }

        private void InOrderPrintTraversal(RuleNode node)
        {
            if (node == null)
                return;

            if (node.left != null && node.right != null)
                this.ruleString += " (";
            InOrderPrintTraversal(node.left);
            this.ruleString += node.value.ToString() + " ";
            InOrderPrintTraversal(node.right);
            if (node.left != null && node.right != null)
                this.ruleString += ") ";
        }

        private void InsertLiterals(RuleNode node)
        {
            if (node == null)
                return;

            
            InsertLiterals(node.left);
            InsertLiterals(node.right);
            if (node.left == null && node.right == null)
            {
                foreach (Literal lit in inferenceValues)
                {
                    if (lit.variable.Equals(((Literal)node.value).variable)
                        && lit.descriptor.Equals(((Literal)node.value).descriptor))
                    {
                        ((Literal)node.value).SetFuzzyValue(lit.fuzzyValue);
                    }
                }

                if (node.value is Literal)
                {
                    if (!((Literal)node.value).fuzzyValueAdded)
                        throw new MissingFuzzyValue();

                }
            }

        }

        private void EvaluateTree(RuleNode node)
        {
            if (node == null)
                return;

            EvaluateTree(node.left);
            EvaluateTree(node.right);
            if (node.left != null || node.right != null)
            {
                if (node.left != null && node.right != null)
                {
                    if (node.left.value is Literal && node.right.value is Literal)
                    {
                        List<Literal> childValues = new List<Literal>();
                        childValues.Add((Literal)node.left.value);
                        node.left = null;
                        childValues.Add((Literal)node.right.value);
                        node.right = null;
                        Literal newValue = ((Operator)node.value).Compose(childValues);
                        node.value = newValue;
                    }
                }
                else if (node.left != null && node.right == null)
                {
                    if (node.left.value is Literal)
                    {
                        List<Literal> childValues = new List<Literal>();
                        childValues.Add((Literal)node.left.value);
                        node.left = null;
                        Literal newValue = ((Operator)node.value).Compose(childValues);
                        node.value = newValue;
                    }
                }
                else if (node.right != null && node.left == null)
                {
                    if (node.right.value is Literal)
                    {
                        List<Literal> childValues = new List<Literal>();
                        childValues.Add((Literal)node.right.value);
                        node.right = null;
                        Literal newValue = ((Operator)node.value).Compose(childValues);
                        node.value = newValue;
                    }
                }

            }
        }

        private static int Precedence(StatementValue value)
        {
            if (value is NOT)
                return 3;
            if (value is AND)
                return 2;
            if (value is OR)
                return 1;
            return 0;
        }

        public static StatementValue[] LexPosfix(StatementValue[] tokenized)
        {
            List<StatementValue> result = new List<StatementValue>();
            Stack<StatementValue> stack = new Stack<StatementValue>();

            for (int i = 0; i < tokenized.Length; i++)
            {
                StatementValue val = tokenized[i];
                if (val is Literal)
                    result.Add(val);
                else if (val is LeftParenthesis)
                    stack.Push(val);
                else if (val is RightParenthesis)
                {
                    while (stack.Count > 0 && !(stack.Peek() is LeftParenthesis))
                    {
                        result.Add(stack.Pop());
                    }

                    if (stack.Count > 0 && !(stack.Peek() is LeftParenthesis))
                    {
                        throw new InvalidRuleException(); // invalid
                                                     // expression
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
                else
                {
                    while (stack.Count > 0 && Precedence(val) <= Precedence(stack.Peek()))
                    {
                        result.Add(stack.Pop());
                    }
                    stack.Push(val);
                }
            }

            while (stack.Count > 0)
            {
                result.Add(stack.Pop());
            }

            return result.ToArray();
        }


        public static StatementValue[] Tokenize(string ruleString)
        {
            Match match = Regex.Match(
                ruleString,
                @"^(?i)if\s+((\(*\s*\w+\s+is\s+(not\s+)?\w+\s*\)*)(\s+(and|or)\s+\(*\s*\w+\s+is\s+(not\s+)?\w+\s*\)*)*)\s+then\s+\(*\s*\w+\s+is\s+(not\s+)?\w+\s*\)*$"
            );
            if (!match.Success)
                throw new InvalidRuleException();

            List<StatementValue> tokenized = new List<StatementValue>();
            string[] splitted = Regex.Matches(ruleString, @"(?i)(if|\(|and|or|(\w+\s+is\s+(not\s+)?\w+)|\)|then)")
                .Cast<Match>().Select(m => m.Value).ToArray();
            
            if (splitted[0].ToLower() != "if")
                throw new InvalidRuleException();
            tokenized.Add(new IfStatement());

            string current = "";
            int i = 1;
            int parenCount = 0;
            while (splitted[i].ToLower() != "then" && i < splitted.Length)
            {
                current = splitted[i];
                if (current.Equals("("))
                {
                    tokenized.Add(new LeftParenthesis());
                    parenCount++;
                }
                else if (current.Equals(")"))
                {
                    tokenized.Add(new RightParenthesis());
                    parenCount--;
                }
                else if (current.ToLower().Equals("and"))
                {
                    tokenized.Add(new AND());
                }
                else if (current.ToLower().Equals("or"))
                {
                    tokenized.Add(new OR());
                }
                else
                {
                    string[] isClause = Regex.Split(splitted[i], @"(?i)\s+is\s+");
                    Variable variable = new Variable(isClause[0]);
                    string[] notClause = Regex.Split(isClause[1], @"not");
                    if (notClause.Length > 1)
                    {
                        tokenized.Add(new NOT());
                        Descriptor descriptor = new Descriptor(notClause[1]);
                        tokenized.Add(new Literal(variable, descriptor));

                    }
                    else
                    {
                        Descriptor descriptor = new Descriptor(isClause[1]);
                        tokenized.Add(new Literal(variable, descriptor));
                    }
                }

                i++;
            }
            
            if (parenCount != 0)
                throw new InvalidRuleException();

            tokenized.Add(new ThenStatement());
            i++;
            string[] thenSplit = Regex.Split(splitted[i], @"(?i)\s+is\s+");
            Variable thenVar = new Variable(thenSplit[0]);
            string[] notSplit = Regex.Split(thenSplit[1], @"not");
            if (notSplit.Length > 1)
            {
                tokenized.Add(new NOT());
                Descriptor thenDescriptor = new Descriptor(notSplit[1]);
                tokenized.Add(new Literal(thenVar, thenDescriptor));
               
            }
            else
            {
                Descriptor thenDescriptor = new Descriptor(thenSplit[1]);
                tokenized.Add(new Literal(thenVar, thenDescriptor));
            }

            return tokenized.ToArray();
           
        }
    }
}