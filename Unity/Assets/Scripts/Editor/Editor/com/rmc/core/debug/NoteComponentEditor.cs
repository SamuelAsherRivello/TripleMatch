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
using UnityEditor;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.core.debug
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
	/// This class is the editor for the NoteComponent class. 
	/// The NoteComponent class simply allows the developer to put a note (for documentation) in the inspector of a GameObject.
	/// 
	/// NOTE: Is a simple, fun example of an editor script.
	/// 
	/// </summary>
	[CustomEditor(typeof(NoteComponent))]
	public class NoteComponentEditor : Editor 
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER
		
		// 	PUBLIC
		/// <summary>
		/// The _note serialized property.
		/// </summary>
		public SerializedProperty _noteSerializedProperty;
		
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

		/// <summary>
		/// Raises the enable event.
		/// </summary>
		protected void OnEnable () 
		{
			_noteSerializedProperty = serializedObject.FindProperty ("_note");
			
		}
		
		
		///<summary>
		///	Called once per frame
		///</summary>
		protected void Update () 
		{
			
			
		}

		/// <summary>
		/// Raises the inspector GUI event.
		/// </summary>
		public override void OnInspectorGUI() 
		{
			serializedObject.Update ();
			_noteSerializedProperty.stringValue = EditorGUILayout.TextArea( _noteSerializedProperty.stringValue, GUILayout.MaxHeight(75) );
			serializedObject.ApplyModifiedProperties ();
		}

		
		//--------------------------------------
		// 	Methods
		//--------------------------------------
		
		
		// 	PUBLIC
		
		
		//	PRIVATE
		
		
		//--------------------------------------
		// 	Event Handlers
		//--------------------------------------
	}
}
