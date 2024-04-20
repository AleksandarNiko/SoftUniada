using System.Text;

namespace _02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int num1 = 0;
            for (int j = 0; j < n / 2; j++)
            {
                string b = "";
                for (int h = 0; h < n + num1; h++)
                {
                    b += ".";
                }

                for (int h = 0; h < n * 5 - (n + num1) * 2; h++)
                {
                    b += "#";
                }

                for (int h = 0; h < n + num1; h++)
                {
                    b += ".";
                }

                Console.WriteLine(b);
                num1++;
            }

            for (int j = 0; j < n / 2 + 1; j++)
            {
                string b = "";
                for (int h = 0; h < n + num1; h++)
                {
                    b += ".";
                }

                b += "#";
                for (int h = 0; h < n * 5 - (n + num1) * 2 - 2; h++)
                {
                    b += ".";
                }

                b += "#";
                for (int h = 0; h < n + num1; h++)
                {
                    b += ".";
                }

                Console.WriteLine(b);
                num1++;
            }

            string mid = "";
            for (int j = 0; j < n * 2; j++)
            {
                mid += ".";
            }

            for (int j = 0; j < n; j++)
            {
                mid += "#";
            }

            for (int j = 0; j < n * 2; j++)
            {
                mid += ".";
            }

            Console.WriteLine(mid);
            for (int j = 0; j < n / 2; j++)
            {
                string b = "";
                for (int h = 0; h < (n * 5 - (n + 4)) / 2; h++)
                {
                    b += ".";
                }

                for (int h = 0; h < n + 4; h++)
                {
                    b += "#";
                }

                for (int h = 0; h < (n * 5 - (n + 4)) / 2; h++)
                {
                    b += ".";
                }

                Console.WriteLine(b);
            }

            string dnc = "";
            for (int h = 0; h < (n * 5 - 10) / 2; h++)
            {
                dnc += ".";
            }

            dnc += "D^A^N^C^E^";
            for (int h = 0; h < (n * 5 - 10) / 2; h++)
            {
                dnc += ".";
            }

            Console.WriteLine(dnc);
            for (int j = 0; j < n / 2 + 1; j++)
            {
                string b = "";
                for (int h = 0; h < (n * 5 - (n + 4)) / 2; h++)
                {
                    b += ".";
                }

                for (int h = 0; h < n + 4; h++)
                {
                    b += "#";
                }

                for (int h = 0; h < (n * 5 - (n + 4)) / 2; h++)
                {
                    b += ".";
                }

                Console.WriteLine(b);


            }
        }
    }
}