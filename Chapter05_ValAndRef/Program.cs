namespace Chapter5_ValAndRef
{
    // Point 是值类型，继承自接口 IComparable
    internal struct Point : IComparable
    {
        private Int32 m_x, m_y;

        // 构造器初始化字段
        public Point(Int32 x,Int32 y)
        {
            m_x = x;
            m_y = y;
        }
        
        // 重写 ToString 方法
        public override String ToString()
        {
            // 将 point 作为字符串返回。调用 ToString 以避免装箱（值类型虽然没有类型对象指针，但可以调用类型继承或重写的虚方法）
            return String.Format("({0},{1})", m_x.ToString(), m_y.ToString());
        }
        
        // 实现类型安全的 CompareTo 方法
        public Int32 CompareTo(Point other)
        {
            // 两坐标距离圆点距离相同则判定相等
            return Math.Sign(Math.Sqrt(m_x * m_y + m_y * m_y)
                             - Math.Sqrt(other.m_x * other.m_x + other.m_y * other.m_y));
        }

        // 实现 IComparable 的 CompareTo 方法
        public Int32 CompareTo(object o)
        {
            if (GetType() != o.GetType())
            {
                throw new ArgumentException("o is not a Point");
            }
            // 调用类型安全的 CompareTo 方法
            return CompareTo((Point)o);
        }
    }
    
    internal static class Program
    {
        public static void Main()
        {
            // 在栈上创建两个 Point 实例
            Point p1 = new Point(10, 10);
            Point p2 = new Point(20, 20);
            
            // 调用 ToString （虚方法） 不需要装箱 p1
            Console.WriteLine(p1.ToString());
            
            // 调用 GetType （非虚方法） 需要对 p1 进行装箱
            Console.WriteLine(p1.GetType());
            
            // 调用 CompareTo(Point) 方法 不需要对 p1 和 p2 装箱
            Console.WriteLine(p1.CompareTo(p2));

            // 将 p1 装箱并引用到 c 中，此时 c 对 Point 的内容一无所知 （C# 不允许更改已装箱值类型中的字段）
            IComparable c = p1; 
            // c 已经装箱，直接调用 GetType 方法
            Console.WriteLine(c.GetType());
            
            // 调用 Compare(object) 方法，p1 不装箱，c 已经引用了 point
            Console.WriteLine(p1.CompareTo(c));
            
            // 调用 Compare(object) 方法， p2 需要装箱
            Console.WriteLine(c.CompareTo(p2));
            
            // 对 c 进行拆箱，并复制字段到 p2
            p2 = (Point)c;
            
            // 证明已经复制到字段 p2 中
            Console.WriteLine(p2.ToString());
        }
    }
    
    
    
}