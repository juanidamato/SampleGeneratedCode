using SampleGeneratedCodeApplication.Commons.Interfaces.Utils;
using SampleGeneratedCodeApplication.Commons.Utils;

namespace SampleGeneratedCode.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange
            IReverseHash reverseHash = new ReverseHash();
            reverseHash.Init("hola mundo");
            //act
            var a = reverseHash.Encode(1500);
            int[] b = reverseHash.Decode(a);
            //assert
            Assert.NotNull(b);
            Assert.Equal(1,b.Length);
            Assert.Equal(1500,b[0]);
        }
    }
}