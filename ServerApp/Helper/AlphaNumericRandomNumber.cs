using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Helper
{
    public class AlphaNumericRandomNumber : IRandomNumberGenerator<string>, IRandomNumberContext
    {
        private static readonly char[] chars =
            " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        private  static int size = 16;
        private static Random random = new Random();

        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        static AlphaNumericRandomNumber instance;
        protected AlphaNumericRandomNumber()
        {

        }

        public static AlphaNumericRandomNumber Instance()
        {
            if (instance == null)
            {
                instance = new AlphaNumericRandomNumber();
            }
            return instance;
        }

        public List<string> list = new List<string>();
        public void GenerateRandomNumber(int range)
        {
            if (list.Count() <= 0)
            {
                for (int index = 0; index < range; index++)
                    list.Add(this.GetRandomNumber());
            }
        }

        public string GetRandomNumber()
        {
            int range = 1;

            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }
            if (!Char.IsWhiteSpace(result[0]) || !Char.IsWhiteSpace(result[(size - 1) % size]))
            {
                range = random.Next(1, 9);
                var mod = range % 2;
                for(int index = 0; index <= range; index ++)
                {
                    if (mod == 0)
                        result.Append(" ");
                    else
                        result.Insert(0," ");
                }
            }

            return result.ToString();
        }
      
    }
}
