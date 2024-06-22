namespace Markov_Chain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText("C:\\Users\\test\\Downloads\\J. K. Rowling - Harry Potter 4 - The Goblet of Fire.txt");
            string bible = File.ReadAllText("C:\\Users\\test\\Downloads\\Bible.txt");
            var markov = new StoryTime();
            markov.Build(text);
            markov.Build(bible);
            markov.Build(bible);
            markov.Build(bible);
            Console.WriteLine(markov.Generate("Harry"));
        }
    }
}