namespace _03
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            int[] numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

     
            int k = int.Parse(Console.ReadLine());

            Array.Sort(numbers);
            Array.Reverse(numbers);
            int[] mm = numbers.OrderBy(x => x).TakeLast(k).ToArray();
            
            for (int i = 0; i < k; i++)
            {
                Console.WriteLine(mm[i]);
            }
        }
    }
}
