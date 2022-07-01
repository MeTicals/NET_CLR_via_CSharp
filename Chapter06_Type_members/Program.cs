namespace Chapter06_Type_members;

/* --------第六章：类型和成员基础--------
   常量：      指出数据值不变的符号
   字段：      只读/可读可写的数据值。可以是静态字段或成员字段
   实例构造器： 新对象的“实例字段”初始化的方法
   类型构造器： 类型的“静态字段”初始化的方法
   方法：      更改或查询类型或对象的函数。可以是静态方法或实例方法
   操作符重载： 实际上是方法，定义操作符的操作方法
   转换操作符： 定义如何隐式或显式将类型转换的方法
   属性：      查询类型或对象的逻辑状态，保证状态不被破坏
   事件：      静态事件或实例事件 允许类型或实例向多个静态方法或实例发送通知
   类型：      允许嵌套类型，将大的复杂的类型分解成小的构建单元来实现
  --------------------------------- */


/*   -----------静态类------------
   静态类直接从基类 System.Ojbect 派生，不能创建任何静态类的实例
   静态类不能实现任何接口，因为只有使用类的实例的时候，才能调用类的接口方法
   静态类只能定义静态成员
   静态类不能作为字段、方法参数或局部变量使用，因为这些都是实例的变量
     ----------------------------- */

/*  -----------call和callvirt------------
    call：该IL指令可以调用静态方法、实例方法和虚方法。
    call 调用静态方法必须指定静态方法的类型，调用实例方法或虚方法必须指定对象的引用，他会假定引用不为null
    call 可以以非虚方式调用虚方法
    
    callvirt：该IL指令调用实例方法和虚方法。
    callvirt 调用虚方法需要检测引用变量的类型，因为要检测，所以必须不为null，所以他稍慢一些
    callvirt 即使调用非虚方法，也会进行nulll检查
    
    当这个类被标记为sealed密封类时，一定不会有派生类类，所以会用call调用
 */
   
public class Program
{
    public static void Main()
    {
        PhoneCompanySupport phonePP = new PhoneCompanySupport();
        PhoneCompanySupport phonePA = new PhoneCompanyA();
        PhoneCompanyA phoneAA = new PhoneCompanyA();
        phonePP.BoHao();
        phonePA.BoHao();

        // phonePA.ICanAIDuiHua();

        var phonePAtoAA = phonePA as PhoneCompanyA;
        
        phonePAtoAA.ICanAIDuiHua();
        
        phoneAA.BoHao();
        phoneAA.ICanAIDuiHua();
    }
}

internal class PhoneCompanySupport
{
    public virtual void BoHao()
    {
        Console.WriteLine("Phone 正在拨号...");
        LianJieChengGong();
    }

    public virtual void LianJieChengGong()
    {
        Console.WriteLine("Phone 连接成功!");
    }
}

internal class PhoneCompanyA : PhoneCompanySupport
{
    public override void LianJieChengGong()
    {
        Console.WriteLine("PhoneA 连接成功!");
    }

    public void ICanAIDuiHua()  //PhoneCompanyA独有的方法
    {
        Console.WriteLine("PhoneA 正在通过AI与您对话");
    }
}