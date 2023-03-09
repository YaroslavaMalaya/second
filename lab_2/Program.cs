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

    while (neighbours.Count > 0)
    {
        if (neighbours.Count == 1)
        {
            var point = neighbours.Dequeue();
            foreach (var n in generator.GetNeighbours(point.Column, point.Row, map, 1))
            {
                if (!visited.Contains(n))
                {
                    neighbours.Enqueue(n);
                    visited.Add(n);
                    Console.WriteLine((n.Row, n.Column));
                }
            }
        }
    
        if (neighbours.Count == 2)
        {
            var point_1 = neighbours.Dequeue();
            var point_2 = neighbours.Dequeue();
            foreach (var n in generator.GetNeighbours(point_1.Column, point_1.Row, map, 1))
            {
                if (!visited.Contains(n))
                {
                    neighbours.Enqueue(n);
                    visited.Add(n);
                    Console.WriteLine((n.Row, n.Column));
                }
            }
            foreach (var n in generator.GetNeighbours(point_2.Column, point_2.Row, map, 1))
            {
                if (!visited.Contains(n))
                {
                    neighbours.Enqueue(n);
                    visited.Add(n);
                    Console.WriteLine((n.Row, n.Column));
                }
            }
        }
        
        if (neighbours.Count == 3)
        {
            var point_1 = neighbours.Dequeue();
            var point_2 = neighbours.Dequeue();
            var point_3 = neighbours.Dequeue();
            foreach (var n in generator.GetNeighbours(point_1.Column, point_1.Row, map, 1))
            {
                if (!visited.Contains(n))
                {
                    neighbours.Enqueue(n);
                    visited.Add(n);
                    Console.WriteLine((n.Row, n.Column));
                }
            }
            foreach (var n in generator.GetNeighbours(point_2.Column, point_2.Row, map, 1))
            {
                if (!visited.Contains(n))
                {
                    neighbours.Enqueue(n);
                    visited.Add(n);
                    Console.WriteLine((n.Row, n.Column));
                }
            }
            foreach (var n in generator.GetNeighbours(point_3.Column, point_3.Row, map, 1))
            {
                if (!visited.Contains(n))
                {
                    neighbours.Enqueue(n);
                    visited.Add(n);
                    Console.WriteLine((n.Row, n.Column));
                }
            }
        }
        
        if (neighbours.Count == 4)
        {
            var point_1 = neighbours.Dequeue();
            var point_2 = neighbours.Dequeue();
            var point_3 = neighbours.Dequeue();
            var point_4 = neighbours.Dequeue();
            foreach (var n in generator.GetNeighbours(point_1.Column, point_1.Row, map, 1))
            {
                if (!visited.Contains(n))
                {
                    neighbours.Enqueue(n);
                    visited.Add(n);
                    Console.WriteLine((n.Row, n.Column));
                }
            }
            foreach (var n in generator.GetNeighbours(point_2.Column, point_2.Row, map, 1))
            {
                if (!visited.Contains(n))
                {
                    neighbours.Enqueue(n);
                    visited.Add(n);
                    Console.WriteLine((n.Row, n.Column));
                }
            }
            foreach (var n in generator.GetNeighbours(point_3.Column, point_3.Row, map, 1))
            {
                if (!visited.Contains(n))
                {
                    neighbours.Enqueue(n);
                    visited.Add(n);
                    Console.WriteLine((n.Row, n.Column));
                }
            }
            foreach (var n in generator.GetNeighbours(point_4.Column, point_4.Row, map, 1))
            {
                if (!visited.Contains(n))
                {
                    neighbours.Enqueue(n);
                    visited.Add(n);
                    Console.WriteLine((n.Row, n.Column));
                }
            }
        }
    }
    if (visited.Contains(goal))
    {
        Console.WriteLine("goal");
    }

    return visited; // щоб червоним не підкреслювало поки не написана програма
}


//GetShortestPath(map, start, goal);
//foreach (var element in GetShortestPath(map, start, goal))
{
    //Console.WriteLine((element.Row, element.Column));
}            