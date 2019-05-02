using FUNDOOAPP.views;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class Tests

    {
        private readonly Signup signup;

        public Tests()
        {
            signup = new Signup();
        }

        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Test1()
        {
            var result = this.signup.CheckValidation();
            Assert.IsTrue(result);
        }
        [Test]
        public async Task Test3()
        {
            var root = new Signup();
            var page = new Signup();
            await root.Navigation.PushAsync(page);
            Assert.AreEqual(root.Navigation.NavigationStack.Last(), page);
        }
    }
}