

// 无参属性（表面上看起来像是直接访问字段，实际上是语法糖，IL在背后声明了方法）
// 这里的get属性没有接受参数，所以称之为无参属性，用起来就像访问字段一样（虽然实际上是通过方法去获取的字段）
// 这种无参属性适用于普通的字段，但是如果声明了一个数组，就会涉及到数组的序号，这时需要有参属性

namespace Chapter10_property_nonePara
{
    public class Program
    {
        public static void Main()
        {
            var e = new Employee();
            e.m_name = "xuan";
            e.m_age = 5;
        }
    }

    public sealed class Employee
    {
        public String m_name;
        public int m_age
        {
            get
            {
                return m_age;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                m_age = value;
            }
        }

        // 想要在获取时判断数组是否越界，但是这样是不行的
        // public string[] names
        // {
        //     get
        //     {
        //         string temp;
        //         if (index >= 0 && index <= nameList.Length - 1)
        //         {
        //             temp = nameList[index];
        //         }
        //         else
        //         {
        //             throw new ArgumentOutOfRangeException("超出数组上下界");
        //         }
        //
        //         return temp;
        //
        //     }
        //     set
        //     {
        //         if (index >= 0 && index < nameList.Length - 1)
        //         {
        //             nameList[index] = value;
        //         }
        //     }
        // }
    }
}

// 有参属性（索引器）
// get访问器接收 1 个或 多 个参数，set 访问器接收 2 个或 多 个参数
// 有参数行一般都用于集合类型，不仅可以设置上下标越界等问题，还让类可以像数组一样访问里面的元素
// 该例子为CLR书本上的例子
namespace Chapter10_property_HasPara_CLRBook
{
    public class Program
    {
        public static void Main()
        {
            BitArray ba = new BitArray(14);

            for (int x = 0; x < 14; x++)
            {
                ba[x] = (x % 2 == 0);
            }

            for (int x = 0; x < 14; x++)
            {
                Console.WriteLine("Bit " + x + "is" + (ba[x] ? "On" : "Off"));
            }
        }

    }

    public sealed class BitArray
    {
        // 容纳二进制位的私有字节数组
        private Byte[] m_byteArray;
        private int m_numBits;

        // 构造器用于分配字节数组，并将所有位设为0
        public BitArray(int numBits)
        {
            // 验证实参
            if (numBits <= 0)
            {
                throw new ArgumentOutOfRangeException("numBits must be > 0");
            }

            // 保存位的个数
            m_numBits = numBits;

            // 为位数组分配字节
            m_byteArray = new Byte[(numBits + 7) / 8];
        }

        // 索引器
        public Boolean this[int bitPos]
        {
            get
            {
                if (bitPos < 0 || (bitPos >= m_numBits))
                    throw new ArgumentOutOfRangeException("bitPos");
                return (m_byteArray[bitPos / 8] & (1 << (bitPos % 8))) != 0;
            }
            set
            {
                if (bitPos < 0 || (bitPos >= m_numBits))
                    throw new ArgumentOutOfRangeException("bitPos", bitPos.ToString());
                if (value)
                {
                    m_byteArray[bitPos / 8] = (Byte)((m_byteArray[bitPos / 8] | 1 << (bitPos % 8)));
                }
                else
                {
                    m_byteArray[bitPos / 8] = (Byte)(m_byteArray[bitPos / 8] & ~(1 << (bitPos % 8)));
                }
            }
        }
    }
}

// 有参属性
// 网络视频方便理解
// 索引器建立了一个类似于数组的操作方式，来访问内部的私有变量
namespace Chapter10_property_HasPara_Internet
{
    class Program
    {
        public static void Main()
        {
            var names = new IndexedNames();
            names[0] = 1.ToString();
            names[0] = 2.ToString();
            names[0] = 3.ToString();
            names[0] = 4.ToString();
            names[0] = 5.ToString();
            names[0] = 6.ToString();
            names[0] = 7.ToString();
            names[0] = 8.ToString();
            names[0] = 9.ToString();
            names[0] = 10.ToString();
            
            Console.WriteLine(names[5]);
        }
    }

    class IndexedNames
    {
        private string[] nameList = new string[10];

        public IndexedNames()
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                nameList[i] = "N/A";
            }
        }

        public string this[int index]
        {
            get
            {
                string temp;
                if (index >= 0 && index <= nameList.Length - 1)
                {
                    temp = nameList[index];
                }
                else
                {
                    throw new ArgumentOutOfRangeException("超出数组上下界");
                }

                return temp;

            }
            set
            {
                if (index >= 0 && index < nameList.Length - 1)
                {
                    nameList[index] = value;
                }
            }
        }
    }
}