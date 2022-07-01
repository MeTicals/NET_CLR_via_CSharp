
namespace Chapter11_Event
{

    internal class MailManager
    {
        public event EventHandler<NewMailEventArgs> NewMail;

        protected virtual void OnNewMail(NewMailEventArgs e)
        {
            // 线程安全的方式引发事件（p225）
            // 这里第二行的判断，是如果有人注册了事件再调用，避免 NullReferenceException 异常
            // 但是有可能线程先检测出 NewMail 不为 null ，但调用之前另一个线程清空了事件委托，导致 NewMail 为空还是会抛出异常
            // 解决办法就是通过 Volatile.Read 调用强迫 NewMail 在调用上发生读取，让变量复制到 temp 中
            // 并只有在 temp 为 null 时才会被调用
            EventHandler<NewMailEventArgs> temp = Volatile.Read(ref NewMail);
            if (temp != null) temp(this, e);    
        }
    }
    
}