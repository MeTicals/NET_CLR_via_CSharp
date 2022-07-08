//-------------第九章: 参数 ---------------

namespace Chapter09_Parameter;

public class Program
{
    public static void Main()
    {
        var items = new int[] { 1, 2, 3, 4, 5 };
        Console.WriteLine(Add(items));
        // params 可以直接传入一组数
        Console.WriteLine(Add2(1,2,3,4,5));
    }

    public static int Add(int[] value)
    {
        int sum = 0;
        if (value != null)
        {
            foreach (var item in value)
            {
                sum += item;
            }
        }

        return sum;
    }
    
    public static int Add2(params int[] value)
    {
        int sum = 0;
        if (value != null)
        {
            foreach (var item in value)
            {
                sum += item;
            }
        }

        return sum;
    }
}