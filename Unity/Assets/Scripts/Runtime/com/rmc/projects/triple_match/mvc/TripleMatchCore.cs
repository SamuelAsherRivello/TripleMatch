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
using System.Collections.Generic;
using com.rmc.projects.triple_match.mvc.model;
using com.rmc.projects.triple_match.mvc.controller;
using com.rmc.projects.triple_match.mvc.view;
using com.rmc.core.support;
using com.rmc.core.managers;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.triple_match.mvc
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
	public class TripleMatchCore : SingletonMonobehavior<TripleMatchCore>  
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER
		
		/// <summary>
		/// All views for the game
		/// </summary>
		[SerializeField]
		private List<AbstractView> _abstractViews;

		
		// 	PUBLIC
		
		
		// 	PRIVATE
		
		
		//--------------------------------------
		// 	Constructor / Creation
		//--------------------------------------	
		
		
		//--------------------------------------
		// 	Unity Methods
		//--------------------------------------
		
		
		/// <summary>
		/// Start this instance.
		/// </summary>
		protected void Start () 
		{

			//	MANAGERS (order matters)
			//	#1
			AudioManager.OnInstantiateCompleted += _OnAudioManagerInstantiateCompleted;
			//	#2
			AudioManager.Instantiate();




			//	MODEL
			Model.Instantiate();



			//	CONTROLLER
			Controller.Instantiate();
			Controller.Instance.Initialize (Model.Instance);



			
			//	VIEW
			//	- WE SET THE VIEWS THROUGH THE INSPECTOR FOR MORE FLEXIBILITIES
			foreach (AbstractView abstractview in _abstractViews)
			{
				// NOTE: In a more mature MVC, the model could...
				//			1. listen to model events
				//			2. get (but not set) values of the model
				//			3. use events instead of having a reference to Controller.
				//			** But this project represents a looser, easier, faster MVC-with-growth-potential approach.
				abstractview.Initialize (Model.Instance, Controller.Instance);
			}



		}

		
		///<summary>
		///	Called once per frame
		///</summary>
		protected void Update () 
		{
			
			
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
		/// _s the on audio manager instantiate completed.
		/// </summary>
		/// <param name="audioManager">Audio manager.</param>
		private void _OnAudioManagerInstantiateCompleted (AudioManager audioManager)
		{
			AudioManager.OnInstantiateCompleted -= _OnAudioManagerInstantiateCompleted;

			
			//	Mimic 'Game Reset' Click
			//	After short delay ...
			//		1. of 1 frame or more for View to be 'ready'
			//		2. and its also a delay for cosmetics
			CoroutineManager.Instance.WaitForSecondsToCall (Controller.Instance.GameReset, TripleMatchConstants.DURATION_DELAY_TO_START_GAME);


		}
	}
}

