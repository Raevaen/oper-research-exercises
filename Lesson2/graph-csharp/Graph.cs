class Graph
{
    private Dictionary<int, List<int>> adjacencyList;

    public Graph()
    {
        adjacencyList = [];
    }

    // Aggiunge un nodo al grafo
    public void AddNode(int node)
    {
        if (!adjacencyList.ContainsKey(node))
        {
            adjacencyList[node] = new List<int>();
        }
    }

    // Aggiunge un arco tra due nodi
    public void AddEdge(int u, int v)
    {
        AddNode(u);
        AddNode(v);
        adjacencyList[u].Add(v);
        adjacencyList[v].Add(u); // Per grafo non orientato
    }

    // Controlla se due nodi sono connessi
    public bool AreConnected(int u, int v)
    {
        var visited = new HashSet<int>();
        return DFS(u, visited).Contains(v);
    }

    // Trova tutti i nodi connessi a un dato nodo
    public List<int> GetConnectedNodes(int u)
    {
        var visited = new HashSet<int>();
        return DFS(u, visited);
    }

    // Conta il numero di componenti connesse
    public int CountConnectedComponents()
    {
        var visited = new HashSet<int>();
        int count = 0;

        foreach (var node in adjacencyList.Keys)
        {
            if (!visited.Contains(node))
            {
                DFS(node, visited);
                count++;
            }
        }

        return count;
    }

    // Calcola il grado di un nodo
    public int GetDegree(int u)
    {
        if (adjacencyList.ContainsKey(u))
        {
            return adjacencyList[u].Count;
        }
        return 0;
    }

    // Trova un cammino tra due nodi (se esiste)
    public List<int> FindPath(int u, int v)
    {
        var visited = new HashSet<int>();
        var path = new List<int>();
        if (DFSFindPath(u, v, visited, path))
        {
            return path;
        }
        return null; // Nessun cammino trovato
    }

    // Stampa il grafo
    public void PrintGraph()
    {
        foreach (var node in adjacencyList)
        {
            Console.WriteLine($"{node.Key}: {string.Join(", ", node.Value)}");
        }
    }

    // DFS per esplorazione
    private List<int> DFS(int node, HashSet<int> visited)
    {
        var result = new List<int>();
        var stack = new Stack<int>();
        stack.Push(node);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (!visited.Contains(current))
            {
                visited.Add(current);
                result.Add(current);
                foreach (var neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }

        return result;
    }

    // DFS per trovare un cammino specifico
    private bool DFSFindPath(int current, int target, HashSet<int> visited, List<int> path)
    {
        visited.Add(current);
        path.Add(current);

        if (current == target)
        {
            return true;
        }

        foreach (var neighbor in adjacencyList[current])
        {
            if (!visited.Contains(neighbor))
            {
                if (DFSFindPath(neighbor, target, visited, path))
                {
                    return true;
                }
            }
        }

        path.RemoveAt(path.Count - 1); // Backtrack
        return false;
    }
}
