namespace _06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            var permutations = GetPermutations(input);
            var palindromes = permutations.Where(IsPalindrome).Select(long.Parse).ToList();

            if (palindromes.Count > 0)
            {
                long largestPalindrome = palindromes.Max();
                Console.WriteLine($"{largestPalindrome}");
            }
            else
            {
                Console.WriteLine("No palindromic number available.");
            }
        }
        static IEnumerable<string> GetPermutations(string input)
        {
            if (input.Length == 1)
            {
                yield return input;
            }
            else
            {
                foreach (var perm in GetPermutations(input.Substring(1)))
                {
                    for (int i = 0; i <= perm.Length; i++)
                    {
                        yield return perm.Insert(i, input[0].ToString());
                    }
                }
            }
        }
        static bool IsPalindrome(string number)
        {
            return number.SequenceEqual(number.Reverse());
        }
    }
}
