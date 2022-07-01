namespace Chaper07_Const_Field;
/* ---------------常量和字段---------------
 * 常量被视为类型定义的一部分，是静态成员
 *
 * 字段是一种数据成员，其中容纳了一个值类型的实例或者一个引用类型的引用
 *
 * 字段修饰符：static（静态），默认（Instance实例），readonly（构造器写入），volatile（线程不安全的优化措施）
 */
public class Program
{
    public static void Main()
    {
        MyClass myClass = new MyClass(5);
        // myClass.a = 5;
    }
}

class MyClass
{
    public readonly int a;

    public MyClass(int a)
    {
        this.a = a;
    }
}