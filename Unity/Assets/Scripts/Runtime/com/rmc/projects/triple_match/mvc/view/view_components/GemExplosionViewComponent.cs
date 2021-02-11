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
	/// This prefab is attached on just-matched gems. Its a puff of smoke and maybe some other particle effects. We'll see.
	/// </summary>
	public class GemExplosionViewComponent : MonoBehaviour
	{


		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// 	GETTER / SETTER
		
		// 	PUBLIC

		
		// 	PRIVATE

		/// <summary>
		/// Add one or more particle system prefabs. Each will be added upon start. EXPLODE!!!
		/// </summary>
		[SerializeField]
		private List<GameObject> _particleSystemPrefabs;


		//--------------------------------------
		// 	Constructor / Creation
		//--------------------------------------
		
		//--------------------------------------
		//	Unity Methods
		//--------------------------------------

		/// <summary>
		/// Start this instance.
		/// </summary>
		protected void Start () 
		{

			ParticleSystem _particleSystem;
			GameObject particleSystemPrefabInstance;
			int particleSystemPrefabIndex_int = 0;

			//	SETUP FOR 1 OR EVEN MORE PARTICLES TO WORK CONCURRENTLY...
			foreach (GameObject particleSystemPrefab in _particleSystemPrefabs)
			{
				particleSystemPrefabInstance = Instantiate (particleSystemPrefab) as GameObject;
				particleSystemPrefabInstance.transform.localPosition = new Vector3 (0,0,0); //Some CFX prefabs are not at 0,0,0. Correct that.
				particleSystemPrefabInstance.transform.SetParent (transform, false);
				
				//
				_particleSystem = particleSystemPrefabInstance.GetComponent<ParticleSystem>();
				
				TripleMatchConstants.InitializeParticleSystemForUnity46X (_particleSystem);
				particleSystemPrefabIndex_int++;
			}

		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		protected void OnDestroy () 
		{
			
		}
		
		
		//--------------------------------------
		// 	Methods
		//--------------------------------------
		
		
		//	PUBLIC
		
		
		//	PRIVATE
		
		
		//--------------------------------------
		//	Event Handlers
		//--------------------------------------
	}
}