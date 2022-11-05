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
        Literal thenCondition;

        public Rule(string ruleString)
        {
            StatementValue[] tokenizedRule = Rule.Tokenize(ruleString);
        }

        public Literal Infer(Literal[] values)
        {
            return new Literal(thenCondition.variable, new Descriptor(""), 0f);
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