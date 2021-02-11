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
using com.rmc.projects.triple_match.mvc.model.data.vo;
using UnityEngine;
using com.rmc.core.exceptions;
using com.rmc.core.grid_system;
using System.Collections.Generic;

//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.triple_match
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
	public class TripleMatchConstants 
	{ 

		//--------------------------------------
		//  Main Settings
		//--------------------------------------
		//
		public static Frequency FREQUENCY_OF_INSTANT_MATCHES_UPON_RESET = Frequency.Sometimes; 

		public const int MAX_ROWS = 8; //For Production, 8
		public const int MAX_COLUMNS = 8; //For Production, 8
		public const int MAX_GEM_TYPE_INDEX = 5; //For Production, 5
		public const int MIN_GEMS_PER_VERTICAL_AXIS_MATCH_REWARD = 3; //For Production, 3, axis-specific for easy debugging
		public const int MIN_GEMS_PER_HORIZONTAL_AXIS_MATCH_REWARD = 3; //For Production, 3, axis-specific for easy debugging

		
		//--------------------------------------
		//  Difficulty
		//--------------------------------------
		public static int SCORE_POINTS_PER_GEM = 5;
		

		//--------------------------------------
		//  Timing - Clock
		//--------------------------------------
		public static int DURATION_TIME_TOTAL_IN_ROUND  = 10;	//For Production: Use 60 SECONDS

		//TIME TICKING - I WAS USING 1 AND 1 FOR THESE, THEN I CREATED THE FUSE ANIMATION THAT NEEDS 
		//MUCH FASTER TIME 'TICKING' TO HAPPEN. Optional: Run this on fixedUpdate instead of ticking manually in a Coroutine.
		public static float DURATION_TIME_TICK = 0.125f; //easily accelerate time for debugging
		public static float TIME_DECREMENT_PER_TICK = .125f;//easily accelerate time for debugging


		//--------------------------------------
		//  Timing - Others
		//--------------------------------------
		public static float DURATION_DELAY_AFTER_MATCH_FOUND_BEFORE_INPUT_ENABLED = 2f;
		public static float DURATION_SCORE_NUMBER_CHANGES_OVER_TIME_TO_TARGET_VALUE = .25f; //lower = 'tweens' faster
		public static float DURATION_DELAY_TO_START_GAME = 0.25f;
		public static float DURATION_FLOATING_SCORE_EXIT = 0.75f;
		//
		//
		//***********
		private static float _DURATION_ANIMATION_MULTIPLIER = 0.75f; //Easy! Increase this and the overall 'gem speed' is increased.
		//***********
		public static float DURATION_DELAY_BEFORE_CHECK_FOR_MATCHES = 0.125f * _DURATION_ANIMATION_MULTIPLIER;
		public static float DURATION_DELAY_BEFORE_FILL_GAPS_IN_GEMS = 0.125f * _DURATION_ANIMATION_MULTIPLIER;
		public static float DURATION_GEM_TWEEN_SWAP = 0.5f * _DURATION_ANIMATION_MULTIPLIER;
		public static float DURATION_GEM_TWEEN_ENTRY = 0.5f * _DURATION_ANIMATION_MULTIPLIER;
		public static float DURATION_GEM_TWEEN_EXIT = 0.5f * _DURATION_ANIMATION_MULTIPLIER;




		//--------------------------------------
		//  Layers
		//--------------------------------------
		public static string SORTING_LAYER_PARTICLE_EFFECTS = "ParticleEffectsSortingLayer";

		//--------------------------------------
		//  Text / Localization
		//--------------------------------------
		public static string TEXT_EMPTY = ""; //for easy programmatic search, when debugging
		public static string TEXT_TITLE 						= "Triple Match";
		public static string TEXT_GAME_RESET_TEXT 				= "Reset Game";
		public static string TEXT_SCORE_TOKEN 					= "Score: {0}";
		public static string TEXT_TIME_TOKEN 					= "Time: {0}";
		public static string TEXT_POINTS_TOKEN 					= "{0} Pts";
		public static string TEXT_INSTRUCTIONS_INPUT_ENABLED	= "Click 2 adjacent gems to swap. Enjoy!";
		public static string TEXT_INSTRUCTIONS_INPUT_DISABLED	= "Rewarding Points! Please Wait...";
		public static string TEXT_INSTRUCTIONS_GAME_OVER		= "Best score: {0}! Click '"+ 
																  TripleMatchConstants.TEXT_GAME_RESET_TEXT+"!'";


		//--------------------------------------
		//  Prefabs
		//--------------------------------------
		public static string PATH_GEM_VIEW_PREFAB 				= "Prefabs/GemViewPrefab";
		public static string PATH_GEM_EXPLOSION_PREFAB 			= "Prefabs/GemExplosionPrefab";
		public static string PATH_FLOATING_SCORE_VIEW_PREFAB 	= "Prefabs/FloatingScoreViewPrefab";

		
		//--------------------------------------
		//  Graphics
		//--------------------------------------
		public static float ROW_SIZE = 0.4f;
		public static float COLUMN_SIZE = 0.4f;
		public static float POSITION_Y_OFFSET_FLOATING_SCORE_PIXELS = 60;


		//--------------------------------------
		//  Audio
		//--------------------------------------
		public static string PATH_BACKGROUND_MUSIC_AUDIO 			= "Audio/Music/BackgroundMusic01";
		//
		public static string PATH_BUTTON_CLICK_AUDIO 				= "Audio/SoundEffects/ButtonClick01";
		public static string PATH_GEM_CLICK_FAIL_AUDIO 				= "Audio/SoundEffects/ButtonClickFail01";
		public static string PATH_GAME_RESET_AUDIO 					= "Audio/SoundEffects/GameStart01";
		public static string PATH_SCORE_INCREASE_AUDIO 				= "Audio/SoundEffects/ScoreIncrease01";
		public static string PATH_GEM_SWAP_AUDIO 					= "Audio/SoundEffects/GemSwap01";
		public static string PATH_GEM_EXPLOSION_AUDIO 				= "Audio/SoundEffects/GemExit01";
		public static string PATH_GEM_DROP_IN_AUDIO 				= "Audio/SoundEffects/GemDropIn01";
		public static string PATH_DYNAMITE_EXPLOSION_AUDIO 			= "Audio/SoundEffects/DynamiteExplosion01";
		public static string PATH_TIME_LEFT_IN_ROUND_EXPIRED_AUDIO 	= "Audio/SoundEffects/TimeLeftInRoundExpired01";
		//
		public static float VOLUME_SCALE_SFX_1 = 0.25f; //louder
		public static float VOLUME_SCALE_SFX_2 = 0.05f; //quieter
		public static float VOLUME_SCALE_MUSIC = 0.10f; 






		//--------------------------------------
		//  Methods
		//--------------------------------------


		//	PUBLIC STATIC

		/// <summary>
		/// Gets the length of the score reward for match of.
		/// </summary>
		public static int GetScoreRewardForMatchOfLength (int gemCount_int)
		{
			//GIVES REWARDS OF 150, 400, 750, ...
			int scoreRewardForMatchOfLength_int = TripleMatchConstants.SCORE_POINTS_PER_GEM * gemCount_int * (gemCount_int - 2);
			return scoreRewardForMatchOfLength_int;
		}

		/// <summary>
		/// Gets the center point vector3 from game objects.
		/// </summary>
		/// <returns>The center point vector3 from game objects.</returns>
		/// <param name="gameObjectList">Game object list.</param>
		public static Vector3 GetCenterPointVector3FromGameObjectsList (List<GameObject> gameObjectList)
		{
			Vector3 centerPoint_vector3 = new Vector3 (0,0, 0);
			foreach (GameObject gameObject in gameObjectList)
			{
				centerPoint_vector3 += gameObject.transform.position;
			}
			centerPoint_vector3 /= gameObjectList.Count;
			return centerPoint_vector3;
		}

		/// <summary>
		/// Gets the gem tween entry delay.
		/// </summary>
		public static float GetGemTweenEntryDelay (GemVO gemVO)
		{
			return (TripleMatchConstants.MAX_ROWS - gemVO.RowIndex) * 0.1f;
		}

		/// <summary>
		/// Gets the gem exit physics force.
		/// </summary>
		/// <returns>The gem exit physics force.</returns>
		public static Vector2 GetGemExitPhysicsForce ()
		{
			int sign_int;
			if (Random.Range (0, 2) == 1)
			{
				sign_int = 1;
			}
			else
			{
				sign_int = -1;
			}
			
			return new Vector2 (sign_int* 30, 70);
		}

		/// <summary>
		/// Gets the gem color by gem V. Since our list of gems is finite and permanent, manually listing the colors is fast/fun.
		/// 
		/// Option: Alternative, Pass GemViewComponent instance instead and take a sample of its pixel colors dynamically.
		/// 
		/// </summary>
		/// <returns>The gem color by gem V.</returns>
		/// <param name="_gemVO">_gem V.</param>
		public static Color GetGemColorByGemVO (GemVO _gemVO)
		{
			Color colorByGemVO = new Color(0,0,0); //create non-null default

			if (_gemVO != null)
			{
				switch (_gemVO.GemTypeIndex)
				{
				case 0:
					//	blue
					colorByGemVO = Color.blue;
					break;
				case 1:
					//	green
					colorByGemVO = Color.green;
					break;
				case 2:
					//	purple
					colorByGemVO = Color.magenta;
					break;
				case 3:
					//	red
					colorByGemVO = Color.red;
					break;
				case 4:
					//	yellow
					colorByGemVO = Color.yellow;
					break;
				default:
					#pragma warning disable 0162
					throw new SwitchStatementException ();
					break;
					#pragma warning restore 0162


				}
			}

			return colorByGemVO;

		}

		/// <summary>
		/// Initializes the particle system for unity4.6.x.
		/// 	FOR (UNITY'S NATIVE) SHURIKEN PARTICLE EFFECTS ON TOP OF UNITY 4.6.X 2D, WE MUST ADJUST SORTING
		/// </summary>
		/// <param name="particlesystem">Particlesystem.</param>
		public static void InitializeParticleSystemForUnity46X (ParticleSystem particlesystem)
		{
			particlesystem.GetComponent<Renderer>().sortingLayerName = TripleMatchConstants.SORTING_LAYER_PARTICLE_EFFECTS;
			particlesystem.GetComponent<Renderer>().sortingOrder = 1;
		}
	}

}
