namespace _05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int n = numbers.Length;

            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int diff = numbers[j] - numbers[i];
                    int next = numbers[j] + diff;
                    int seqCount = 2;

                    for (int k = j + 1; k < n; k++)
                    {
                        if (numbers[k] == next)
                        {
                            next += diff;
                            seqCount++;
                        }
                    }

                    count += seqCount - 1;
                }
            }

            Console.WriteLine(count + n+1);

        }
    

    }
}
