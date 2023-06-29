using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGeneratedCodeApplication.Commons.Utils
{
    public static class StringUtils
    {
        private static string ValidLatinCharacters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        public static bool ValidateLatinCharacters(string value)
        {
            if (value == null)
            {
                return false;
            }
            for(int i=0;i<=value.Length-1;i++) { 
                if (ValidLatinCharacters.IndexOf(  value.Substring(i,1)  )==-1 )
                {
                    return false;
                }
            }
            return true;
        }
    }
}
