using Kse.Algorithms.Samples;

var check = true;
var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 35,
    Width = 90,
    Noise = .1f,
    AddTraffic = check,
    TrafficSeed = 1234
});

string[,] map = generator.Generate();
var (start, goal) = generator.GetPoints(map);
var path = GetShortestPath(map, start, goal, check);
new MapPrinter().Print(map, path);

List<Point> GetShortestPath(string[,] map, Point start, Point goal, bool check)
{
    var distances = new Dictionary<Point, int>();
    var origins = new Dictionary<Point, Point>();
    double time = 0;
    double hours = 0;

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
        if (check)
        {
            if ((point.Column, point.Row) != (start.Column, start.Row))
            {
                var number = map[point.Column, point.Row];
                double v = 60 - (int.Parse(number) - 1) * 6;
                time += (1 / v) * 60; // in minutes
            }
        }
    }
    
    Console.WriteLine($"\nPath length: {distances[goal]} km");
    if (check)
    {
        while (time > 60.0)
        {
            hours += Math.Round(time / 60, 0);
            time -= 60;
        }
        Console.WriteLine($"Time: {hours} hours {Math.Round(time, 0)} minutes\n");
    }
    
    return path;
}