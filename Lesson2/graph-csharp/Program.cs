Graph graph = new Graph();

// Aggiungi nodi e archi al grafo
graph.AddEdge(1, 2);
graph.AddEdge(1, 3);
graph.AddEdge(3, 4);
graph.AddEdge(5, 6);

Console.WriteLine("Grafo:");
graph.PrintGraph();

Console.WriteLine("\n1. Nodi 1 e 4 connessi?");
Console.WriteLine(graph.AreConnected(1, 4));

Console.WriteLine("\n1. Nodi 1 e 6 connessi?");
Console.WriteLine(graph.AreConnected(1, 6));


Console.WriteLine("\n2. Nodi connessi a 1:");
Console.WriteLine(string.Join(", ", graph.GetConnectedNodes(1)));

Console.WriteLine("\n3. Numero di componenti connesse:");
Console.WriteLine(graph.CountConnectedComponents());

Console.WriteLine("\n4. Grado del nodo 1:");
Console.WriteLine(graph.GetDegree(1));

Console.WriteLine("\n5. Cammino tra 1 e 4:");
var path = graph.FindPath(1, 4);
Console.WriteLine(path != null ? string.Join(" -> ", path) : "Nessun cammino trovato");