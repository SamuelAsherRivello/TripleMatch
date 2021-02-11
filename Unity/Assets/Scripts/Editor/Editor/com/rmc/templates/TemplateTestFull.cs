/**
 * Copyright (C) 2005-2015 by Rivello Multimedia Consulting (RMC).                    
 * code [at] RivelloMultimediaConsulting [dot] com                                                  
 *                                                                      
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the      
 * "Software"), to deal in the Software without restriction, including  
 * without limitation the rights to use, copy, modify, merge, publish,  
 * distribute, sublicense, and#or sell copies of the Software, and to   
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:                                            
 *                                                                      
 * The above copyright notice and this permission notice shall be       
 * included in all copies or substantial portions of the Software.      
 *                                                                      
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,      
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF   
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR    
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.                                      
 */
// Marks the right margin of code *******************************************************************


//--------------------------------------
//  Imports
//--------------------------------------
using NUnit.Framework;
using System;
using System.Threading;

//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.templates
{
	
	//--------------------------------------
	//  Namespace Properties
	//--------------------------------------
	
	//--------------------------------------
	//  Class Attributes
	//--------------------------------------
	
	//--------------------------------------
	//  Class
	//--------------------------------------
	[TestFixture]
	public class TemplateTestFull
	{

		/*
		//--------------------------------------
		//  Setup
		//--------------------------------------
		
		//PROPERTIES TO REUSE


		//CALLED EXACTLY ONCE BEFORE THE FIRST TEST
		[TestFixtureSetUp] 
		public void testFixtureSetUp()
		{ 
			Debug.Log ("TemplateTestFull.testFixtureSetUp()");
		}

		//CALLED EXACTLY ONCE AFTER THE LAST TEST
		[TestFixtureTearDown] 
		public void testFixtureTearDown()
		{
			Debug.Log ("TemplateTestFull.testFixtureTearDown()");
		}


		//CALLED BEFORE EVERY 'TEST' METHOD IN THIS FIXTURE
		[SetUp] 
		public void setUp()
		{
			Debug.Log ("  -TemplateTestFull.setup()");
			
			
		}
		
		
		//CALLED AFTER EVERY 'TEST' METHOD IN THIS FIXTURE
		[TearDown] 
		public void tearDown()
		{
			Debug.Log ("  -TemplateTestFull.tearDown()");
		}

		//--------------------------------------
		//  SampleTests
		//--------------------------------------
		[Test]
		public void test1 ()
		{
			Debug.Log ("     **TemplateTestFull.test1()");
			Assert.Pass();
		}

		[Test]
		public void test2 ()
		{
			Debug.Log ("     **TemplateTestFull.test2()");
			Assert.Pass();
		}

		*/


		//--------------------------------------
		//  More Tests
		//--------------------------------------
		/*
		[Test]
		public void ExceptionTest ()
		{
			throw new Exception ("Exception throwing test");
		}
		
		[Test]
		[Ignore ("Ignored test")]
		public void IgnoredTest ()
		{
			throw new Exception ("Ignored this test");
		}
		
		[Test]
		[MaxTime (100)]
		public void SlowTest ()
		{
			Thread.Sleep (200);
		}
		
		[Test]
		public void FailingTest ()
		{
			Assert.Fail ();
		}
		
		[Test]
		public void InconclusiveTest ()
		{
			Assert.Inconclusive();
		}
		
		[Test]
		public void PassingTest ()
		{
			Assert.Pass ();
		}
		
		[Test]
		public void ParameterizedTest ([Values (1, 2, 3)] int a)
		{
			Assert.Pass ();
		}
		
		[Test]
		public void RangeTest ( [Range (1, 10, 3)] int x )
		{
			Assert.Pass ();
		}
		
		[Test]
		[Culture ("pl-PL")]
		public void CultureSpecificTest ()
		{
		}
		
		[Test]
		[ExpectedException (typeof (ArgumentException), ExpectedMessage = "expected message")]
		public void ExpectedExceptionTest ()
		{
			throw new ArgumentException ("expected message");
		}
		
		[Datapoint]
		public double zero = 0;
		[Datapoint]
		public double positive = 1;
		[Datapoint]
		public double negative = -1;
		[Datapoint]
		public double max = double.MaxValue;
		[Datapoint]
		public double infinity = double.PositiveInfinity;
		
		[Theory]
		public void SquareRootDefinition ( double num )
		{
			Assume.That (num >= 0.0 && num < double.MaxValue);
			
			var sqrt = Math.Sqrt (num);
			
			Assert.That (sqrt >= 0.0);
			Assert.That (sqrt * sqrt, Is.EqualTo (num).Within (0.000001));
		}
		
		*/
		//--------------------------------------
		//  Events
		//--------------------------------------



	}
}
