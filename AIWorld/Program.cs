namespace AIWorld
{
    internal class Program
    {
        //Todo:
        //make cost track
        //get shit to kinda work (DONE!!!!1)
        //do a* and heuristic (mostly done, finish later)

        //agents and search without graph
        static void Main(string[] args)
        {
            Graph<int> grafy = new Graph<int>();

            grafy.AddVertex(1);
            grafy.AddVertex(7);
            grafy.AddVertex(12);
            grafy.AddVertex(14);
            grafy.AddVertex(19);
            grafy.AddVertex(21);
            grafy.AddVertex(31);
            grafy.AddVertex(67);


            grafy.AddEdge(1, 7, 4);
            grafy.AddEdge(1, 12, 3);
            grafy.AddEdge(1, 21, 12);
            grafy.AddEdge(7, 21, 13);
            grafy.AddEdge(12, 19, 16);
            grafy.AddEdge(19, 1, 3);
            grafy.AddEdge(19, 21, 2);
            grafy.AddEdge(21, 14, 23);
            grafy.AddEdge(21, 31, 14);

            var yeet = grafy.Search(1, 31, new Frontier<int>(), Searchs.UCS);
        }
    }
}