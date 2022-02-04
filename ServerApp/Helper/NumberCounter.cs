using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Helper
{
    public class NumberCounter
    {
        public int TotalCount { get; set; }

        public int counter1 { get; set; }
        public int counter2 { get; set; }
        public int counter3 { get; set; }

        static NumberCounter instance;
        protected NumberCounter()
        {

        }

        public static NumberCounter Instance()
        {
            if (instance == null)
            {
                instance = new NumberCounter();
            }
            return instance;
        }

        public void ReSet()
        {
            TotalCount = 0;
        }
    }
}