using System.Text;

namespace Core.Data.Code
{
    public class WriteWorker
    {
        StringBuilder m_builder = new StringBuilder();

        public WriteWorker EndLine()
        {
            m_builder.AppendLine();
            return this;
        }

        public WriteWorker Append(char value)
        {
            m_builder.Append(value);
            return this;
        }

        public WriteWorker Append(string value)
        {
            m_builder.Append(value);
            return this;
        }

        public WriteWorker Tab()
        {
            m_builder.Append('\t');
            return this;
        }

        public WriteWorker Tab(int count)
        {
            m_builder.Append('\t', count);
            return this;
        }

        public string Get()
        {
            return m_builder.ToString();
        }
    }
}