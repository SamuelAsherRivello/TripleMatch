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
using com.rmc.core.grid_system.data;

//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.triple_match.mvc.model.data.vo
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
	/// <summary>
	/// VO = Value Object. These are purposefully dumb parts of MVC which hold nfo and get sent around as needed between MVC.
	/// </summary>
	public class GemVO : object, IGridSpot
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER
		
		
		// 	PUBLIC
		public int RowIndex {get; set;}
		public int ColumnIndex {get; set;}
		public int TypeIndex {get; set;}


		/// <summary>
		/// 
		/// NOTE: In our case, this is the same as another property, always. 
		/// 		However its reasonable that thew view/model could splinter. Ex. have multiple 'red' gems for cosmetic use only.
		/// 
		/// </summary>
		/// <value>The index of the gem type.</value>
		public int GemTypeIndex
		{
			get
			{
				return TypeIndex;
			}
		}
		
		// 	PRIVATE
		
		
		//--------------------------------------
		// 	Constructor / Creation
		//--------------------------------------	
		
		/// <summary>
		/// Initializes a new instance of the <see cref="com.rmc.projects.triple_match.mvc.model.data.vo.GemVO"/> class.
		/// </summary>
		public GemVO ()
		{
			//parameterless to assist generics
		}
		
		/// <summary>
		/// Initialize the specified rowIndex, columnIndex and typeIndex.
		/// </summary>
		/// <param name="rowIndex">Row index.</param>
		/// <param name="columnIndex">Column index.</param>
		/// <param name="typeIndex">Type index.</param>
		public void Initialize (int rowIndex, int columnIndex, int typeIndex)
		{
			
			RowIndex = rowIndex;
			ColumnIndex = columnIndex;
			TypeIndex = typeIndex;
			
			//
			//Debug.Log (this);
		}

		//--------------------------------------
		// 	Methods
		//--------------------------------------
		
		
		// 	PUBLIC
		
		/// <summary>
		/// Show nice debuggable output
		/// </summary>
		override public string ToString ()
		{
			return "[GemVO (r="+RowIndex+", c="+ColumnIndex+", gti="+GemTypeIndex+")]";
			
		}
		
		
		//	PRIVATE
		
		
		//--------------------------------------
		// 	Event Handlers
		//--------------------------------------
	}
}




