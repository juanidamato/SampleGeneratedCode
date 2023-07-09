using HashidsNet;
using SampleGeneratedCodeApplication.Commons.Interfaces.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGeneratedCodeApplication.Commons.Utils
{
    public class ReverseHash : IReverseHash
    {
        private  string _salt="";

        public void Init(string salt)
        {
            _salt = salt;
        }

        public string Encode(int dataToEncode)
        {
            Hashids encoder = new Hashids(_salt);
            return encoder.Encode(dataToEncode);
     
        }

        public int[] Decode(string dataToDecode)
        {
            Hashids decoder = new Hashids(_salt);
            return decoder.Decode(dataToDecode);
        }

       
    }
}
