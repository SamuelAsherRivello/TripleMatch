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
using UnityEngine;
using com.rmc.core.grid_system.data;
using System.Collections.Generic;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.core.grid_system
{
	
	//--------------------------------------
	//  Namespace Properties
	//--------------------------------------

	/// <summary>
	/// Frequency determines "How often will we have 1 or more matches upon 'Reset Game'?"
	/// </summary>
	public enum Frequency
	{
		Always,
		Sometimes
		
	}
	
	//--------------------------------------
	//  Class Attributes
	//--------------------------------------

	

	
	//--------------------------------------
	//  Class
	//--------------------------------------
	/// <summary>
	/// The logic for rendering, adding, removing, matching gridspots is isolated here.
	/// 
	/// PURPOSE:
	/// 			1. Reuse
	/// 			2. Unit Testability (isolated from Unity-specific code (e.g. MonoBehaviors and Coroutines)
	/// 
	/// 
	/// </summary>
	public class GridSystem<T> : Object where T : IGridSpot, new()
	{


		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER


		/// <summary>
		/// The GRID SPOT VO.
		/// 
		/// NOTE: We keep them as 2-dimensional array for 'check my neighbor' type grid-checking
		/// 
		/// </summary>
		private T[,] _gridSpotVO_array;
		public T[,] GridSpotVOArray
		{
			get
			{
				return _gridSpotVO_array;
			}
			
		}

		/// <summary>
		/// Gets the list from grid spot V os.
		/// 
		/// NOTE: We keep a List format for Count and loop operations
		/// 
		/// </summary>
		/// <returns>The list from grid spot V os.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public List<T> GridSpotVOList ()
		{
			return ConvertGridSpotVOArrayToList (_gridSpotVO_array);

		}

		
		/// <summary>
		/// Frequency determines "How often will we have 1 or more matches upon 'Reset Game'?"
		/// </summary>
		private Frequency _frequencyOfInstantMatchesUponReset;  

		//	PUBLIC

		
		// 	PUBLIC STATIC
		
		/// <summary>
		/// We do conversions on the complete gridSpotArray sometimes, and also on partial arrays sometimes.
		/// </summary>
		/// <returns>The grid spot VO array to list.</returns>
		/// <param name="gridSpotVO_array">Grid spot V o_array.</param>
		public static List<T> ConvertGridSpotVOArrayToList (T[,] gridSpotVO_array)
		{
			List<T> gridSpotVOList = new List <T>();
			//
			for (int rowIndex_int = 0; rowIndex_int < gridSpotVO_array.GetLength(0); rowIndex_int += 1) 
			{
				for (int columnIndex_int = 0; columnIndex_int < gridSpotVO_array.GetLength(1); columnIndex_int += 1) 
				{
					gridSpotVOList.Add ( gridSpotVO_array[rowIndex_int,columnIndex_int]);
				}
			}
			
			return gridSpotVOList;
			
		}
		
		
		// 	PRIVATE
		private int _maxRows_int;
		private int _maxColumns_int;
		private int _maxGridSpotTypeIndex_int;	
		private int _minMatchesForRewardHorizontal_int; //axis-specific for easy debugging
		private int _minMatchesForRewardVertical_int; //axis-specific for easy debugging


		
		//--------------------------------------
		// 	Constructor / Creation
		//--------------------------------------	
		
		/// <summary>
		/// Initializes a new instance of the <see cref="com.rmc.core.grid_system.GridSystem`1"/> class.
		/// </summary>
		/// <param name="maxRows_int">Max rows_int.</param>
		/// <param name="maxColumns_int">Max columns_int.</param>
		/// <param name="minMatchesForRewardHorizontal_int">Minimum matches for reward horizontal_int.</param>
		/// <param name="minMatchesForRewardVertical_int">Minimum matches for reward vertical_int.</param>
		/// <param name="maxGridSpotTypeIndex_int">Max grid spot type index_int.</param>
		public GridSystem 
			(
				int maxRows_int, 
			  	int maxColumns_int, 
			  	int minMatchesForRewardHorizontal_int, 
			  	int minMatchesForRewardVertical_int,
			  	int maxGridSpotTypeIndex_int
		    ) 
		{
			_maxRows_int = maxRows_int;
			_maxColumns_int = maxColumns_int;
			_minMatchesForRewardHorizontal_int = minMatchesForRewardHorizontal_int;
			_minMatchesForRewardVertical_int = minMatchesForRewardVertical_int;
			_maxGridSpotTypeIndex_int = maxGridSpotTypeIndex_int;
			
		}
		
		
		//--------------------------------------
		// 	Methods
		//--------------------------------------
		
		
		// 	PUBLIC
		
		
		//	PRIVATE
		
		
		//--------------------------------------
		// 	Event Handlers
		//--------------------------------------

		/// <summary>
		/// Reset the grid. This happens upon game launch and upon clicking 'Reset Game'.
		/// </summary>
		/// <param name="frequencyOfInstantMatchesUponReset">Frequency of instant matches upon reset.</param>
		public void Reset (Frequency frequencyOfInstantMatchesUponReset)
		{
			_gridSpotVO_array = new T[_maxRows_int, _maxColumns_int];
			_frequencyOfInstantMatchesUponReset = frequencyOfInstantMatchesUponReset;
			PopulateGrid();


		}

		
		/// <summary>
		/// In early testing of the art, this was helpful to ensure that the data model matches the GridSpot-art used.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="com.rmc.core.grid_system.GridSystem`1"/>.</returns>
		override public string ToString ()
		{
			string s = "";
			s+= "[GridSystem] (Single Click Here For Grid Type Output)";
			s+= "\n";
			for (int rowIndex_int = 0; rowIndex_int < _gridSpotVO_array.GetLength(0); rowIndex_int += 1) 
			{
				for (int columnIndex_int = 0; columnIndex_int < _gridSpotVO_array.GetLength(1); columnIndex_int += 1) 
				{
					s += _gridSpotVO_array[rowIndex_int,columnIndex_int].TypeIndex;
				}
				s += "\n";
			}
			return s;
			
		}


		/// <summary>
		/// Populates the grid.
		/// </summary>
		public void PopulateGrid ()
		{
			
			//	CREATE ALL NEW GRID SPOT
			T nextGridSpotVO;
			int nextGridSpotTypeIndex_int;
			
			//
			//
			//	TOP TO BOTTOM
			for (int rowIndex_int = 0; rowIndex_int < _gridSpotVO_array.GetLength (0); rowIndex_int++)
			{
				//	LEFT TO RIGHT
				for (int columnIndex_int = 0; columnIndex_int < _gridSpotVO_array.GetLength (1); columnIndex_int++)
				{


					//	DUE TO THE GRID SIZE (64 DEFAULT) AND THE GRID SPOT VARIETY (5 TYPES)...
					//		1. IT IS LIKELY THAT There will be a match
					//
					//	choose 'any' gridSpot type and don't worry if it matches a neighbor
					nextGridSpotTypeIndex_int = Random.Range (0, _maxGridSpotTypeIndex_int);
					nextGridSpotVO = new T ();
					nextGridSpotVO.Initialize(rowIndex_int, columnIndex_int, nextGridSpotTypeIndex_int);
					_gridSpotVO_array[rowIndex_int, columnIndex_int] = nextGridSpotVO;
					
					
				}
			}


			//	DUE TO THE GRID SIZE (64 DEFAULT) AND THE GRID SPOT VARIETY (5 TYPES)...
			//		1. IT IS LIKELY THAT There will be a match
			//		2. In the rare event we don't have a match, repeat

			if (_frequencyOfInstantMatchesUponReset == Frequency.Always && !HasMatches())
			{
				PopulateGrid();
			}

		}


		/// <summary>
		/// Just after swapping 2 GridSpots, this determines if the swapped GridSpots are part of a match. If not, we'll swap them back.
		/// </summary>
		/// <returns><c>true</c> if this instance is there A match containing either grid spot V the specified gridSpotVO1 gridSpotVO2;
		/// otherwise, <c>false</c>.</returns>
		/// <param name="gridSpotVO1">Grid spot V o1.</param>
		/// <param name="gridSpotVO2">Grid spot V o2.</param>
		public bool IsThereAMatchContainingEitherGridSpotVO (T gridSpotVO1, T gridSpotVO2)
		{
			
			bool isThereAMatchContainingEitherGridSpotVO = false;
			
			//	1. BUILD LIST OF MATCHES
			List<List<T>> gridSpotVOsMatchingInAllChecksListOfLists = GetMatches();
			
			foreach (List<T> gridSpotVOList in gridSpotVOsMatchingInAllChecksListOfLists)
			{
				if (gridSpotVOList.Contains (gridSpotVO1) || gridSpotVOList.Contains (gridSpotVO2))
				{
					
					isThereAMatchContainingEitherGridSpotVO = true;
					break;
				}
				
			}
			
			return isThereAMatchContainingEitherGridSpotVO;
		}

		
		
		/// <summary>
		/// Swaps the data instantly. This is not the tweening of the view GridSpots, only the data.
		/// </summary>
		/// <param name="gridSpotVO1">Grid spot V o1.</param>
		/// <param name="gridSpotVO2">Grid spot V o2.</param>
		public void DoInstantlySwapTwoGridSpotVOs (T gridSpotVO1, T gridSpotVO2)
		{
			
			//	1. SWAP INTERNAL DATA
			int rowIndex = gridSpotVO1.RowIndex;
			int columnIndex = gridSpotVO1.ColumnIndex;
			//
			gridSpotVO1.RowIndex = gridSpotVO2.RowIndex;
			gridSpotVO1.ColumnIndex = gridSpotVO2.ColumnIndex;
			gridSpotVO2.RowIndex = rowIndex;
			gridSpotVO2.ColumnIndex = columnIndex;
			
			//	2. SWAP ITS 'ADDRESS' IN THE GRID
			_gridSpotVO_array[gridSpotVO1.RowIndex, gridSpotVO1.ColumnIndex] = gridSpotVO1;
			_gridSpotVO_array[gridSpotVO2.RowIndex, gridSpotVO2.ColumnIndex] = gridSpotVO2;
			
		}
		
		
		
		
		/// <summary>
		/// </summary>
		/// <returns><c>true</c> if this instance has matches; otherwise, <c>false</c>.</returns>
		public bool HasMatches ()
		{
			return GetMatches().Count > 0;
		}

		
		/// <summary>
		/// </summary>
		/// <returns>The matches.</returns>
		public List<List<T>> GetMatches ()
		{
			List<List<T>> gridSpotVOsMatchingInAllChecksListOfLists = new List<List<T>>();
			gridSpotVOsMatchingInAllChecksListOfLists.AddRange (_GetMatchesHorizontal());
			gridSpotVOsMatchingInAllChecksListOfLists.AddRange (_GetMatchesVertical());
			return gridSpotVOsMatchingInAllChecksListOfLists;

		}

		
		
		
		/// <summary>
		/// We sweep through the core data and mark data for (visual, animated) deletion in the view.
		/// </summary>
		/// <param name="gridSpotVOsMatchingListOfLists">Grid spot V os matching list of lists.</param>
		public void DoMarkGridSpotVOsForDeletion (List<List<T>> gridSpotVOsMatchingListOfLists)
		{
			
			//	1. REMOVE FROM MASTER LIST, NOW THE MODEL HAS NO RECORD OF THEM ANYMORE. THAT IS OK, JUST REMEMBER THAT
			foreach (List<T> gridVOList in gridSpotVOsMatchingListOfLists)
			{
				foreach (T gridSpot in gridVOList)
				{
					//
					for (int rowIndex_int = 0; rowIndex_int < _gridSpotVO_array.GetLength (0); rowIndex_int++)
					{
						for (int columnIndex_int = 0; columnIndex_int < _gridSpotVO_array.GetLength (1); columnIndex_int++)
						{

							if (_gridSpotVO_array[rowIndex_int, columnIndex_int] != null &&
							    _gridSpotVO_array[rowIndex_int, columnIndex_int].Equals (gridSpot))
							{
								_gridSpotVO_array[rowIndex_int, columnIndex_int] = default(T);
							};
						}
					}
				}
			}
			
		}


		
		/// <summary>
		/// Dos the fill gaps in grid spots_ overall.
		/// 
		/// NOTE: This is step 1 of 3 for 'filling'.
		/// 
		/// </summary>
		/// <returns>The fill gaps in grid spots_ overall.</returns>
		public int DoFillGapsInGridSpots_Overall ()
		{
			
			
			//todo: Remove this 
			//3. Count the gaps. This is for debugging only
			//
			int totalAmountRemoved = 0;
			for (int rowIndex_int = 0; rowIndex_int < _gridSpotVO_array.GetLength(0); rowIndex_int++)
			{
				for (int columnIndex_int = 0; columnIndex_int < _gridSpotVO_array.GetLength(1); columnIndex_int++)
				{
					
					if (_gridSpotVO_array[rowIndex_int, columnIndex_int] == null)
					{
						totalAmountRemoved++;
					};
					
				}
			}
			
			//Debug.Log ("totalAmountRemoved: " + totalAmountRemoved);
			
			return totalAmountRemoved;
			
			
		}
		

		//	PRIVATE

		
		/// <summary>
		/// Existing GridSpots drop into empty spaces
		/// 
		/// NOTE: This is step 2 of 3 for 'filling'.
		/// 
		/// </summary>
		/// <returns>The fill gaps in grid spots__ shift down.</returns>
		public List<T> DoFillGapsInGridSpots__ShiftDown ()
		{
			
			List<T> gridSpotVOsMarkedForShiftingDownChanged = new List<T>();
			
			// START AT THE BOTTOM ROW
			//TODO: WHY -1 here?
			for (int rowIndexToCheck_int = _gridSpotVO_array.GetLength(0) -1; rowIndexToCheck_int >= 0; rowIndexToCheck_int--)
			{
				//	CHECK LEFT TO RIGHT
				for (int columnIndexToCheck_int = 0; columnIndexToCheck_int < _gridSpotVO_array.GetLength(1); columnIndexToCheck_int++)
				{
					
					//IS A SPOT NULL?
					if (_gridSpotVO_array[rowIndexToCheck_int, columnIndexToCheck_int] == null)
					{
						//...THEN CHECK EACH SPOT ABOVE
						for (int rowIndexToFind_int = rowIndexToCheck_int; rowIndexToFind_int >= 0; rowIndexToFind_int--)
						{
							// ...AND SWAP FIRST NON-NULL (above) INTO THE NULL SPOT (Below)
							if (_gridSpotVO_array[rowIndexToFind_int, columnIndexToCheck_int] != null)
							{
								
								
								//COPY THE OLD TO THE NEW
								_gridSpotVO_array[rowIndexToCheck_int, columnIndexToCheck_int] = _gridSpotVO_array[rowIndexToFind_int, columnIndexToCheck_int];
								gridSpotVOsMarkedForShiftingDownChanged.Add (_gridSpotVO_array[rowIndexToCheck_int, columnIndexToCheck_int]);
								_gridSpotVO_array[rowIndexToFind_int, columnIndexToCheck_int] = default(T); //CLEAR OUT THE OLD 
								
								//UPDATE THE PROPERTIES WITHIN THE NEW, SO THE VIEW CAN TWEEN TO NEW POSITION
								_gridSpotVO_array[rowIndexToCheck_int, columnIndexToCheck_int].RowIndex = rowIndexToCheck_int;
								_gridSpotVO_array[rowIndexToCheck_int, columnIndexToCheck_int].ColumnIndex = columnIndexToCheck_int;
								
								break;
							}
							
						}
					}
					
				}
			}
			
			return gridSpotVOsMarkedForShiftingDownChanged;
		}
		
		/// <summary>
		/// New Gridspots are created and fall into the empty spaces. These empty spaces have nothing above them at the moment.
		/// 
		/// NOTE: This is step 3 of 3 for 'filling'.
		/// 
		/// </summary>
		/// <returns>The fill gaps in grid spots__ drop new from above.</returns>
		public List<T> DoFillGapsInGridSpots__DropNewFromAbove ()
		{
			
			List<T> gridSpotVOsAddedToFillGapsChanged = new List<T>();
			T nextGridSpot;
			int nextGridSpotTypeIndex_int;
			
			// START AT THE BOTTOM ROW
			for (int rowIndex_int = _gridSpotVO_array.GetLength(0) -1; rowIndex_int >= 0; rowIndex_int--)
			{
				//	CHECK LEFT TO RIGHT
				for (int columnIndex_int = 0; columnIndex_int < _gridSpotVO_array.GetLength(1); columnIndex_int++)
				{
					if (_gridSpotVO_array[rowIndex_int, columnIndex_int] == null)
					{
						//	SET INDEX (THIS MEANS THE COLOR)
						nextGridSpotTypeIndex_int = Random.Range (0, _maxGridSpotTypeIndex_int);
						
						//	CREATE 1 NEW GRID SPOT
						nextGridSpot = new T();
						nextGridSpot.Initialize(rowIndex_int, columnIndex_int, nextGridSpotTypeIndex_int);
						_gridSpotVO_array[rowIndex_int, columnIndex_int] = nextGridSpot;
						gridSpotVOsAddedToFillGapsChanged.Add (nextGridSpot);
					}
				}
			}
			
			return gridSpotVOsAddedToFillGapsChanged;
			
		}



		//	PRIVATE



		//------------------------------------------------------------------------------
		//
		//
		//  FINDING MATCHES - This is one of the core routines of the game logic.
		//
		//
		//	NOTES:
		//			1. 	Horizontally and Vertically are handled separately
		//			2. 	This allows readability and testability (axis-specifically)
		//			3. 	Admittadly its a duplication of code and 'doubles?' the 
		//					maintainability of this area. That is ok for this demo.
		//
		//
		//------------------------------------------------------------------------------

		/// <summary>
		/// _s the get matches horizontal.
		/// </summary>
		/// <returns>The get matches horizontal.</returns>
		private List<List<T>> _GetMatchesHorizontal ()
		{

			
			List<List<T>> gridSpotVOsMatchingInAllChecksListOfLists = new List<List<T>>();
			//HORIZONTAL
			for (int rowIndex_int = 0; rowIndex_int < _gridSpotVO_array.GetLength(0); rowIndex_int += 1) 
			{

				//clear matches
				List<T> gridSpotVOsMatchingInCurrentCheck = new List<T>();
				
				//VERTICAL
				for (int columnIndex_int = 0; columnIndex_int < _gridSpotVO_array.GetLength(1); columnIndex_int += 1) 
				{
					
					T gridSpotVO = _gridSpotVO_array[rowIndex_int, columnIndex_int];
					
					//NOTE: WE DO A NUL CHECK, BECAUSE WE ALSO RUN HasMatches() before the grid is completely drawn in.
					if (gridSpotVO != null)
					{
						//	FIRST CHECK IN THIS AXIS?, ADD IT!
						if (gridSpotVOsMatchingInCurrentCheck.Count == 0)
						{
							gridSpotVOsMatchingInCurrentCheck.Add (gridSpotVO);
						}
						//	NOT THE FIRST CHECK IN THIS AXIS?, CHECK FOR MATCHING TYPE!
						else if (gridSpotVOsMatchingInCurrentCheck[0].TypeIndex == gridSpotVO.TypeIndex)
						{
							gridSpotVOsMatchingInCurrentCheck.Add (gridSpotVO);
							
						}
						
						
						//	NEXT DOESN'T MATCH PREVIOUS,...
						//	OR END OF THE AXIS?
						if (gridSpotVOsMatchingInCurrentCheck[0].TypeIndex != gridSpotVO.TypeIndex ||
						    columnIndex_int == _gridSpotVO_array.GetLength(1) -1)
						{
							//	DO WE HAVE ENOUGH TO MAKE A REWARD?
							if (gridSpotVOsMatchingInCurrentCheck.Count >= _minMatchesForRewardHorizontal_int)
							{
								gridSpotVOsMatchingInAllChecksListOfLists.Add (gridSpotVOsMatchingInCurrentCheck);
							}

							//	CLEAR OUT CURRENT LIST
							gridSpotVOsMatchingInCurrentCheck = new List<T>();
							gridSpotVOsMatchingInCurrentCheck.Add (gridSpotVO);
						}
					}
					
				}
			}
			
			return gridSpotVOsMatchingInAllChecksListOfLists;
		}
		
		
		/// <summary>
		/// _s the get matches vertical.
		/// </summary>
		/// <returns>The get matches vertical.</returns>
		private List<List<T>> _GetMatchesVertical()
		{
			
			List<List<T>> gridSpotVOsMatchingInAllChecksListOfLists = new List<List<T>>();
			
			//VERTICAL
			for (int columnIndex_int = 0; columnIndex_int < _gridSpotVO_array.GetLength(1); columnIndex_int += 1) 
			{

				//clear matches
				List<T> gridSpotVOsMatchingInCurrentCheck = new List<T>();
				
				//HORIZONTAL
				for (int rowIndex_int = 0; rowIndex_int < _gridSpotVO_array.GetLength(0); rowIndex_int += 1) 
				{

					T nextGridSpotVO = _gridSpotVO_array[rowIndex_int, columnIndex_int];
					//NOTE: WE DO A NUL CHECK, BECAUSE WE ALSO RUN HasMatches() before the grid is completely drawn in.
					if (nextGridSpotVO != null)
					{
						
						//	FIRST CHECK IN THIS AXIS?, ADD IT!
						if (gridSpotVOsMatchingInCurrentCheck.Count == 0)
						{
							gridSpotVOsMatchingInCurrentCheck.Add (nextGridSpotVO);
						}
						//	NOT THE FIRST CHECK IN THIS AXIS?, CHECK FOR MATCHING TYPE!
						else if (gridSpotVOsMatchingInCurrentCheck[0].TypeIndex == nextGridSpotVO.TypeIndex)
						{
							gridSpotVOsMatchingInCurrentCheck.Add (nextGridSpotVO);
							
						}
						
						
						//	NEXT DOESN'T MATCH PREVIOUS,...
						//	OR END OF THE AXIS?
						if (gridSpotVOsMatchingInCurrentCheck[0].TypeIndex != nextGridSpotVO.TypeIndex ||
						    rowIndex_int == _gridSpotVO_array.GetLength(0) -1)
						{
							//	DO WE HAVE ENOUGH TO MAKE A REWARD?
							if (gridSpotVOsMatchingInCurrentCheck.Count >= _minMatchesForRewardVertical_int)
							{
								gridSpotVOsMatchingInAllChecksListOfLists.Add (gridSpotVOsMatchingInCurrentCheck);
							}
							
							//	CLEAR OUT CURRENT LIST
							gridSpotVOsMatchingInCurrentCheck = new List<T>();
							gridSpotVOsMatchingInCurrentCheck.Add (nextGridSpotVO);
							
						}
					}
					
				}
			}
			
			return gridSpotVOsMatchingInAllChecksListOfLists;
			
		}	


	}
}
