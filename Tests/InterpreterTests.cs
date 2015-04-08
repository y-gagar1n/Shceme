using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shceme;

namespace Tests
{
    class InterpreterTests
    {
        [TestCase("5", "5")]
        [TestCase("(+ 5 6)", "11")]
        [TestCase("(+ 5 (+ 3 3))", "11")]
        [TestCase("(+ (+ 4 8) (+ 3 3))", "18")]
        [TestCase("(- 7 2)", "5")]
        [Test]
        public void TestSingleLine(string text, string result)
        {
            var interpreter = new ScmInterpreter();
            Assert.AreEqual(result, interpreter.Run(text));
        }

        [TestCase("(define five 5)", "five", "5")]
        [TestCase("(define five 5)", "(* 2 five)", "10")]
        [TestCase("(define (add a b) (+ a b))", "(add 3 4)", "7")]
        [TestCase("(define (add a b) (+ a b))", "(add (add 1 2) 4)", "7")]
        [TestCase("(define (add a b c) (+ (+ a b) c))", "(add 1 2 4)", "7")]
        [TestCase("(define (sum-of-squares a b) (+ (* a a) (* b b)))", "(sum-of-squares 3 4)", "25")]
        public void TestFunctionCall(string def, string call, string result)
        {
            var interpreter = new ScmInterpreter();
            interpreter.Run(def);
            Assert.AreEqual(result, interpreter.Run(call));
        }
    }
}
