namespace _04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            int R = int.Parse(Console.ReadLine());
            int C = int.Parse(Console.ReadLine());
            int T = int.Parse(Console.ReadLine());
            int F = int.Parse(Console.ReadLine());

            int[,] moves = { { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 }, { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 } };

            int[,] board = new int[N, N];
            board[R, C] = 1;

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((R, C));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int row = current.Item1;
                int col = current.Item2;

                if (row == T && col == F)
                {
                    Console.WriteLine(board[row, col] - 1);
                    break;
                }

                for (int i = 0; i < 8; i++)
                {
                    int newRow = row + moves[i, 0];
                    int newCol = col + moves[i, 1];

                    if (newRow >= 0 && newRow < N && newCol >= 0 && newCol < N && board[newRow, newCol] == 0)
                    {
                        board[newRow, newCol] = board[row, col] + 1;
                        queue.Enqueue((newRow, newCol));
                    }
                }
            }
        }
    }
}
