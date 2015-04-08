using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shceme;

namespace Tests
{
    public class TokenizerTests
    {
        [Test]
        public void Test1()
        {
            var t = new Tokenizer();
            var res = t.Parse("5 + 5").ToArray();
            Assert.AreEqual(new String[] {"5", "+", "5"}, res.Select(x => x.Value));
        }

        [Test]
        public void Test2()
        {
            var t = new Tokenizer();
            var res = t.Parse("(5 + 5)").ToArray();
            Assert.AreEqual(new String[] { "(5 + 5)" }, res.Select(x => x.Value));
        }

        [Test]
        public void Test3()
        {
            var t = new Tokenizer();
            var res = t.Parse("define (add a b) (a + b)").ToArray();
            Assert.AreEqual(new String[] { "define", "(add a b)", "(a + b)" }, res.Select(x => x.Value));
        }
    }
}
