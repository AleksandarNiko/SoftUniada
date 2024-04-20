namespace _01
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] numbers = input.Split(' ');

            int topElement = FindTopElement(numbers);
            Console.WriteLine(topElement);
        }
        static int FindTopElement(string[] numbers)
        {
            int topElement = int.MinValue;

            for (int i = 1; i < numbers.Length - 1; i++)
            {
                int current = int.Parse(numbers[i]);
                int prev = int.Parse(numbers[i - 1]);
                int next = int.Parse(numbers[i + 1]);

                if (current > prev && current > next && current > topElement)
                {
                    topElement = current;
                }
            }

            return topElement;
        }
    }
}
