namespace _07
{
    public class Program
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            int M = int.Parse(Console.ReadLine());

            for (int i = N; i <= M; i++)
            {
                if (IsSpecialNumber(i))
                {
                    Console.WriteLine(i);
                }
            }
        }

        static bool IsSpecialNumber(int num)
        {
            string numStr = num.ToString();

            for (int i = 0; i < numStr.Length - 1; i++)
            {
                if (Math.Abs(numStr[i] - numStr[i + 1]) != 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
