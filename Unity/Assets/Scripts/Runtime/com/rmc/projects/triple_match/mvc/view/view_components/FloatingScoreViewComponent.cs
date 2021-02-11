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
using UnityEngine.UI;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.triple_match.mvc.view.view_components
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
	/// This prefab rises above just-matched gems and shows '100 points' or such messaging.
	/// </summary>
	public class FloatingScoreViewComponent : MonoBehaviour
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER
		
		// 	PUBLIC
		
		
		[SerializeField]
		private Text _text;
		
		// 	PRIVATE
		
		
		//--------------------------------------
		// 	Constructor / Creation
		//--------------------------------------	

		/// <summary>
		/// Initialize the specified score, initialPosition_vector3 and targetPosition_vector3.
		/// </summary>
		public void Initialize (int score_int, Vector3 initialPosition_vector3)
		{
			//
			_text.text = string.Format (TripleMatchConstants.TEXT_POINTS_TOKEN, score_int);
			
			//	CONVERT: 3D V3 to 2D V3
			Vector3 initialPositionPixels_vector3 = Camera.main.WorldToScreenPoint(initialPosition_vector3);

			transform.position = initialPositionPixels_vector3; 

			//
			Vector3 targetPositionPixels_vector3 = new Vector3 
				(
					initialPositionPixels_vector3.x, 
					initialPositionPixels_vector3.y + TripleMatchConstants.POSITION_Y_OFFSET_FLOATING_SCORE_PIXELS, 
					initialPositionPixels_vector3.z
				);
			TweenToNewPosition(targetPositionPixels_vector3);
		}
		
		
		
		//--------------------------------------
		// 	Unity Methods
		//--------------------------------------
		
		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		protected void OnDestroy () 
		{
			
		}
		
		
		//--------------------------------------
		// 	Methods
		//--------------------------------------
		
		
		// 	PUBLIC
		
		
		/// <summary>
		/// Tweens to new position.
		/// </summary>
		public void TweenToNewPosition (Vector3 targetPosition_vector3)
		{
			
			iTween.MoveTo(
				gameObject,
				iTween.Hash
				(
				iT.MoveTo.x, 		targetPosition_vector3.x,
				iT.MoveTo.y,		targetPosition_vector3.y,
				iT.MoveTo.easetype, iTween.EaseType.easeOutQuad,
				iT.MoveTo.time,		TripleMatchConstants.DURATION_FLOATING_SCORE_EXIT,
				iT.MoveTo.islocal,	 false,
				iT.MoveTo.oncomplete, "_OnTweenToNewPositionCompleted",
				iT.MoveTo.oncompletetarget, gameObject
				)
				);
			
		}
		
		
		//	PRIVATE
		
		
		//--------------------------------------
		// 	Event Handlers
		//--------------------------------------

		/// <summary>
		/// _s the on tween to new position completed.
		/// </summary>
		private void _OnTweenToNewPositionCompleted ()
		{
			Destroy (gameObject);


		}
		
	}
}