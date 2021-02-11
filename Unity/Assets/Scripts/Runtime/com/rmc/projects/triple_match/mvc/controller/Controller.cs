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
using com.rmc.projects.triple_match.mvc.model;
using com.rmc.projects.triple_match.mvc.model.data.vo;
using com.rmc.core.support;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.triple_match.mvc.controller
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
	/// Loose MVC: This is a Controller.
	/// 
	/// TYPICAL ROLES
	/// 		1. (M)odel - Store Data, State
	/// 		2. (V)iew - Handle Input, Visuals, Sounds
	/// 		3. (C)ontroller - Process Input / Coordinate interactions
	/// 
	/// </summary>
	public class Controller: SingletonMonobehavior<Controller>
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------

		//	SETTER/GETTER
		
		// 	PUBLIC
		
		
		// 	PRIVATE
		/// <summary>
		/// MVC Reference
		/// </summary>
		private Model _model;
			
		
		//--------------------------------------
		//  Constructor / Creation
		//--------------------------------------	
		
			
		/// <summary>
		/// Initialize the specified model.
		/// </summary>
		/// <param name="model">Model.</param>
		public void Initialize (Model model)
		{
			_model = model;
		}

		//--------------------------------------
		//  Unity Methods
		//--------------------------------------
		
		//--------------------------------------
		//  Methods
		//--------------------------------------
		
		
		// 	PUBLIC
			
		/// <summary>
		/// Resets the game.
		/// </summary>
		public void GameReset ()
		{
			
			_model.GameReset();
		}

		/// <summary>
		/// Checks for matches.
		/// </summary>
		public void CheckForMatches ()
		{
			_model.CheckForMatches();
		}


		/// <summary>
		/// Adds the gems to fill gaps.
		/// </summary>
		public void DoFillGapsInGems ()
		{
			_model.DoFillGapsInGems();

		}


		/// <summary>
		/// 
		/// NOTE: We coordinate score sending through the view to faciliate timing issues.
		/// 	  On a larger scale project, its recommended to determine/evaluate/update score within the model
		/// 
		/// </summary>
		/// <param name="amountScoreToAdd_int">Amount score to add_int.</param>
		/// <param name="delayUntilSet_float">Delay until set_float.</param>
		public void AddToScore (int amountScoreToAdd_int, float delayUntilSet_float)
		{
			_model.AddToScore (amountScoreToAdd_int, delayUntilSet_float);
		}


		/// <summary>
		/// Sets the is input enabled to false.
		/// 
		/// NOTE: Its parameterless to work with the (current) limits of the very young CoroutineManager.
		/// 
		/// </summary>
		public void SetIsInputEnabledToFalse()
		{
			_model.IsInputEnabled = false;
		}

		
		/// <summary>
		/// Sets the is input enabled to false.
		/// 
		/// NOTE: Its parameterless to work with the (current) limits of the very young CoroutineManager
		/// 
		/// </summary>
		public void SetIsInputEnabledToTrue()
		{
			_model.IsInputEnabled = true;
		}
	
		
		/// <summary>
		/// Selecteds the gem
		/// </summary>
		public GemVO SelectedGemVO 
		{
			set
			{
				_model.SelectedGemVO = value;
			}
		}
		
		
		// 	PRIVATE
		
		
		//--------------------------------------
		//  Events
		//--------------------------------------




	}
}

