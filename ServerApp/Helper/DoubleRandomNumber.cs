using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Helper
{
    public class DoubleRandomNumber : IRandomNumberGenerator<double>, IRandomNumberContext
    {
        static DoubleRandomNumber instance;
        protected DoubleRandomNumber()
        {

        }

        public static DoubleRandomNumber Instance()
        {
            if (instance == null)
            {
                instance = new DoubleRandomNumber();
            }
            return instance;
        }

        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        public List<double> list = new List<double>();
        public void GenerateRandomNumber(int range)
        {
            if(list.Count() <= 0)
            {
                for (int index = 0; index < range; index++)
                    list.Add(this.GetRandomNumber());
            }
        }

        public double GetRandomNumber()
        {
            var data = new Byte[8];
            // Fill buffer.
            rng.GetBytes(data);
            var ul = BitConverter.ToUInt64(data, 0);
            Double d = ul / (Double)(1UL << 59);
            return d;
        }
    }
}
