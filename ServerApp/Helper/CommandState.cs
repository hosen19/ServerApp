using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Helper
{
    public abstract class CommandState
    {
        public int executionState { get; set; }
        public abstract void Start();
        public abstract void End();
    }

    public class StartEndStreamWrite : CommandState
    {
        static StartEndStreamWrite instance;
        protected StartEndStreamWrite()
        {

        }

        public static StartEndStreamWrite Instance()
        {
            if (instance == null)
            {
                instance = new StartEndStreamWrite();
            }
            return instance;
        }

        public override void End()
        {
            executionState = 0;
        }

        public override void Start()
        {
            executionState = 1;
        }

    }
}