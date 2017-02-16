using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class RangeOfArray
    {
        int x=0;
        int y = 0;
        int[] array;
        public RangeOfArray(int _x, int _y){
            x=_x;
            y=_y;
            array = new int[y- x+1];
        }
        public int this[int index]
        {
            get{
                if ((index < x) || (index >= y))
                    throw new System.Exception("Недопустимый индекс");
                return array[index-x];
            }
            set
            {
                if ((index < x) || (index > y))
                    throw new System.Exception("Недопустимый индекс");
                array[index - x] = value;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int upper;
            int lower;
            Console.WriteLine("upper is: ");
            upper = int.Parse(Console.ReadLine());
            Console.WriteLine("lower is: ");
            lower =  int.Parse(Console.ReadLine());
            RangeOfArray ROF = new RangeOfArray(lower, upper);
            try
            {
                ROF[-6] = 5;
                ROF[-4] = 10;
                ROF[9] = 4;
                Console.WriteLine(ROF[-6]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { }
        }
    }
}
