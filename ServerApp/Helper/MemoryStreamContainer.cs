using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ServerApp.Helper
{
    public class MemoryStreamContainer
    {
        public MemoryStream memoryStream { get; set; }

        public string memoryNumbers { get; set; }

        static MemoryStreamContainer instance;
        protected MemoryStreamContainer()
        {

        }

        public static MemoryStreamContainer Instance()
        {
            if (instance == null)
            {
                instance = new MemoryStreamContainer();
            }
            return instance;
        }

        public string getNumbers()
        {
            if (!string.IsNullOrEmpty(memoryNumbers))
                return memoryNumbers;
            else
              return  string.Empty;
        }
    }
}