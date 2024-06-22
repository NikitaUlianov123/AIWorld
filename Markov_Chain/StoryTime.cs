using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Markov_Chain
{
    public class StoryTime
    {
        Dictionary<string, List<string>> words;

        public StoryTime()
        { 
            words = new Dictionary<string, List<string>>();
        }

        public void Build(string input)
        {
            string[] strings = input.Split(' ');
            for (int i = 0; i < strings.Length; i++)
            {
                if (!words.ContainsKey(strings[i])) words.Add(strings[i], new List<string>());
                for (int j = 0; j < 5 && i + j < strings.Length; j++)
                {
                    if (strings[i] != strings[i + j]) words[strings[i]].Add(strings[i + j]);

                }
            }
        }

        public string Generate(string input)
        {
            Random random = new Random();
            List<string> result = input.Split(' ').ToList();

            for (int i = 0; i < 100; i++)
            {
                result.Add(words[result[^1]][random.Next(words[result[^1]].Count)]);
            }

            string story = "";
            foreach (string word in result)
            {
                story += word + " ";
            }

            return story;
        }
    }
}