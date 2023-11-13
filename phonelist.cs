using System;

public static class Program

{
    public static bool Solve (int phoneNumberCount, string[] phoneNumbers)

    {

        var trie = new Trie();
        for (int i = 0; i < phoneNumberCount; ++i)

        {
            if (trie.Add(phoneNumbers[i]))
                return false;
        }

        return true;

    }
}


public sealed class Trie

{

    private Node _root = new Node((char)0);


    public bool Add(string word)

    {
        bool isPrefixedByAWord = false;
        bool isPrefixOfAWord = false;

        Node currentNode = _root;
        Node nextNode;

        int index = 0;

        while (index < word.Length && currentNode.Children.TryGetValue(word[index], out nextNode))

        {
            currentNode = nextNode;

            ++index;

            if (currentNode.IsAWordEnd)

            {

                isPrefixedByAWord = true;

            }
        }

        if (index == word.Length)

        {
            if (currentNode.Children.Count != 0)

            {

                isPrefixOfAWord = true;
            }
        }

        else

        {

            while (index < word.Length)

            {

                nextNode = new Node(word[index]);
                currentNode.Children.Add(word[index], nextNode);
                currentNode = nextNode;
                ++index;

            }
        }


        currentNode.IsAWordEnd = true;

        return isPrefixedByAWord || isPrefixOfAWord;

    }

    private sealed class Node

    {
        public Node(char value)

        {
            Value = value;
        }


        public char Value { get; }
        public bool IsAWordEnd { get; set; }
        public Dictionary<char,Node> Children { get; } = new Dictionary<char,Node>();

    }
}

public static class PhoneList

{
    private static void Main()

    {
        string[] phoneNumbers = new string[10000];
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)

        {
            int phoneNumberCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < phoneNumberCount; ++i)

            {
                phoneNumbers[i] = Console.ReadLine();
            }

            Console.WriteLine(

                Program.Solve(phoneNumberCount, phoneNumbers) ? "YES" : "NO");

        }
    }
}