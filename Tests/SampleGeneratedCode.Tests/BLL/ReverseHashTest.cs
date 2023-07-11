using SampleGeneratedCodeApplication.Commons.Interfaces.Utils;
using SampleGeneratedCodeApplication.Commons.Utils;

namespace SampleGeneratedCode.Tests.BLL
{
    public class ReverseHashTest
    {
        [Fact]
        public void RevesrseHash_EncodeDecode_Return_SameValue()
        {
            //arrange
            IReverseHash reverseHash = new ReverseHash();
            reverseHash.Init("helllo world");
            //act
            var a = reverseHash.Encode(1500);
            int[] b = reverseHash.Decode(a);
            //assert
            Assert.NotNull(b);
            Assert.Equal(1, b?.Length);
            Assert.Equal(1500, b?[0]);
        }
    }
}