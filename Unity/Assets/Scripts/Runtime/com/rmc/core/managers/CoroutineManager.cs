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
using com.rmc.core.support;
using System;
using System.Collections;


//--------------------------------------
//  Private Class
//--------------------------------------
/// <summary>
/// WaitForSecondsToCallArguments. Main purpose;
/// 
/// 	1. Pack together data payload into one instance. 
/// 
/// </summary>
class WaitForSecondsToCallArguments
{
	public Action Callback_action ;
	public float DelayBeforeCalling_float;
	
	public WaitForSecondsToCallArguments ( Action callback_action, float delayBeforeCalling_float)
	{
		Callback_action = callback_action ;
		DelayBeforeCalling_float = delayBeforeCalling_float;
	}
	
}


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.core.managers
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
	/// GOALS;
	/// 	1. Allow non-monobehavior classes to call coroutines easily
	/// 	2. Allow monobehavior classes to call methods after a delay (with less boilerplate code)
	/// 	
	/// WHAT'S GREAT?
	/// 		1. Centralize 'call-this-after-2-seconds' type routines here. Instead of throughout codebase.
	/// 		2. Quickly create delayed method calls.
	/// 		2. This freedom empower devs to tweak/polish the UX of animations/delays/loading/messaging easily.
	/// 
	/// WHAT'S NOT? 
	///			1. Doesn't accept parameters to pass along with the method-to-be-called after the delay. That's a TODO item.  
	///
	/// 
	/// </summary>
	public class CoroutineManager : SingletonMonobehavior<CoroutineManager> 
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// 	GETTER / SETTER
		
		// 	PUBLIC
		
		// 	PUBLIC STATIC
		
		// 	PRIVATE
		
		// 	PRIVATE STATIC
		
		//--------------------------------------
		//  Constructor / Creation
		//--------------------------------------	
		
		
		
		//--------------------------------------
		//  Unity Methods
		//--------------------------------------
		
		//--------------------------------------
		//  Methods
		//--------------------------------------
		/// <summary>
		/// Waits for seconds to call.
		/// </summary>
		/// <param name="callback">Callback.</param>
		/// <param name="delayBeforeCalling_float">Delay before calling_float.</param>
		public void WaitForSecondsToCall(Action callback_action, float delayBeforeCalling_float, bool willAllowConcurrentCalls_bool = true)
		{
			if (!willAllowConcurrentCalls_bool)
			{
				StopCoroutine ("_WaitForSecondsToCall");
			}

			StartCoroutine ("_WaitForSecondsToCall", new WaitForSecondsToCallArguments (callback_action, delayBeforeCalling_float));
		}

		/// <summary>
		/// _s the wait for seconds to call.
		/// </summary>
		/// <returns>The wait for seconds to call.</returns>
		/// <param name="parameters_array">Parameters_array.</param>
		private IEnumerator _WaitForSecondsToCall(WaitForSecondsToCallArguments waitForSecondsToCallArguments)
		{
			yield return new WaitForSeconds (waitForSecondsToCallArguments.DelayBeforeCalling_float);
			waitForSecondsToCallArguments.Callback_action();
		}
				
		
		// 	PUBLIC
		//--------------------------------------
		//  Events
		//--------------------------------------
	}
}
