using Kse.Algorithms.Samples;

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 10,
});

string[,] map = generator.Generate();
new MapPrinter().Print(map);
var (start, goal) = generator.GetPoints(map);
Console.WriteLine((goal.Column, goal.Row));
Console.WriteLine("___________");
var path = GetShortestPath(map, start, goal);
new MapPrinter().Print(map);

List<Point> GetShortestPath(string[,] map, Point start, Point goal)
{
    var distances = new Dictionary<Point, int>();
    var origins = new Dictionary<Point, Point>();

    distances[start] = 0;
    origins[start] = start;
    var point = start;
    while (!(goal.Column == point.Column && goal.Row == point.Row))
    {
        foreach (var n in generator.GetNeighbours(point.Column, point.Row, map, 1, true))
        {
            if (!origins.ContainsKey(n))
            {
                origins[n] = point;
                distances[n] = 1 + distances[point];
            }
        }

        distances.Remove(point);
        point = distances.MinBy(pair => pair.Value).Key; 
    }

    List<Point> path = new List<Point>();
    while ((point.Column, point.Row) != (start.Column, start.Row))
    {
        path.Add(point);
        point = origins[point];
    }

    return path; // щоб червоним не підкреслювало поки не написана програма
}

foreach (var ppp in path)
{
    Console.WriteLine((ppp.Column, ppp.Row));
}