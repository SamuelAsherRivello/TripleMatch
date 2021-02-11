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
using com.rmc.projects.triple_match.mvc.model;
using com.rmc.projects.triple_match.mvc.controller;
using com.rmc.core.managers;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.triple_match.mvc.view
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
	/// Loose MVC: This is a View. See superclass for more info.
	/// 
	/// GOALS:
	/// 		1. Match the fuse spark animation to the Model's clock timer.
	/// 		2. Explode the dynamite when time is out
	/// 
	/// </summary>
	public class DynamiteFuseView : AbstractView
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER



		// 	PUBLIC

		// 	PRIVATE
		/// <summary>
		/// The _transforms.
		/// </summary>
		[SerializeField]
		private Transform[] _waypoint_transforms;

		/// <summary>
		/// The _spark_gameobject.
		/// </summary>
		[SerializeField]
		private GameObject _spark_gameobject;


		/// <summary>
		/// The _dynamite_gameobject.
		/// </summary>
		[SerializeField]
		private GameObject _dynamite_gameobject;


		/// <summary>
		/// The current path percent_float.
		/// </summary>
		private float _sparksPercentageThroughPath_float;

		
		//--------------------------------------
		// 	Constructor
		//--------------------------------------	

		/// <summary>
		/// Initialize the specified model and controller.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="controller">Controller.</param>
		override public void Initialize (Model model, Controller controller)
		{
			base.Initialize (model, controller);
			
			_model.OnTimeLeftInRoundChanged += _OnTimeLeftInRoundChanged;
			_model.OnTimeLeftInRoundExpired += _OnTimeLeftInRoundExpired;
			_model.OnGameResetted			+= _OnGameResetted;

			_sparksPercentageThroughPath_float = 0;

			ParticleSystem spark_particlesystem = _spark_gameobject.GetComponentInChildren<ParticleSystem>();
			TripleMatchConstants.InitializeParticleSystemForUnity46X (spark_particlesystem);
			//
			//DYNAMITE IS 2+ PREFABS, SO SET EACH UP AND PLAY THEM LATER
			foreach (ParticleSystem particleSystemInstance in _dynamite_gameobject.GetComponentsInChildren<ParticleSystem>())
			{
				TripleMatchConstants.InitializeParticleSystemForUnity46X (particleSystemInstance);
				particleSystemInstance.Stop();
			}


		}
		


		
		//--------------------------------------
		// 	Unity Methods
		//--------------------------------------
		
		///<summary>
		///	Use this for initialization
		///</summary>
		override protected void Start () 
		{
			
		}


		/// <summary>
		/// Update this instance.
		/// </summary>
		override protected void Update () 
		{
			iTween.PutOnPath(_spark_gameobject, _waypoint_transforms, _sparksPercentageThroughPath_float);	
		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		override protected void OnDestroy () 
		{

			_model.OnTimeLeftInRoundChanged -= _OnTimeLeftInRoundChanged;
			_model.OnTimeLeftInRoundExpired -= _OnTimeLeftInRoundExpired;
			_model.OnGameResetted			-= _OnGameResetted;

		}

		/// <summary>
		/// Raises the draw gizmos event.
		/// </summary>
		public void OnDrawGizmos()
		{
			//Visual. Not used in movement
			iTween.DrawPath(_waypoint_transforms);
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
		/// _s the on time left in round changed.
		/// </summary>
		/// <param name="timeLeft_int">Time left_int.</param>
		private void _OnTimeLeftInRoundChanged (float timeLeft_float, float timeTotal_float)
		{
			//Calculate % as... 0 (at round start) to 1 (at round complete) and keep result in proper range
			_sparksPercentageThroughPath_float = 1 - (float)timeLeft_float / (float)timeTotal_float;
			_sparksPercentageThroughPath_float = Mathf.Max (_sparksPercentageThroughPath_float, 0);
			_sparksPercentageThroughPath_float = Mathf.Min (_sparksPercentageThroughPath_float, 1);
			//Debug.Log ("OK: ("+timeLeft_int+"/"+timeTotal_int+")" + _sparksPercentageThroughPath_float);

		}
		
		
		
		/// <summary>
		/// _s the on time left in round expired.
		/// </summary>
		private void _OnTimeLeftInRoundExpired ()
		{
			//
			_spark_gameobject.gameObject.SetActive (false);

			//
			foreach (ParticleSystem particleSystemInstance in _dynamite_gameobject.GetComponentsInChildren<ParticleSystem>())
			{
				particleSystemInstance.Play();
			}

			
			if (AudioManager.IsInstantiated())
			{
				AudioManager.Instance.PlayAudioResourcePath (TripleMatchConstants.PATH_DYNAMITE_EXPLOSION_AUDIO);
			}

		}

		/// <summary>
		/// _s the on time left in round expired.
		/// </summary>
		private void _OnGameResetted ()
		{
			_spark_gameobject.gameObject.SetActive (true);
		}


	}
}

