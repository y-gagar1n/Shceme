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
        [TestCase("(* (+ 2 (* 4 6))(+ 3 5 7))", "390")]
        [TestCase("(- 7 2)", "5")]
        [TestCase("(> 3 2)", "true")]
        [TestCase("(define five 5)", "five", "5")]
        [TestCase("(define five (+ 2 3))", "(five)", "5")]
        [TestCase("(define minus-five (- 5))", "(minus-five)", "-5")]
        [TestCase("(define five 5)", "(* 2 five)", "10")]
        [TestCase("(define (add a b) (+ a b))", "(add 3 4)", "7")]
        [TestCase("(define (add a b) (+ a b))", "(add (add 1 2) 4)", "7")]
        [TestCase("(define (add a b c) (+ (+ a b) c))", "(add 1 2 4)", "7")]
        [TestCase("(define (sum-of-squares a b) (+ (* a a) (* b b)))", "(sum-of-squares 3 4)", "25")]
        [TestCase("(define pi 3.14159)", "(define radius 10)", "(* pi (* radius radius))", "314.159")]
        [TestCase("(define (positive x) (cond ((x > 0) 1) ((x < 0) 2)))", "(positive 5)", "1")]
        public void TestMultiline(params string[] lines)
        {
            var interpreter = new ScmInterpreter();
            string result = "";
            for (int i = 0; i < lines.Length - 1; i++)
            {
                result = interpreter.Run(lines[i]);
            }
            
            Assert.That(result, Is.EqualTo(lines.Last()));
        }
    }
}
