/*  ---------------第八章：方法-------------------
 * 类实例的构造器：为了使代码“可验证”，类的实例构造器在访问从基类继承的任何字段前，必须调用基类的构造器
 * 
 * 结构体的构造器：C# 不允许为值类型定义”无参构造器“，所以结构体内声明的字段不能有初始值设定，所有值类型初始值都是 0 或 null
 *              但可以为结构体创建”有参构造器“，只有显式的使用 new 才会调用构造器
 *   struct point
 *   {
 *       public int x, y;
 *       public point(int _x, int _y)
 *       {
 *           x = _x;
 *           y = _y;
 *       }
 *   }
 *
 *   private void Method()
 *   {
 *       point p = new point(3, 4);
 *   }
 *
 * 类型构造器：可以应用于引用类型和值类型，设置类型的初始状态，只可定义一个且无参数
 *----------------------------------------------------------------------
 * 扩展方法：它允许定义一个静态方法，并用实例方法的语法来调用。
 *     public static class StringBuilderExtensions {
 *         public static Int32 IndexOf(this StringBuilder sb, Char value) {
 *             for (Int32 index = 0; index < sb.Length; index++)
 *                 if (sb[index] == value) return index;
 *             return -1;
 *         }
 *     }
 *
 *     StringBuilder sb = new StringBuilder("Hello world!");
 *     Int32 index = sb.IndexOf('l');
 *     // 编译器看到该代码会首先查找StringBuilder类和它的基类是否有单个Char参数名为IndexOf的实例方法
 *     // 如果没有，就会寻找是否有静态类定义了名为IndexOf的静态方法，第一个参数与该类型相同，且后面的参数与方法参数相同
 *
 * 
 */

//---------------------分部方法 Part1-----------------------
// 例如某个工具定义了一些行为，其中更改名字前有一个虚方法可以由开发人员自定义
// 这种虚方法的方式存在两种问题
// 1. 类型必须是非密封的类。不能用于sealed类，也不能用于值类型（隐式密封），也不能用于静态类（不能被继承）
// 2. 效率问题。定义类型只为了重写一个方法会浪费少量系统资源，如果不想重写OnNameChanging方法，系统再set处也会调用一个什么都不做的虚方法，并直接返回
//            无论OnNameChanging是否访问传给他的实参，编译器都会对ToUpper调用并生成IL代码。
namespace Chapter08_Method_FenBuFangFaPart1
{ 
    public class Program
    {
        public static void Main()
        {
            Derived derived = new Derived();
            derived.Name = "HelloWorld";
        }
    }

    internal class Base //工具的代码，储存在某个源文件中
    {
        private String m_name;

        protected virtual void OnNameChaging(String value)  //可以自定义更改名字前进行的操作
        {
        }

        public String Name
        {
            get { return m_name; }
            set
            {
                OnNameChaging(value.ToUpper()); //调用自定义的方法
                m_name = value; //更改字段
                Console.WriteLine("my name is: " + m_name); //输出自己的名字
            }
        }
    }

    internal class Derived : Base   //开发人员生成的代码，储存在另一个源文件中
    {
        protected override void OnNameChaging(String value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException();
        }
    }
}

//---------------------分部方法 Part2----------------------
// 分部方法如果没有补全方法的行为，编译器生成的IL代码和元数据会直接抛弃这个方法
// 分部方法不允许存在返回值，也就是方法一定是 void ，也不能用 out 修饰符标记，因为该方法再运行时可能不存在
// 分部方法一定是 private 方法，所以编辑器禁止添加 private 关键字

namespace Chapter08_Method_FenBuFangFaPart2
{
    public class Program
    {
        public static void Main()
        {
            Base partBase = new Base();
            partBase.Name = "HelloWorld";
        }
    }

    internal sealed partial class Base
    {
        private String m_name;

        //分部方法的声明
        partial void OnNameChanging(String value);

        public String Name
        {
            get { return m_name; }
            set
            {
                OnNameChanging(value.ToUpper());
                m_name = value;
                Console.WriteLine(m_name);
            }
        }
    }

    internal sealed partial class Base
    {
        // 这是分部方法的实现，会在m_name更改前调用
        partial void OnNameChanging(String value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");
        }
    }
}