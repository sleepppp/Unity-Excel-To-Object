using System.Text;
using System.IO;

namespace Core.Data.Table
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
            m_worker.Append(c_comment).Append(value).EndLine();
        }

        public void Using(string value)
        {
            m_worker.Append(c_using).Append(value).Append(c_semicolon).EndLine();
        }

        public void NameSpace(string value)
        {
            m_worker.Append(c_namespace).Append(c_space).Append(value).Append(c_semicolon).EndLine();
        }

        public void Class(string value)
        {
            m_worker.Append(c_public).Append(c_space).Append(c_class).Append(c_space).
                Append(value).EndLine();
        }

        public void StartBlock()
        {
            m_worker.Append(c_startBlock).EndLine();
        }
        public void EndBlock()
        {
            m_worker.Append(c_endBlock).EndLine();
        }

        public void Field(string type, string name)
        {
            m_worker.Append(c_public).Append(c_space).Append(type).Append(c_space).Append(name).
                Append(c_semicolon).EndLine();
        }

        public void Tab()
        {
            m_worker.Tab();
        }
    }
}