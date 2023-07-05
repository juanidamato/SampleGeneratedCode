using SampleGeneratedCodeDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGeneratedCodeDomain.Commons
{
    public class OperationResultModel<T>
    {
        public OperationResultCodesEnum code { get; set; }
        public string message { get; set; } = string.Empty;
        public T? payload;
    }
}
