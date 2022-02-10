using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models
{
    public class CommandModel
    {
        public bool isNumeric { get; set; }
        public bool isAlpha { get; set; }
        public bool isDouble { get; set; }
        public Int32 inputSize { get; set; }
        public Int32 cType { get; set; }

        public Int32 NumericPercentage { get; set; }
        public Int32 AlphaPercentage { get; set; }
        public Int32 DoublePercentage { get; set; }

    }

    public class ReportModel
    {
        public int totalNumbers { get; set; }
        public double totalInt { get; set; }
        public double totalDouble { get; set; }
        public double totalAlph { get; set; }
        public List<string> list { get; set; }
    }

    public class CounterModel
    {
        public int counter1 { get; set; }
        public int counter2 { get; set; }
        public int counter3 { get; set; }
    }
}