using Kse.Algorithms.Samples;

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 10,
});

string[,] map = generator.Generate();
var (start, goal) = generator.GetPoints(map);
var path = GetShortestPath(map, start, goal);
new MapPrinter().Print(map, path);

List<Point> GetShortestPath(string[,] map, Point start, Point goal)
{
    var distances = new Dictionary<Point, int>();
    var origins = new Dictionary<Point, Point>();

    distances[start] = 0;
    origins[start] = start;
    var point = start;
    while ((point.Column, point.Row) != (goal.Column, goal.Row))
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
        if ((point.Column, point.Row) == (goal.Column, goal.Row)) // щоб не передавати точку goal у список шляху
        {
            point = origins[point]; 
        }
        path.Add(point);
        point = origins[point];
    }
    
    Console.WriteLine($"\nPath lenth: {distances[goal]}\n");
    
    return path;
}