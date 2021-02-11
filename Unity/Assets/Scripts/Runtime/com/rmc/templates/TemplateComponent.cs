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
	/// <summary>
	/// Template component.
	/// </summary>
	public class TemplateComponent : MonoBehaviour 
	{
		

		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER
		/// <summary>
		/// The _sample public text_string.
		/// </summary>
		private string _samplePublicText_string;
		public string SamplePublicText 
		{ 
			get 
			{ 
				//OPTIONAL: CONTROLL ACCESS TO PRIVATE VALUE
				return _samplePublicText_string; 
			}
			set 
			{ 
				//OPTIONAL: CONTROLL ACCESS TO PRIVATE VALUE
				_samplePublicText_string = value; 
			}
		}
			
		
		// 	PUBLIC
		
		
		// 	PRIVATE
		
		
		//--------------------------------------
		// 	Constructor / Creation
		//--------------------------------------	


		//--------------------------------------
		// 	Unity Methods
		//--------------------------------------
		
		///<summary>
		///	Use this for initialization
		///</summary>
		protected void Start () 
		{


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
	
		/// <summary>
		/// Samples the public method.
		/// </summary>
		/// <returns>The public method.</returns>
		/// <param name="message_string">Message_string.</param>
		public string SamplePublicMethod (string message_string) 
		{
			return message_string;
			
		}
		
		
		//	PRIVATE




		
		//--------------------------------------
		// 	Event Handlers
		//--------------------------------------
		/// <summary>
		/// Handles the Event
		/// </summary>
		/// <param name="message_string">Message_string.</param>
		public void _OnEventOccurred (string message_string) 
		{
			
		}
	}
}
