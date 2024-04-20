using System.Diagnostics;

namespace _10
{
    public class Program
    {
        static void Main(string[] args)
        {
            var costs = ReadCosts();

#if DEBUG
            byte[] inputBuffer = new byte[16384];
            Stream inputStream = Console.OpenStandardInput(inputBuffer.Length);
            Console.SetIn(new StreamReader(inputStream, Console.InputEncoding, false, inputBuffer.Length));
#endif



            var positionA = Console.ReadLine()!.Split();
            var positionsB = Console.ReadLine()!.Split();

            var a = ReadHierarchy(positionA);
            var b = ReadHierarchy(positionsB);

            SetIndices(a);
            SetLeftMost(a);
            var la = ExtractLeftMostIndices(a);
            var kra = ExtractKeyRoots(la);
            var ova = ExtractOrderedValues(a);

            SetIndices(b);
            SetLeftMost(b);
            var lb = ExtractLeftMostIndices(b);
            var krb = ExtractKeyRoots(lb);
            var ovb = ExtractOrderedValues(b);

            var dp = new long[la.Length + 1, lb.Length + 1];

            var forest = new long[positionA.Length + 1, positionsB.Length + 1];

            for (var k1 = 0; k1 < kra.Length; k1++)
            {
                for (var k2 = 0; k2 < krb.Length; k2++)
                {
                    dp[kra[k1], krb[k2]] = CalculateMinimumEditDistance(kra[k1], krb[k2], la, lb, ova, ovb, costs, dp, forest);
                }
            }

            var ans = dp[la.Length, lb.Length];
            Console.WriteLine(ans);
        }
        private static long CalculateMinimumEditDistance(int i, int j, int[] la, int[] lb, string[] ova, string[] ovb, Costs c, long[,] dp, long[,] forest)
        {
            var rows = i + 1;
            var cleanCount = rows * forest.GetLength(1);
            Array.Clear(forest, 0, cleanCount);


            for (var k1 = la[i - 1]; k1 <= i; k1++) forest[k1, 0] = forest[k1 - 1, 0] + ova[k1 - 1].Length * c.Remove;
            for (var k2 = lb[j - 1]; k2 <= j; k2++) forest[0, k2] = forest[0, k2 - 1] + ovb[k2 - 1].Length * c.Add;

            for (var k1 = la[i - 1]; k1 <= i; k1++)
            {
                for (var k2 = lb[j - 1]; k2 <= j; k2++)
                {
                    var t1 = la[i - 1] > k1 - 1 ? 0 : k1 - 1;
                    var t2 = lb[j - 1] > k2 - 1 ? 0 : k2 - 1;

                    var currentInsertCost = c.Add * ovb[k2 - 1].Length;
                    var currentDeleteCost = c.Remove * ova[k1 - 1].Length;
                    if (la[i - 1] == la[k1 - 1] && lb[j - 1] == lb[k2 - 1])
                    {
                        var costCurrentReplace = CalculateMinimumEditDistance(ova[k1 - 1], ovb[k2 - 1], c);
                        forest[k1, k2] = Math.Min(
                            Math.Min(forest[t1, k2] + currentDeleteCost, forest[k1, t2] + currentInsertCost),
                            forest[t1, t2] + costCurrentReplace);

                        dp[k1, k2] = forest[k1, k2];
                    }
                    else
                    {
                        var u1 = la[k1 - 1] - 1;
                        var u2 = lb[k2 - 1] - 1;

                        var v1 = la[i - 1] > u1 ? 0 : u1;
                        var v2 = lb[j - 1] > u2 ? 0 : u2;

                        forest[k1, k2] = Math.Min(
                            Math.Min(forest[t1, k2] + currentDeleteCost, forest[k1, t2] + currentInsertCost),
                            forest[v1, v2] + dp[k1, k2]);
                    }
                }
            }

            return forest[i, j];
        }

