using Kse.Algorithms.Samples;

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 10,
});

string[,] map = generator.Generate();
var (start, goal) = generator.GetPoints(map);
new MapPrinter().Print(map);
// Console.Write(map[10, 1]);
// Console.WriteLine((start.Column, start.Row, goal.Column, goal.Row));

List<Point> GetShortestPath(string[,] map, Point start, Point goal)
{
    var distances = new Dictionary<Point, int>();
    var origins = new Dictionary<Point, Point>();
    var visited = new List<Point>();
    var check = new List<Point>(); // щоб код червоним
    var neighbours = new Queue<Point>();

    distances[start] = 0;
    visited.Add(start);

    // while (unvisited.Count > 0)
    //{
    foreach (var n in generator.GetNeighbours(start.Column, start.Row, map, 1)) // але це тільки для стартової точки
    {
        if (!visited.Contains(n))
        {
            neighbours.Enqueue(new Point(n.Row, n.Column));
            visited.Add(n);
            distances[new Point(n.Row, n.Column)] = 1;
            origins[start] = new Point(n.Row, n.Column);
            check.Add(new Point(n.Row, n.Column));
        }   
    }
    //}

    return visited; // щоб червоним не підкреслювало поки не написана програма
}


// це для себе я перевірила як працює отримання сусідів

GetShortestPath(map, start, goal);
    
foreach (var element in generator.GetNeighbours(start.Column, start.Row, map, 1))
{
    Console.WriteLine(element.Column);
    Console.WriteLine(element.Row);
}