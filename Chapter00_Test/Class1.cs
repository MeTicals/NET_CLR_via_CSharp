using System.Text.RegularExpressions;

namespace Chapter00_Test;

public class Class1
{
    public dynamic dClass2;
    public static void Main()
    {
        var class2 = new class2();
        var class1 = new Class1();
        class1.otherMethod();
    }

    public void otherMethod()
    {
        dClass2 = new class2();
        // dClass2.StaticMethod();
        dClass2.Method();
    }
}

public class class2
{
    public static int staticInt = 0;
    public int myInt = 0;

    public static void StaticMethod()
    {
        Console.WriteLine("静态方法");
    }

    public void Method()
    {
        Console.WriteLine("成员方法");
    }
}