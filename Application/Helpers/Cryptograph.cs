using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace SistemaVenda.Helpers
{
    public class Cryptograph
    {
        public string GetMD5Hash(string value)
        {
            MD5 mD5 = MD5.Create();
            byte[] data = mD5.ComputeHash(Encoding.UTF8.GetBytes(value));
            var retorno = new StringBuilder();
            for(int i = 0; i< data.Length; i++)
            {
                retorno.Append(data[i].ToString("x2"));
            }
            return retorno.ToString();
        }
    }
}
