using AdventOfCode2022.Extensions;
using System.ComponentModel;
using System.Numerics;

namespace AdventOfCode2022.Days;

public class Day11 : IDay
{
    public string Part1(List<string> inputs)
    {
        var inputList = inputs.Split(x => string.IsNullOrEmpty(x));
        List<Monkey> monkeys = new ();
        foreach (var input in inputList)
        {
            var monkeyData = input.ToList();
            monkeys.Add(new Monkey
            {
                ItemList = new(monkeyData[1].Split(": ")[1].Split(", ").Select(long.Parse)),
                Operation = TranslateOperationType(monkeyData[2].Split(' ')[6]),
                OperationValue = monkeyData[2].Split(' ')[7],
                DivisibleBy = int.Parse(monkeyData[3].Split(' ')[5]),
                MonkeyThrowIndexIfTrue = int.Parse(monkeyData[4].Split(' ')[9]),
                MonkeyThrowIndexIfFalse = int.Parse(monkeyData[5].Split(' ')[9])
            });
        }

        for(int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                while(monkey.ItemList.Count > 0)
                {
                    var worryLevel = monkey.NewWorryLevel();
                    worryLevel /= 3;
                    monkeys[(worryLevel % monkey.DivisibleBy == 0) ?
                        monkey.MonkeyThrowIndexIfTrue :
                        monkey.MonkeyThrowIndexIfFalse].ItemList.Enqueue(worryLevel);
                    monkey.MonkeyOperationCount++;
                }                
            }
        }

        return monkeys
            .Select(x => x.MonkeyOperationCount)
            .OrderByDescending(x => x)
            .Take(2)
            .Aggregate((a, b) => a * b)
            .ToString();
    }
    
    public string Part2(List<string> inputs)
    {
        var inputList = inputs.Split(x => string.IsNullOrEmpty(x));
        List<Monkey> monkeys = new ();
        foreach (var input in inputList)
        {
            var monkeyData = input.ToList();
            monkeys.Add(new Monkey
            {
                ItemList = new(monkeyData[1].Split(": ")[1].Split(", ").Select(long.Parse)),
                Operation = TranslateOperationType(monkeyData[2].Split(' ')[6]),
                OperationValue = monkeyData[2].Split(' ')[7],
                DivisibleBy = int.Parse(monkeyData[3].Split(' ')[5]),
                MonkeyThrowIndexIfTrue = int.Parse(monkeyData[4].Split(' ')[9]),
                MonkeyThrowIndexIfFalse = int.Parse(monkeyData[5].Split(' ')[9])
            });
        }

        var leastCommonMultiple = monkeys.Select(monkey => monkey.DivisibleBy).Aggregate((a, b) => a * b);

        for (int i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.ItemList.Count > 0)
                {
                    var worryLevel = monkey.NewWorryLevel();
                    worryLevel %= leastCommonMultiple;
                    monkeys[(worryLevel % monkey.DivisibleBy == 0) ?
                        monkey.MonkeyThrowIndexIfTrue :
                        monkey.MonkeyThrowIndexIfFalse].ItemList.Enqueue(worryLevel);
                    monkey.MonkeyOperationCount++;
                }
            }
        }

        return monkeys
            .Select(x => x.MonkeyOperationCount)
            .OrderByDescending(x => x)
            .Take(2)
            .Aggregate((a, b) => a * b)
            .ToString();
    }

    private static OperationType TranslateOperationType(string operation)
    {
        return operation switch
        {
            "+" => OperationType.Add,
            "-" => OperationType.Subtract,
            "*" => OperationType.Multiply,
            "/" => OperationType.Divide,
            _ => OperationType.Add
        };
    }
}

public class Monkey
{
    public Queue<long> ItemList { get; set; }
    public OperationType Operation { get; set; }
    public string OperationValue { get; set; }
    public int DivisibleBy { get; set; }
    public int MonkeyThrowIndexIfTrue { get; set; }
    public int MonkeyThrowIndexIfFalse { get; set; }
    public long MonkeyOperationCount { get; set; } = 0;
    public long NewWorryLevel()
    {
        var item = ItemList.Dequeue();
        
        if (!long.TryParse(OperationValue, out long operationValue))
        {
            operationValue = item;
        }

        return Operation switch
        {
            OperationType.Add => item + operationValue,
            OperationType.Subtract => item - operationValue,
            OperationType.Multiply => item * operationValue,
            OperationType.Divide => item * operationValue,
            _ => operationValue
        };
    }
}

public enum OperationType
{
    Add,
    Subtract,
    Divide,
    Multiply
}