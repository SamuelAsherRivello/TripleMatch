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
using com.rmc.projects.triple_match.mvc.model.data.vo;
using com.rmc.projects.triple_match;
using System.Collections.Generic;
using System.Linq;

//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.core.grid_system
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
	public class GridSystemTest
	{
		

		//--------------------------------------
		//  Setup
		//--------------------------------------
		
		//	PROPERTIES TO REUSE ACROSS TESTS
		private GridSystem<GemVO> _gridSystem;

		//	CALLED BEFORE EVERY 'TEST' METHOD IN THIS FIXTURE
		[SetUp] 
		public void SetUp()
		{
			_gridSystem = new GridSystem<GemVO>
				(
					TripleMatchConstants.MAX_ROWS, 
					TripleMatchConstants.MAX_COLUMNS,
					TripleMatchConstants.MIN_GEMS_PER_HORIZONTAL_AXIS_MATCH_REWARD,
					TripleMatchConstants.MIN_GEMS_PER_VERTICAL_AXIS_MATCH_REWARD,
					TripleMatchConstants.MAX_GEM_TYPE_INDEX
				);
			
			
		}
		
		
		//	CALLED AFTER EVERY 'TEST' METHOD IN THIS FIXTURE
		[TearDown] 
		public void TearDown()
		{
			_gridSystem = null;
		}



		//--------------------------------------
		//  Tests
		//--------------------------------------


		[Test]
		/// <summary>
		/// The instance will not be null after Initialization
		/// </summary>
		public void InstantiationTest ()
		{

			Assert.NotNull (_gridSystem);
		}


		[Test]
		/// <summary>
		/// After Reset we will have x*y gridspots in an x by y grid
		/// </summary>
		public void GameResetTest ()
		{
			_gridSystem.Reset(Frequency.Sometimes);
			int expectedValue = TripleMatchConstants.MAX_ROWS * TripleMatchConstants.MAX_COLUMNS;
			int actualValue = _gridSystem.GridSpotVOList().Count (gemVO => gemVO != null);
			Assert.AreEqual (expectedValue, actualValue);
		}


		[Test]
		/// <summary>
		/// If we want to ALWAYS have a match at reset, then we will.
		/// </summary>
		public void FindMatchesAlwaysWithSuccessTest ()
		{
			_gridSystem.Reset(Frequency.Always);

			//	REPEATING X TIMES IS A GOOD TEST FOR FREQUENCY
			int totalIterations_int = 10;
			for (var i=0; i < totalIterations_int; i++)
			{
				int expectedValue = 0; //we expect one or more matches
				int actualValue = _gridSystem.GetMatches().Count;
				Assert.Greater (actualValue, expectedValue);
			}
		}


		[Test]
		/// <summary>
		/// If we want to SOMETIMES have a match at reset, then BY AVERAGE we will NOT always have a 1+ matches and NOT have 0 matches.
		/// </summary>
		public void FindMatchesSometimesWithSuccessTest ()
		{

			
			//	REPEATING X TIMES IS A GOOD TEST FOR FREQUENCY
			int totalIterations_int = 1000;
			int foundAMatchCount_int = 0;
			for (var i=0; i < totalIterations_int; i++)
			{
				_gridSystem.Reset(Frequency.Sometimes);
				if (_gridSystem.GetMatches().Count > 0)
				{
					foundAMatchCount_int++;
				}
			}

			//	IF WE ITERATE 100 TIMES WE EXPECT TO FIND **BETWEEN** 0 AND 100 (EXCLUSIVE) MATCHES
			int actualValue = foundAMatchCount_int;
			int expectedValueMax = totalIterations_int; 
			int expectedValueMin = 0; 
			Assert.Less (actualValue, expectedValueMax);
			Assert.Greater (actualValue, expectedValueMin);
		}


		[Test]
		/// <summary>
		/// If we ALWAYS have a match upon reset, and then we remove those matches, we should have < x*y non-null gridspots
		/// </summary>
		public void RemoveMatchesTest ()
		{

			_ResetAndRemoveMatches();

			//	WE WILL HAVE LESS THAN X*Y NON-NULL GRIDSPOTS
			int expectedValueMax = TripleMatchConstants.MAX_ROWS * TripleMatchConstants.MAX_COLUMNS;
			int actualValue = _gridSystem.GridSpotVOList().Count (gemVO => gemVO != null);
			Assert.Less ( actualValue, expectedValueMax);
		}



		[Test]
		/// <summary>
		/// If we remove matches, then fill them, we should have x*y not-null GridSpots again
		/// </summary>
		public void RemoveMatchesAndFillGapsTest ()
		{

			//	REMOVE
			_ResetAndRemoveMatches();

			//	REGAIN SOME FOR 100% TOTAL
			_gridSystem.DoFillGapsInGridSpots__ShiftDown();
			_gridSystem.DoFillGapsInGridSpots__DropNewFromAbove();

			
			//	WE WILL HAVE EQUAL TO X*Y NON-NULL GRIDSPOTS
			int expectedValue = TripleMatchConstants.MAX_ROWS * TripleMatchConstants.MAX_COLUMNS;
			int actualValue = _gridSystem.GridSpotVOList().Count (gemVO => gemVO != null);
			Assert.AreEqual ( actualValue, expectedValue);
		}

		
		[Test]
		/// <summary>
		/// Any time we will have a match, the 'IsThereAMatchContainingEitherGemVO()' will equal true at least once. 
		/// 
		/// NOTE: 	When we have x 'matches' in total, we don't expect 'IsThereAMatchContainingEitherGemVO()' to be true x times. 
		/// 		That is not logical and we don't test for it.
		/// 
		/// </summary>
		public void IsThereAMatchContainingEitherGemVOTest	 ()
		{

			bool isThereAMatchAtLeastOneTime_bool = false;


			//	WE MUST HAVE MATCHES
			_gridSystem.Reset(Frequency.Always);

			//	CHECK EVERY SINGLE GRIDSPOT AGAINST EVERY GRIDSPOT (INCLUDING SELF VS SELF)
			//		1. WE SHOULD HAVE AT LEAST ONE MATCH (SINCE WE SET TO 'ALWAYS' ABOVE
			GemVO gemVO1;
			GemVO gemVO2;
			for (int rowIndex1_int = 0; rowIndex1_int < _gridSystem.GridSpotVOArray.GetLength(0); rowIndex1_int++) 
			{
				for (int columnIndex1_int = 0; columnIndex1_int < _gridSystem.GridSpotVOArray.GetLength(1); columnIndex1_int++) 
				{

					//	CHOOSE A GRIDSPOT
					gemVO1 = _gridSystem.GridSpotVOArray[rowIndex1_int, columnIndex1_int];

					for (int rowIndex2_int = 0; rowIndex2_int < _gridSystem.GridSpotVOArray.GetLength(0); rowIndex2_int++) 
					{
						for (int columnIndex2_int = 0; columnIndex2_int < _gridSystem.GridSpotVOArray.GetLength(1); columnIndex2_int++) 
						{

							//	CHOOSE ANOTHER (OR SAME!) GRIDSPOT
							gemVO2 = _gridSystem.GridSpotVOArray[rowIndex2_int, columnIndex2_int];

							if (_gridSystem.IsThereAMatchContainingEitherGridSpotVO (gemVO1, gemVO2))
							{
								isThereAMatchAtLeastOneTime_bool = true;

								//
								//
								//	ELEGANT TRICK TO BREAK OUT OF ALL FOUR LOOPS (FYI, 'break;' only exits inner loop)
								columnIndex2_int = _gridSystem.GridSpotVOArray.GetLength(1);
								rowIndex2_int = _gridSystem.GridSpotVOArray.GetLength(0);
								rowIndex1_int = _gridSystem.GridSpotVOArray.GetLength(0);
								columnIndex1_int = _gridSystem.GridSpotVOArray.GetLength(1);
								break;//unecessary, but VERY helpful to catch the human eye and inform on what is happening
								//
								//
								//
							}
						}
					}
				}
			}

			
			//	WE EXPECT TRUE, ALWAYS
			bool actualValue = isThereAMatchAtLeastOneTime_bool;
			Assert.IsTrue ( actualValue );
			
		}



		
		
		
		//--------------------------------------
		//  Methods
		//--------------------------------------

		
		/// <summary>
		/// NOT A TEST. Just a helper. Its 'methodized' for resuse in 2+ tests
		/// </summary>
		private void _ResetAndRemoveMatches ()
		{
			
			//	WE MUST HAVE MATCHES
			_gridSystem.Reset(Frequency.Always);
			
			//	FIND MATCHES AND MARK THEM FOR DELETION
			List<List<GemVO>> matchingGridSpotVOsListOfLists = _gridSystem.GetMatches();
			_gridSystem.DoMarkGridSpotVOsForDeletion (matchingGridSpotVOsListOfLists);
			
		}
		



		
		//--------------------------------------
		//  Events
		//--------------------------------------
		
		
		
	}
}
