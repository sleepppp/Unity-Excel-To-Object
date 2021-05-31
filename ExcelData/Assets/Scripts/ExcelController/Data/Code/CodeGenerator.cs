using System.Text;
using System.IO;

namespace Core.Data.Code
{
    public class CodeGenerator
    {
        readonly string c_comment = "//";
        readonly string c_using = "using";
        readonly string c_public = "public";
        readonly string c_namespace = "namespace";
        readonly string c_semicolon = ";";
        readonly string c_dot = ".";
        readonly string c_class = "class";
        readonly string c_space = " ";
        readonly string c_startBlock = "{";
        readonly string c_endBlock = "}";

        WriteWorker m_worker = new WriteWorker();
        int m_codeBlockCount = 0;
        public WriteWorker worker { get { return m_worker; } }
        public string Get() { return m_worker.Get(); }

        public void WriteFile(string path)
        {
            using (FileStream stream = File.Open(path, FileMode.OpenOrCreate))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(m_worker.Get());
            }
        }

        public void Comment(string value)
        {
            CheckTab();
            m_worker.Append(c_comment).Append(value).EndLine();
        }

        public void Using(string value)
        {
            CheckTab();
            m_worker.Append(c_using).Append(c_space).Append(value).Append(c_semicolon).EndLine();
        }

        public void NameSpace(string value)
        {
            CheckTab();
            m_worker.Append(c_namespace).Append(c_space).Append(value).EndLine();
        }

        public void Class(string value)
        {
            CheckTab();
            m_worker.Append(c_public).Append(c_space).Append(c_class).Append(c_space).
                Append(value).EndLine();
        }

        public void StartBlock()
        {
            CheckTab();
            m_worker.Append(c_startBlock).EndLine();
            ++m_codeBlockCount;
        }
        public void EndBlock()
        {
            --m_codeBlockCount;
            CheckTab();
            m_worker.Append(c_endBlock).EndLine();
        }

        public void Field(string type, string name)
        {
            CheckTab();
            m_worker.Append(c_public).Append(c_space).Append(type).Append(c_space).Append(name).
                Append(c_semicolon).EndLine();
        }

        public void Tab()
        {
            m_worker.Tab();
        }

        public void EndLine()
        {
            m_worker.EndLine();
        }

        void CheckTab()
        {
            if (m_codeBlockCount != 0)
                m_worker.Tab(m_codeBlockCount);
        }
    }
}