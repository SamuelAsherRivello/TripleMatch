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
using System.Collections.Generic;


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
	/// GOAL: Allow for easy sound setup and fucntionality (play, stop, volume)
	/// 
	/// STEPS TO ADD NEW SOUND
	/// 		1. Add sound file to project window within 'Resources' folder
	/// 		2. Call PlayAudioResourcePath("path/to/file/within/resources/folder/without/file/extension")
	/// 		3. Done!
	/// 
	/// </summary>
	public class AudioManager : SingletonMonobehavior<AudioManager> 
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// 	GETTER / SETTER
		
		// 	PUBLIC
		
		// 	PUBLIC STATIC


		// 	PRIVATE
		/// <summary>
		/// The _audio sources_dictionary.
		/// </summary>
		private Dictionary<string, AudioSource> _audioSources_dictionary;


		// 	PRIVATE STATIC
		
		//--------------------------------------
		//  Constructor / Creation
		//--------------------------------------	
		


		//--------------------------------------
		//  Unity Methods
		//--------------------------------------

		///<summary>
		///	Use this for initialization
		///</summary>
		void Start () 
		{

		}


		
		
		///<summary>
		///	Called once per frame
		///</summary>
		void Update () 
		{
			
		}


		
		
		//--------------------------------------
		//  Methods
		//--------------------------------------


		/// <summary>
		/// Plaies the sound.
		/// </summary>
		/// <returns>The sound.</returns>
		/// <param name="audioResourcePath_string">Audio resource path_string.</param>
		public AudioSource PlayAudioResourcePath (string audioResourcePath_string, float volumeScale_float = 1 )
		{

			AudioSource audioSource = GetAudioSourceByResourcePath (audioResourcePath_string);
			//Debug.Log ("playing : " + audioSource.clip);
			audioSource.PlayOneShot (audioSource.clip, volumeScale_float);
			return audioSource;
		}

		/// <summary>
		/// Stops the audio resource path.
		/// </summary>
		/// <returns>The audio resource path.</returns>
		/// <param name="pATH_GAME_RESET_AUDIO">P AT h_ GAM e_ RESE t_ AUDI.</param>
		public AudioSource StopAudioResourcePath (string audioResourcePath_string)
		{
			AudioSource audioSource = GetAudioSourceByResourcePath (audioResourcePath_string);
			audioSource.Stop();
			return audioSource;
		}

		/// <summary>
		/// Gets the audio source by resource path.
		/// </summary>
		/// <returns>The audio source by resource path.</returns>
		/// <param name="audioResourcePath_string">Audio resource path_string.</param>
		public AudioSource GetAudioSourceByResourcePath (string audioResourcePath_string)
		{

			AudioSource audioSource = new AudioSource ();

			//	CREATE AND POPULATE A DICTIONARY OF AUDIO SOURCES (EACH CONTAINING ONE CLIP)
			//	ONLY POPULATES EACH INDEX ONE TIME (OPTIMIZATION)
			if (_audioSources_dictionary == null)
			{
				_audioSources_dictionary = new Dictionary<string, AudioSource>();
			}
			if (!_audioSources_dictionary.ContainsKey (audioResourcePath_string))
			{
				_audioSources_dictionary[audioResourcePath_string] = gameObject.AddComponent<AudioSource>();
				_audioSources_dictionary[audioResourcePath_string].clip = Resources.Load (audioResourcePath_string) as AudioClip;
			}

			//
			audioSource = _audioSources_dictionary[audioResourcePath_string];

			if (audioSource == null) {
				throw new Exception ("AudioClip '"+audioResourcePath_string+"' Cannot Be Found. Choose new path name.");
			}

			return audioSource;
		}


		// 	PUBLIC
		//--------------------------------------
		//  Events
		//--------------------------------------
	}
}
