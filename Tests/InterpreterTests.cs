﻿using System;
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
        [TestCase("(define (abs x) (if (< x 0) (- x) x))", "(abs -5)", "5")]
        [TestCase("(define (abs x) (cond ((> x 0) x)((= x 0) 0)((< x 0)(- x))))", "(abs (- 5))", "5")]
        [TestCase("(define (abs x) (cond ((> x 0) x) (else (- x))))", "(abs (- 5))", "5")]
        [TestCase(@"
(define (add a b)
    (+ a b))", 
  "(add 3 4)", "7")]
        [TestCase("(define (inner x) (and (> x 5) (< x 10)))", "(inner 7)", "true")]
        [TestCase("(define (inner x) (and (> x 5) (< x 10)))", "(inner 3)", "false")]
        [TestCase("(define (outer x) (or (< x 5) (> x 10)))", "(outer 7)", "false")]
        [TestCase("(define (outer x) (or (< x 5) (> x 10)))", "(outer 3)", "true")]

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
