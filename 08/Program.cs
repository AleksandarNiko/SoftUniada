
namespace _08
{
    public class TopologicalSort
    {
        public static void Main(string[] args)
        {
            int verticesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());
            List<List<int>> adjList = new List<List<int>>(verticesCount);

            for (int i = 0; i < verticesCount; i++)
            {
                adjList.Add(new List<int>());
            }

            for (int i = 0; i < edgesCount; i++)
            {
                string[] splitParams = Console.ReadLine().Split(" ");
                int parent = int.Parse(splitParams[0]);
                int child = int.Parse(splitParams[1]);
                adjList[parent].Add(child);
            }

            List<int> inDegree = new List<int>(verticesCount);
            for (int i = 0; i < verticesCount; i++)
            {
                inDegree.Add(0);
            }

            for (int u = 0; u < verticesCount; u++)
            {
                for (int i = 0; i < adjList[u].Count; i++)
                {
                    int index = adjList[u][i];
                    inDegree[index]++;
                }
            }

            PriorityQueue<int> pq = new PriorityQueue<int>();
            for (int i = 0; i < verticesCount; i++)
            {
                if (inDegree[i] == 0)
                {
                    pq.Add(i);
                }
            }

            List<int> topologicalOrder = new List<int>(verticesCount);

            while (pq.Count > 0)
            {
                int u = pq.Poll();
                topologicalOrder.Add(u);

                for (int i = 0; i < adjList[u].Count; i++)
                {
                    int index = adjList[u][i];
                    inDegree[index]--;
                    if (inDegree[index] == 0)
                        pq.Add(index);
                }
            }

            if (topologicalOrder.Count != verticesCount)
            {
                Console.WriteLine("circular dependency");
            }
            else
            {
                string joined = string.Join(" ", topologicalOrder);
                Console.WriteLine(joined);
            }
        }
    }
}