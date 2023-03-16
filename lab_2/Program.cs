using System.Diagnostics;
using Kse.Algorithms.Samples;

var check = false; // true - з заторами, false - без заторів
var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 35,
    Width = 90,
    Noise = .1f,
    AddTraffic = check,
    TrafficSeed = 1234
});

var count = 0;
while (count < 4)
{
    Console.ForegroundColor = ConsoleColor.White;
    string[,] map = generator.Generate();
    var (start, goal) = generator.GetPoints(map);
    var pathDijkstra = GetShortestPathDijkstra(map, start, goal, check);
    var pathA = GetShortestPathA(map, start, goal);
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Dijkstra:");
    Console.ForegroundColor = ConsoleColor.White;
    new MapPrinter().Print(map, pathDijkstra);
    Console.WriteLine("------------------------------");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("A*:");
    Console.ForegroundColor = ConsoleColor.White;
    new MapPrinter().Print(map, pathA);
    count++;
}

List<Point> GetShortestPathA(string[,] map, Point start, Point goal)
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    double Heuristic(Point point)
    {
        return Math.Sqrt(Math.Pow(goal.Column - point.Column, 2) + Math.Pow(goal.Row - point.Row, 2));
    }
    var open = new PriorityQueue<Point, double>();
    var origins = new Dictionary<Point, Point>();
    var steps = 0;
    var distances = new Dictionary<Point, double>();
    var distancesHeuristic = new Dictionary<Point, double>();
    open.Enqueue(start, 0);
    distances[start] = 0;
    distancesHeuristic[start] = Heuristic(start);

    while (open.Count > 0)
    {
        var current = open.Dequeue();
        // перевірка чи досягли ми фінішу
        if ((current.Column,current.Row) == (goal.Column,goal.Row))
        {
            var path = new List<Point>();
            var point = current;
            while ((point.Column,point.Row) != (start.Column,start.Row))
            {
                path.Add(point);
                point = origins[point];
            }
            path.Reverse();
            stopwatch.Stop();
            Console.WriteLine($"Steps in A*: {steps}");
            Console.WriteLine($"Time: {stopwatch.Elapsed}");
            //Console.WriteLine($"Ticks: {stopwatch.ElapsedTicks}");
            Console.WriteLine($"Milliseconds: {stopwatch.ElapsedMilliseconds} \n");
            return path;
        }
        
        foreach (var neighbor in generator.GetNeighbours(current.Column, current.Row, map, 1, true))
        {
            var tentativeDistance = distances[current] + 1;
            if (!distances.ContainsKey(neighbor) || tentativeDistance < distances[neighbor])
            {
                distances[neighbor] = tentativeDistance;
                distancesHeuristic[neighbor] = tentativeDistance + Heuristic(neighbor);
                open.Enqueue(neighbor, distancesHeuristic[neighbor]);
                origins[neighbor] = current;
            }
        }
        steps++;
    }
    return null;
}

List<Point> GetShortestPathDijkstra(string[,] map, Point start, Point goal, bool check)
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    var distances = new Dictionary<Point, int>();
    var origins = new Dictionary<Point, Point>();
    var point = start;
    double time = 0;
    double hours = 0;
    var steps = 0;
    
    distances[start] = 0;
    origins[start] = start;

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
        steps++;
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
        steps++;
    }
    
    Console.WriteLine($"\nPath length: {distances[goal]} km");
    if (check)
    {
        while (time > 60.0)
        {
            hours += Math.Round(time / 60, 0);
            time -= 60;
        }
        Console.WriteLine($"Time: {hours} hours {Math.Round(time, 0)} minutes");
    }
    stopwatch.Stop();
    Console.WriteLine($"\nSteps in Dijkstra: {steps}");
    Console.WriteLine($"Time: {stopwatch.Elapsed}");
    //Console.WriteLine($"Ticks: {stopwatch.ElapsedTicks}");
    Console.WriteLine($"Milliseconds: {stopwatch.ElapsedMilliseconds} \n");
    return path;
}