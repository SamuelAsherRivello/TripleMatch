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
using com.rmc.projects.triple_match.mvc.model;
using com.rmc.projects.triple_match.mvc.controller;
using com.rmc.projects.triple_match.mvc.view.view_components;
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
	/// 		1. Accept user input
	/// 		2. Render text information (score, time, etc...)
	/// 
	/// </summary>
	public class HUDView : AbstractView 
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER

		// 	PUBLIC

		// 	PRIVATE

		[SerializeField]
		private GameObject _floatingScoreParent;

		[SerializeField]
		private Text _titleText;

		[SerializeField]
		private Text _timeText;

		[SerializeField]
		private Text _scoreText;

		[SerializeField]
		private Text _instructionsText;

		[SerializeField]
		private Button _gameResetButton;

		private Text _gameResetText;

		private int _currentScore_int = 0;

		
		
		//--------------------------------------
		// 	Constructor / Creation
		//--------------------------------------	

		/// <summary>
		/// Initialize the specified model and controller.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="controller">Controller.</param>
		override public void Initialize (Model model, Controller controller)
		{
			base.Initialize (model, controller);

			_RenderInstructionsText (TripleMatchConstants.TEXT_EMPTY);
			
			_model.OnGameResetted += _OnGameResetted;
			_model.OnScoreChanged += _OnScoreChanged;
			_model.OnIsInputEnabledChanged += _OnIsInputEnabledChanged;
			_model.OnTimeLeftInRoundChanged += _OnTimeLeftInRoundChanged;
			_model.OnTimeLeftInRoundExpired += _OnTimeLeftInRoundExpired;
		}
		
		//--------------------------------------
		// 	Unity Methods
		//--------------------------------------



		override protected void Start () 
		{
			if (_gameResetButton != null)
			{
				_gameResetText = _gameResetButton.GetComponentInChildren<Text>();
			}
		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		override protected void OnDestroy () 
		{
			_model.OnGameResetted -= _OnGameResetted;
			_model.OnIsInputEnabledChanged -= _OnIsInputEnabledChanged;
			_model.OnScoreChanged -= _OnScoreChanged;
			_model.OnTimeLeftInRoundChanged -= _OnTimeLeftInRoundChanged;
			_model.OnTimeLeftInRoundExpired -= _OnTimeLeftInRoundExpired;
		}


		
		//--------------------------------------
		// 	Methods
		//--------------------------------------
		
		
		// 	PUBLIC

		/// <summary>
		/// Rewards the one match.
		/// </summary>
		public void RewardOneMatch (int amountScoreToAdd_int, Vector3 initialPosition_vector3)
		{

			GameObject floatingScoreViewPrefab = Instantiate (Resources.Load (TripleMatchConstants.PATH_FLOATING_SCORE_VIEW_PREFAB)) as GameObject;;
			floatingScoreViewPrefab.gameObject.transform.SetParent (_floatingScoreParent.transform);
			FloatingScoreViewComponent floatingScoreView = floatingScoreViewPrefab.GetComponent<FloatingScoreViewComponent>();
			floatingScoreView.Initialize (amountScoreToAdd_int, initialPosition_vector3);

			//	NOTE: WE ASSIGN SCORE HERE (FROM VIEW) TO ASSIST WITH TIMING ISSUES...
			_controller.AddToScore (amountScoreToAdd_int, TripleMatchConstants.DURATION_FLOATING_SCORE_EXIT);

		}

		//	PRIVATE


		/// <summary>
		/// _renders the title text.
		/// </summary>
		private void _RenderTitleText (string text_string)
		{
			_titleText.text = text_string;
		}

		/// <summary>
		/// _renders the time text.
		/// </summary>
		private void _RenderTimeText (float time_float)
		{
			_timeText.text = string.Format (TripleMatchConstants.TEXT_TIME_TOKEN, Mathf.CeilToInt(time_float));
		}

		/// <summary>
		/// _renders the title text.
		/// </summary>
		private void _RenderScoreTextFromCurrentScore ()
		{
			_scoreText.text = string.Format (TripleMatchConstants.TEXT_SCORE_TOKEN, _currentScore_int); 
		}
		
		/// <summary>
		/// _renders the title text.
		/// </summary>
		private void _RenderGameResetText (string text_string)
		{
			_gameResetText.text = text_string; 
		}


		/// <summary>
		/// _renders the instructions text.
		/// </summary>
		private void _RenderInstructionsText (string text_string)
		{
			_instructionsText.text = text_string; 
		}



		//	PRIVATE
		/// <summary>
		/// _s the state of the render instructions text depending on.
		/// </summary>
		private void _RenderInstructionsTextDependingOnState ()
		{
			
			//	WHILE PLAYING WE TOGGLE BETWEEN A FEW PHRASES
			if (_model.GameState == GameState.GAME_OVER)
			{
				
				//
				string gameOver_string = string.Format (TripleMatchConstants.TEXT_INSTRUCTIONS_GAME_OVER, _model.GetHighestScoreEverThisSession());
				_RenderInstructionsText (gameOver_string);
			}
			else
			{
				if (_model.IsInputEnabled)
				{
					_RenderInstructionsText (TripleMatchConstants.TEXT_INSTRUCTIONS_INPUT_ENABLED);
				}
				else
				{
					_RenderInstructionsText (TripleMatchConstants.TEXT_INSTRUCTIONS_INPUT_DISABLED);
				}
			}

		}

		/// <summary>
		/// _s the state of the update game button enabled depending on.
		/// </summary>
		private void _UpdateGameButtonEnabledDependingOnState ()
		{
			_gameResetButton.interactable = _model.IsInputEnabled || _model.GameState == GameState.GAME_OVER;
		}

		
		
		//--------------------------------------
		// 	Event Handlers
		//--------------------------------------
		/// <summary>
		/// Raises the game reset button clicked event.
		/// 
		/// NOTE: Must be public for Unity 4.6.x UI Event System
		/// 
		/// </summary>
		public void OnGameResetButtonClicked()
		{

			//This Animator State Machine will call OnCoverFadeInComplete()
			gameObject.GetComponent<Animator>().SetTrigger ("StartGameTrigger");

			
			if (AudioManager.IsInstantiated())
			{
				AudioManager.Instance.PlayAudioResourcePath (TripleMatchConstants.PATH_BUTTON_CLICK_AUDIO, TripleMatchConstants.VOLUME_SCALE_SFX_1);
			}



		}


		
		/// <summary>
		/// Raises the cover fade in complete event.
		/// 
		/// NOTE: Must be public for Unity 4.6.x State Machine Events
		/// 
		/// </summary>
		public void OnCoverFadeInComplete()
		{
			
			//Debug.Log ("HUD: Resetted");
			_controller.GameReset();
			

			
		}


		
		/// <summary>
		/// _ons the game resetted.
		/// </summary>
		private void _OnGameResetted ()
		{

			//	RENDER DISPLAY TEXT
			//		OPTIONAL: REPLACE STATIC CONST WITH STATIC METHOD TO ADD LOCALIZATION BY SPOKEN LANGUAGE
			_RenderGameResetText (TripleMatchConstants.TEXT_GAME_RESET_TEXT);
			_RenderTitleText (TripleMatchConstants.TEXT_TITLE );
			_OnScoreChanged (0);
			_RenderScoreTextFromCurrentScore ();
			_RenderInstructionsText (TripleMatchConstants.TEXT_INSTRUCTIONS_INPUT_ENABLED);
			
			//	HIDE GLOW ON RESTART BUTTON 
			gameObject.GetComponent<Animator>().SetBool ("IsGameOver", false);

		}



		/// <summary>
		/// _ons the game resetted.
		/// </summary>
		private void _OnIsInputEnabledChanged (bool isInputEnabled)
		{
			_RenderInstructionsTextDependingOnState();

			_UpdateGameButtonEnabledDependingOnState();


			
		}

	


		/// <summary>
		/// _s the on time left in round changed.
		/// </summary>
		/// <param name="timeLeft_int">Time left_int.</param>
		private void _OnTimeLeftInRoundChanged (float timeLeft_float,  float timeTotalInRound_float)
		{
			_RenderTimeText (timeLeft_float);	
		}



		/// <summary>
		/// _s the on time left in round expired.
		/// </summary>
		private void _OnTimeLeftInRoundExpired ()
		{
			//	SOUND
			if (AudioManager.IsInstantiated())
			{
				AudioManager.Instance.PlayAudioResourcePath (TripleMatchConstants.PATH_TIME_LEFT_IN_ROUND_EXPIRED_AUDIO, TripleMatchConstants.VOLUME_SCALE_SFX_1);
			}


			//	ORDER MATTERS
			//	#1
			_UpdateGameButtonEnabledDependingOnState();
			_RenderInstructionsTextDependingOnState();

			//	#2
			//	SHOW GLOW ON RESTART BUTTON TO ATTRACT USER ATTENTION
			gameObject.GetComponent<Animator>().SetBool ("IsGameOver", true);


		}

		/// <summary>
		/// When the model's score changes, we capture the new target value 
		/// and slowly over x seconds we tween the display text to that value
		/// </summary>
		private void _OnScoreChanged (int targetScore_int)
		{

			if (targetScore_int > 0)
			{
				iTween.ValueTo
					(gameObject, 
					 iTween.Hash
					 	(
						iT.ValueTo.from, _currentScore_int,
						iT.ValueTo.to, targetScore_int,
						iT.ValueTo.delay, 0,
						iT.ValueTo.time, TripleMatchConstants.DURATION_SCORE_NUMBER_CHANGES_OVER_TIME_TO_TARGET_VALUE,
						iT.ValueTo.easetype, iTween.EaseType.linear,
						iT.ValueTo.onupdatetarget, gameObject,
						iT.ValueTo.onupdate, "_OnScoreChangedUpdated"
						)
					 );
				}
			else
			{
				_OnScoreChangedUpdated (0); //don't tween. Change instantly to 0
			}

		}


		/// <summary>
		/// Every 'tick' of the tween, we update the display text to the new current value
		/// </summary>
		/// <param name="newScore_int">New score_int.</param>
		private void _OnScoreChangedUpdated (int newScore_int)
		{
			_currentScore_int = newScore_int;
			_RenderScoreTextFromCurrentScore ();	
			_RenderInstructionsTextDependingOnState();

		}



	}
}
