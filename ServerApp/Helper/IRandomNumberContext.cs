using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Helper
{
    public interface IRandomNumberContext
    {
        void GenerateRandomNumber(int range);
    }
}
