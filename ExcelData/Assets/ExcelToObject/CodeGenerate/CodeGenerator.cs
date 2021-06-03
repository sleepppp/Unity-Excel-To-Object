
using System.IO;

namespace Core.Data.Code
{
    public class CodeGenerator
    {
        public readonly string c_comment = "//";
        public readonly string c_using = "using";
        public readonly string c_public = "public";
        public readonly string c_private = "private";
        public readonly string c_namespace = "namespace";
        public readonly string c_static = "static";
        public readonly string c_semicolon = ";";
        public readonly string c_dot = ".";
        public readonly string c_class = "class";
        public readonly string c_space = " ";
        public readonly string c_startBlock = "{";
        public readonly string c_endBlock = "}";
        public readonly string c_equal = "=";

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

        public void Field(string type, string name,string value = null,bool isStatic = false)
        {
            CheckTab();
            m_worker.Append(c_public).Append(c_space);

            if(isStatic)
            {
                m_worker.Append(c_static).Append(c_space);
            }

            m_worker.Append(type).Append(c_space).Append(name);

            if(string.IsNullOrEmpty(value) == false)
            {
                m_worker.Append(c_space).Append(c_equal).Append(c_space).Append(value);
            }

            m_worker.Append(c_semicolon).EndLine();
        }

        public void StringField(string type, string name, string value = null, bool isStatic = false)
        {
            CheckTab();
            m_worker.Append(c_public).Append(c_space);

            if (isStatic)
            {
                m_worker.Append(c_static).Append(c_space);
            }

            m_worker.Append(type).Append(c_space).Append(name);

            if (string.IsNullOrEmpty(value) == false)
            {
                m_worker.Append(c_space).Append(c_equal).Append(c_space).Append('"').Append(value)
                    .Append('"');
            }

            m_worker.Append(c_semicolon).EndLine();
        }

        public void PrivateField(string type, string name)
       {
            CheckTab();
            m_worker.Append(c_private).Append(c_space).Append(type).Append(c_space).Append(name).
                Append(c_semicolon).EndLine();
       }

        public void Dictionary(string key, string value,string name)
        {
            CheckTab();
            m_worker.Append(c_public).Append(c_space).Append("Dictionary<").Append(key).Append(',')
                .Append(value).Append('>').Append(c_space).Append(name).Append(c_semicolon).EndLine();
        }

        public void StartVoidFunc(string name)
        {
            CheckTab();
            m_worker.Append(c_private).Append(c_space).Append(name).Append("()").EndLine();
            StartBlock();
        }

        public void EndVoidFunc()
        {
            EndBlock();
        }

        public void Tab()
        {
            m_worker.Tab();
        }

        public void EndLine()
        {
            m_worker.EndLine();
        }

        public void CheckTab()
        {
            if (m_codeBlockCount != 0)
                m_worker.Tab(m_codeBlockCount);
        }

        public void StartConstructor(string className)
        {
            CheckTab();
            m_worker.Append(c_public).Append(c_space).Append(className).Append("()").EndLine();
            StartBlock();
        }

        public void EndConstructor()
        {
            EndBlock();
        }

        public CodeGenerator Append(string write)
        {
            m_worker.Append(write);
            return this;
        }

        public CodeGenerator Space()
        {
            m_worker.Append(c_space);
            return this;
        }

        public void Singleton(string className)
        {
            CheckTab();
            m_worker.Append("static").Append(c_space).Append(className).Append(c_space).Append("_instance")
                .Append(c_semicolon).EndLine();
            CheckTab();
            m_worker.Append(c_public).Append(c_space).Append("static").Append(c_space).Append(className)
                .Append(c_space).Append("instance").EndLine();
            StartBlock();
            {
                CheckTab();
                m_worker.Append("get");
                StartBlock();
                {
                    CheckTab();
                    m_worker.Append("if(_instance == null)_instance = FindObjectOfType<")
                        .Append(className).Append(">();").EndLine();
                    CheckTab();
                    m_worker.Append("return _instance;");
                }
                EndBlock();
            }
            EndBlock();
        }

    }
}