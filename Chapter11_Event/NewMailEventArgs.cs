namespace Chapter11_Event
{
    //定义需要发送给事件通知者的附加信息
    internal class NewMailEventArgs : EventArgs 
    {
        private readonly string m_from, m_to, m_subject;

        public NewMailEventArgs(string from, string to, string subject)
        {
            m_from = from;
            m_to = to;
            m_subject = subject;
        }

        public string from
        {
            get { return m_from; }
        }

        public string to
        {
            get { return m_to; }
        }

        public string subject
        {
            get { return m_subject; }
        }

    }
}
