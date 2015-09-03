using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Model
{
    public enum EncodeSate
    {
        未加密,
        已加密
    }
    
    public class FileModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string OutputDir { get; set; }

        public EncodeSate FileEncodeSate { get; set; }


        public FileModel() { FileEncodeSate = EncodeSate.未加密; }

        public FileModel(string name, string path)
        {
            this.Name = name;
            this.Path = path;
            FileEncodeSate = EncodeSate.未加密;
        }

        public FileModel(string name, string path,string outPutDir):this(name,path)
        {
            this.OutputDir = outPutDir;
        }
    }
}
