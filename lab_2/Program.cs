using Kse.Algorithms.Samples;

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 10,
});

string[,] map = generator.Generate();
var (start, goal) = generator.GetPoints(map);
var path = GetShortestPath(map, start, goal);
new MapPrinter().Print(map);

List<Point> GetShortestPath(string[,] map, Point start, Point goal)
{
    var distances = new Dictionary<Point, int>();
    var origins = new Dictionary<Point, Point>();
    var visited = new List<Point>();
    var neighbours = new Queue<Point>();

    distances[start] = 0;
    visited.Add(start);
    
    foreach (var n in generator.GetNeighbours(start.Column, start.Row, map, 1)) // але це тільки для стартової точки
    {
        if (!visited.Contains(n))
        {
            neighbours.Enqueue(n);
            visited.Add(n);
            distances[n] = 1;
            origins[start] = n;
        }   
    }

    return visited; // щоб червоним не підкреслювало поки не написана програма
}