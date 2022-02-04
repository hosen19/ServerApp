using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Helper
{
    public interface IRandomNumberGenerator<T> 
    {
        T GetRandomNumber();
    }
}
