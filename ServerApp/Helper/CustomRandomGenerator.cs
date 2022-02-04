using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ServerApp.Helper
{
    public class CustomRandomGenerator
    {

        DoubleRandomNumber doubleInst;
        AlphaNumericRandomNumber alphInst;
        IntRandomNumber intInst;
        private int size;
        private CommandModel commandModel;
        public CustomRandomGenerator(CommandModel model)
        {
            doubleInst = DoubleRandomNumber.Instance();
            alphInst = AlphaNumericRandomNumber.Instance();
            intInst = IntRandomNumber.Instance();
            size = model.inputSize;
            commandModel = model;
            //
            doubleInst.GenerateRandomNumber(model.inputSize);
            intInst.GenerateRandomNumber(model.inputSize);
            alphInst.GenerateRandomNumber(model.inputSize);
        }

        public void StartGenerate()
        {
            var state = StartEndStreamWrite.Instance();
            state.Start();
            var counter = NumberCounter.Instance();
            counter.TotalCount = 0;
            counter.counter1 = 0;
            counter.counter2 = 0;
            counter.counter3 = 0;

            var mem = MemoryStreamContainer.Instance();
            mem.memoryNumbers = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                var sw = new StreamWriter(memoryStream);
                int i = 0;
                string content = string.Empty;

                while (memoryStream.Length <= size 
                    && state.executionState == 1)
                {
                    content = string.Empty;
                    int? randomInt = null;
                    string alp = string.Empty;
                    double? doubleNumber = null;
                    if (commandModel.isNumeric)
                    {
                        randomInt = intInst.list[i % intInst.list.Count];
                        counter.counter1 += 1;
                    }
                    if (commandModel.isAlpha)
                    {
                        alp = alphInst.list[i % alphInst.list.Count];
                        counter.counter2 += 1;
                    }
                    if (commandModel.isDouble)
                    {
                        doubleNumber = doubleInst.list[i % doubleInst.list.Count];
                        counter.counter3 += 1;
                    }

                    if (i == 0)
                        content = getBuildStr(randomInt,alp,doubleNumber); // randomInt + "," + alp + "," + doubleNumber;
                    else
                        content = "#" + getBuildStr(randomInt, alp, doubleNumber); //randomInt + "," + alp + "," + doubleNumber;

                    sw.Write(content);
                    i++;
                    CounterHub.BroadcastCountDataStatic(i.ToString());
                }

                counter.TotalCount = i;

                state.executionState = 0;
                sw.Flush();
                mem.memoryNumbers = Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                WriteInFile(memoryStream);
            }
        }

        private string getBuildStr(int? randomInt, 
            string alp, 
            double? doubleNumber)
        {
            string build = string.Empty;
            if (randomInt != null)
                build += randomInt;
            if (!string.IsNullOrEmpty(alp) && !string.IsNullOrEmpty(build))
                build += "," + alp ;
            else if (!string.IsNullOrEmpty(alp) && string.IsNullOrEmpty(build))
                build += alp;

            if (doubleNumber != null && string.IsNullOrEmpty(build))
                build += doubleNumber;
            else if(doubleNumber != null && !string.IsNullOrEmpty(build))
                build += "," + doubleNumber;

                return build;
        }
        public void EndGenerate()
        {
            var state = StartEndStreamWrite.Instance();
            state.End();
        }

        private void WriteInFile(MemoryStream memoryStream)
        {
            try
            {
                string root = HttpContext.Current.Server.MapPath("~/Helper/");
                string path = root + DateTime.Now.Ticks + "stream.txt";
                FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                memoryStream.Position = 0;
                memoryStream.WriteTo(fileStream);
            }
            catch(Exception ex)
            {

            }
        }
    }
}