        private static long CalculateMinimumEditDistance(string from, string to, Costs costs)
        {
            var dp = new long[from.Length + 1, to.Length + 1];
            for (var i = 1; i <= from.Length; i++) dp[i, 0] = i * costs.Remove;
            for (var j = 1; j <= to.Length; j++) dp[0, j] = j * costs.Add;

            for (var i = 1; i <= from.Length; i++)
            {
                for (var j = 1; j <= to.Length; j++)
                {
                    if (from[i - 1] == to[j - 1]) dp[i, j] = dp[i - 1, j - 1];
                    else dp[i, j] = Math.Min(dp[i - 1, j - 1] + costs.Change, Math.Min(dp[i - 1, j] + costs.Remove, dp[i, j - 1] + costs.Add));
                }
            }

            return dp[from.Length, to.Length];
        }

        private static Costs ReadCosts()
        {
            var addCost = int.Parse(Console.ReadLine()!);
            var changeCost = int.Parse(Console.ReadLine()!);
            var removeCost = int.Parse(Console.ReadLine()!);

            return new Costs(addCost, changeCost, removeCost);
        }

        private static HierarchyNode ReadHierarchy(string[] positions)
        {
            var connectionsCount = int.Parse(Console.ReadLine()!); // positions.Length - 1;
            Debug.Assert(connectionsCount == positions.Length - 1);

            var nodesByName = new Dictionary<string, HierarchyNode>();
            for (var i = 0; i < positions.Length; i++) nodesByName[positions[i]] = new HierarchyNode { Value = positions[i] };

            var children = new HashSet<string>();

            for (var i = 0; i < connectionsCount; i++)
            {
                var data = Console.ReadLine()!.Split();
                nodesByName[data[0]].Children.Add(nodesByName[data[1]]);
                children.Add(data[1]);
            }

            var candidateRoots = positions.Where(p => !children.Contains(p)).ToArray();
            Debug.Assert(candidateRoots.Length == 1);

            return nodesByName[candidateRoots[0]];
        }


        public static void SetIndices(HierarchyNode root) => SetIndices(root, 0);

        private static int SetIndices(HierarchyNode node, int index)
        {
            foreach (var child in node.Children) index = SetIndices(child, index);
            return node.Index = index + 1;
        }

        private static void SetLeftMost(HierarchyNode node)
        {
            if (node.Children.Count == 0) return;
            foreach (var child in node.Children) SetLeftMost(child);

            node.LeftMost = node.Children[0].LeftMost;
        }

        private static int[] ExtractLeftMostIndices(HierarchyNode root)
        {
            var result = new List<int>();
            ExtractLeftMostIndices(root, result);
            return result.ToArray();
        }

        private static void ExtractLeftMostIndices(HierarchyNode node, List<int> indices)
        {
            foreach (var child in node.Children) ExtractLeftMostIndices(child, indices);
            indices.Add(node.LeftMost.Index);
        }

        public static int[] ExtractKeyRoots(int[] leftMostIndices)
        {
            var result = new List<int>();
            for (var i = 0; i < leftMostIndices.Length; i++)
            {
                var add = true;
                for (var j = i + 1; j < leftMostIndices.Length; j++)
                    if (leftMostIndices[i] == leftMostIndices[j]) { add = false; break; }

                if (add) result.Add(i + 1);
            }

            return result.ToArray();
        }

        public static string[] ExtractOrderedValues(HierarchyNode root)
        {
            var result = new List<string>();
            ExtractOrderedValues(root, result);
            return result.ToArray();
        }

        private static void ExtractOrderedValues(HierarchyNode node, List<string> values)
        {
            foreach (var child in node.Children) ExtractOrderedValues(child, values);
            values.Add(node.Value);
        }
    }

    public class Costs
    {
        public Costs(int add, int change, int remove)
        {
            this.Add = add;
            this.Change = change;
            this.Remove = remove;
        }

        public int Add { get; }
        public int Change { get; }
        public int Remove { get; }
    }

    public class HierarchyNode
    {
        private HierarchyNode? _leftMostChild;

        public string Value { get; set; }
        public int Index { get; set; }

        public HierarchyNode LeftMost
        {
            get => this._leftMostChild ?? this;
            set => this._leftMostChild = value;
        }
        public List<HierarchyNode> Children { get; } = new();
    }
}
