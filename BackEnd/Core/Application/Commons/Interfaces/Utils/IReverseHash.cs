using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGeneratedCodeApplication.Commons.Interfaces.Utils
{
    public interface IReverseHash
    {
        public void Init(string salt);
        public string Encode(int dataToEncode);
        public int[]  Decode(string dataToDecode);
    }
}
