using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Helper
{
    public class IntRandomNumber : IRandomNumberGenerator<int> , IRandomNumberContext
    {
        static IntRandomNumber instance;
        protected IntRandomNumber()
        {

        }

        public static IntRandomNumber Instance()
        {
            if (instance == null)
            {
                instance = new IntRandomNumber();
            }
            return instance;
        }

        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        public List<int> list = new List<int>();
        public void GenerateRandomNumber(int range)
        {
            if (list.Count() <= 0)
            {
                for (int index = 0; index < range; index++)
                    list.Add(this.GetRandomNumber());
            }
        }

        public int GetRandomNumber()
        {
            var data = new Byte[8];
            // Fill buffer.
            rng.GetBytes(data);
            return BitConverter.ToInt32(data, 0);
        }
    }
}
