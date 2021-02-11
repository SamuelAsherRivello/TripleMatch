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
using com.rmc.projects.triple_match.mvc.model.data.vo;
using System.Collections.Generic;
using System.Linq;
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
	/// 		2. Render gems with sound and graphics. 
	/// 		3. Handle animation and overal game timing.
	/// 
	/// </summary>
	public class EnvironmentView : AbstractView
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER



		// 	PUBLIC


		/// <summary>
		/// The _gems parent.
		/// </summary>
		[SerializeField]
		public GameObject _gemsParent;

		/// <summary>
		/// The _hud view.
		/// </summary>
		[SerializeField]
		public HUDView _hudView;
		
		/// <summary>
		/// The _gem views.
		/// </summary>
		private List<GemViewComponent> _gemViews;
		
		
		// 	PRIVATE
		
		
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
			
			_model.OnGameResetted += _OnGameResetted;
			_model.OnScoreChanged += _OnScoreChanged;
			_model.OnSelectedGemVOChanged += _OnSelectedGemVOChanged;
			_model.OnGemVOsMarkedForRewardAndRemovalChanged += _OnGemVOsMarkedForRewardAndRemovalChanged;
			_model.OnGemVOsMarkedForRemovalChanged += _OnGemVOsMarkedForRemovalChanged;
			_model.OnGemVOsMarkedForShiftingDownChanged += _OnGemVOsMarkedForShiftingDownChanged;
			_model.OnGemVOsAddedToFillGapsChanged += _OnGemVOsAddedToFillGapsChanged;
		}
		


		
		//--------------------------------------
		// 	Unity Methods
		//--------------------------------------

		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		override protected void OnDestroy () 
		{
			_model.OnGameResetted -= _OnGameResetted;
			_model.OnSelectedGemVOChanged -= _OnSelectedGemVOChanged;
			_model.OnGemVOsMarkedForRewardAndRemovalChanged -= _OnGemVOsMarkedForRewardAndRemovalChanged;
			_model.OnGemVOsMarkedForRemovalChanged -= _OnGemVOsMarkedForRemovalChanged;
			_model.OnGemVOsMarkedForShiftingDownChanged -= _OnGemVOsMarkedForShiftingDownChanged;
			_model.OnGemVOsAddedToFillGapsChanged -= _OnGemVOsAddedToFillGapsChanged;
			_model.OnScoreChanged -= _OnScoreChanged;

			foreach (GemViewComponent gemView in _gemViews)
			{
				gemView.OnClicked -= _OnGemViewClicked;
				
			}
		}
		
		
		//--------------------------------------
		// 	Methods
		//--------------------------------------
		
		
		// 	PUBLIC



		
		//	PRIVATE
		
		


		
		/// <summary>
		/// _renders the title text.
		/// </summary>
		private void _DoLayoutGems (GemVO[,] gemVOs)
		{
			
			//	CLEAR OUT GEMS
			if (_gemViews != null)
			{
				foreach (GemViewComponent gemView in _gemViews)
				{
					_DoDestroyAndRemoveGemView (gemView);
					
				}
			}
			
			_gemViews = new List<GemViewComponent>();
			
			
			//
			GemVO nextGemVO;
			for (int rowIndex_int = 0; rowIndex_int < gemVOs.GetLength(0); rowIndex_int += 1) 
			{
				for (int columnIndex_int = 0; columnIndex_int < gemVOs.GetLength(1); columnIndex_int += 1) 
				{
					nextGemVO = gemVOs[rowIndex_int,columnIndex_int];
					_DoCreateAndAddGemView (nextGemVO);

				}
			}

			
		}
		

		
		
		/// <summary>
		/// _s the get gem view for gem vo.
		/// </summary>
		private GemViewComponent _GetGemViewForGemVo (GemVO gemVO)
		{
			GemViewComponent gemViewFound = null;
			if (_gemViews != null)
			{
				foreach (GemViewComponent gemView in _gemViews)
				{
					if (gemView.GemVO == gemVO)
					{
						gemViewFound = gemView;
					}
				}
			}
			
			return gemViewFound;
			
		}
		
		
		/// <summary>
		/// _s the swap two gem V os.
		/// </summary>
		private void _AttemptSwapTwoGemVOs (GemVO gemVO1, GemVO gemVO2)
		{

			//	SOUND FOR SWAP #1
			if (AudioManager.IsInstantiated())
			{
				CoroutineManager.Instance.WaitForSecondsToCall (_AudioManagerPlayGemSwap, TripleMatchConstants.DURATION_GEM_TWEEN_SWAP/2);
			}


			//SWAP THE DATA MODEL (INSTANT)
			_model.DoInstantlySwapTwoGemVOs (gemVO1, gemVO2);

			//SWAP THE VISUALS (OVER X SECONDS)
			_GetGemViewForGemVo (gemVO1).TweenToNewPositionSwap(0);
			_GetGemViewForGemVo (gemVO2).TweenToNewPositionSwap(0);

			//NO MATCH, THEN SWAP BACK
			if (!_model.IsThereAMatchContainingEitherGemVO(gemVO1, gemVO2))
			{

				//local variables used for code-readability
				float delayToStartGemTween_float = TripleMatchConstants.DURATION_GEM_TWEEN_SWAP;
				float delayToStartGemSound_float = delayToStartGemTween_float * 1.5f;

				
				//	SOUND FOR SWAP #2
				if (AudioManager.IsInstantiated())
				{
					CoroutineManager.Instance.WaitForSecondsToCall (_AudioManagerPlayGemSwap, delayToStartGemSound_float);
				}


				//SWAP THE DATA MODEL (INSTANT)
				_model.DoInstantlySwapTwoGemVOs (gemVO1, gemVO2);
				
				//SWAP THE VISUALS (OVER X SECONDS)
				_GetGemViewForGemVo (gemVO1).TweenToNewPositionSwap(delayToStartGemTween_float);
				_GetGemViewForGemVo (gemVO2).TweenToNewPositionSwap(delayToStartGemTween_float);
			}
			else 
			{
				//	CHECK FOR MATCHES after a cosmetic delay
				CoroutineManager.Instance.WaitForSecondsToCall (_controller.CheckForMatches, TripleMatchConstants.DURATION_GEM_TWEEN_SWAP);
			
			}

		}

		/// <summary>
		/// Call a sound with a needed delay. Delay not otherwise supported by the very young 'AudioManager', yet.
		/// </summary>
		private void _AudioManagerPlayGemSwap ()
		{
			AudioManager.Instance.PlayAudioResourcePath (TripleMatchConstants.PATH_GEM_SWAP_AUDIO, TripleMatchConstants.VOLUME_SCALE_SFX_1);
		}

		


		/// <summary>
		/// Reward one match.
		/// </summary>
		private void _RewardOneMatchFromGemVOList (List<GemVO> gemVOs)
		{
			List<GemViewComponent> gemViews = new List<GemViewComponent>();

			//TODO: replace with linq for brevity
			GemViewComponent nextGemView;
			foreach (GemVO gemVO in gemVOs)
			{
				nextGemView = _GetGemViewForGemVo (gemVO);

				//	The null-check is because
				//	some match shapes like...
				//				M
				//			   MMM
				//				M
				//	...will destroy 3 horizontally, THEN just 2 vertically (or vice versa), 
				//		Instead of 3 then 3. This is ok.
				//
				if (nextGemView != null)
				{
					gemViews.Add (nextGemView);
				}
			}

			//	Find centerpoints from the GameObjects of the GemViewComponents.
			Vector3 centerPointOfMatch_vector3 = TripleMatchConstants.GetCenterPointVector3FromGameObjectsList 
					(
						gemViews.Select (gvc => gvc.gameObject).ToList<GameObject>()
					);


			//	CREATE AND REPARENT
			_hudView.RewardOneMatch 
				(
					//NOTE: its important to NOT use the gemView count since some are being destroy 
					//		in arbitrary order during this time-frame
					//		and their count may not be accurate. So we use gemVOs.count. Good!
					TripleMatchConstants.GetScoreRewardForMatchOfLength (gemVOs.Count), 
					centerPointOfMatch_vector3
				);

			foreach (GemViewComponent gemView in gemViews)
			{
				gemView.TweenToNewPositionExit();
				_gemViews.Remove (gemView);
			}

			if (AudioManager.IsInstantiated())
			{
				AudioManager.Instance.PlayAudioResourcePath (TripleMatchConstants.PATH_GEM_EXPLOSION_AUDIO);
			}

			
			_controller.SetIsInputEnabledToFalse();

			//local variable for readability
			bool willAllowConcurrentCalls_bool = false;
			CoroutineManager.Instance.WaitForSecondsToCall (_controller.SetIsInputEnabledToTrue, TripleMatchConstants.DURATION_DELAY_AFTER_MATCH_FOUND_BEFORE_INPUT_ENABLED, willAllowConcurrentCalls_bool);
	
		
		}

		
		/// <summary>
		/// Reward one match.
		/// </summary>
		private void _RemoveAllGemVOsInList (List<GemVO> gemVOs)
		{
			List<GemViewComponent> gemViews = new List<GemViewComponent>();
			
			//TODO: replace with linq for brevity
			foreach (GemVO gemVO in gemVOs)
			{
				gemViews.Add (_GetGemViewForGemVo (gemVO));
			}
			
			foreach (GemViewComponent gemView in gemViews)
			{
				gemView.TweenToNewPositionExit();
			}
			
		}

		/// <summary>
		/// _s the do create and add gem view.
		/// </summary>
		/// <param name="nextGemVO">Next gem V.</param>
		private void _DoCreateAndAddGemView (GemVO nextGemVO)
		{
			
			//	CREATE AND REPARENT

			GameObject nextGemViewPrefab = Instantiate (Resources.Load (TripleMatchConstants.PATH_GEM_VIEW_PREFAB)) as GameObject;
			nextGemViewPrefab.transform.parent = _gemsParent.transform;
			
			//	INITIALIZE WITH DATA VO
			GemViewComponent nextGemView = nextGemViewPrefab.GetComponent<GemViewComponent>();
			
			
			//
			Vector3 spawnPointForGemsVector3 = new Vector3 
				(
					TripleMatchConstants.COLUMN_SIZE * nextGemVO.ColumnIndex, 
					5, 
					transform.localPosition.z
					);
			//
			nextGemView.OnTweenToNewPositionEntryCompleted += _OnGemTweenToNewPositionEntryCompleted;
			nextGemView.OnClicked += _OnGemViewClicked;
			nextGemView.Initialize (nextGemVO, spawnPointForGemsVector3);
			
			_gemViews.Add (nextGemView);
		}

		/// <summary>
		/// _s the do destroy and remove gem view.
		/// </summary>
		/// <param name="gemView">Gem view.</param>
		private void _DoDestroyAndRemoveGemView (GemViewComponent gemView)
		{
			gemView.OnClicked -= _OnGemViewClicked;
			gemView.OnTweenToNewPositionEntryCompleted -= _OnGemTweenToNewPositionEntryCompleted;
			gemView.Destroy();
			_gemViews.Remove (gemView);
		}


		//--------------------------------------
		// 	Event Handlers
		//--------------------------------------

		/// <summary>
		/// _s the on tween to new position entry completed.
		/// </summary>
		/// <param name="gemView">Gem view.</param>
		private void _OnGemTweenToNewPositionEntryCompleted (GemViewComponent gemView)
		{
			gemView.OnTweenToNewPositionEntryCompleted -= _OnGemTweenToNewPositionEntryCompleted;

			//	ARE 100% IN PROPER TARGET POSITION?
			if (_gemViews.Where (nextGemView => nextGemView.IsAtTargetPosition()).Count() == _gemViews.Count)
			{

				CoroutineManager.Instance.WaitForSecondsToCall (_controller.CheckForMatches, TripleMatchConstants.DURATION_DELAY_BEFORE_CHECK_FOR_MATCHES);

			}
		}

		
		//


		/// <summary>
		/// _ons the game resetted.
		/// </summary>
		private void _OnGameResetted ()
		{
			
			//	RENDER
			_DoLayoutGems (_model.GemVOArray);

			//	SOUND
			if (AudioManager.IsInstantiated())
			{
				AudioManager.Instance.StopAudioResourcePath (TripleMatchConstants.PATH_GAME_RESET_AUDIO);
				AudioManager.Instance.PlayAudioResourcePath (TripleMatchConstants.PATH_GAME_RESET_AUDIO, TripleMatchConstants.VOLUME_SCALE_SFX_1);

				AudioManager.Instance.StopAudioResourcePath (TripleMatchConstants.PATH_BACKGROUND_MUSIC_AUDIO);
				AudioManager.Instance.PlayAudioResourcePath (TripleMatchConstants.PATH_BACKGROUND_MUSIC_AUDIO, TripleMatchConstants.VOLUME_SCALE_MUSIC);
			}

		}
		
		/// <summary>
		/// _s the on score changed.
		/// </summary>
		private void _OnScoreChanged (int score_int)
		{
			//	SOUND
			if (AudioManager.IsInstantiated() && score_int > 0)
			{
				AudioManager.Instance.PlayAudioResourcePath (TripleMatchConstants.PATH_SCORE_INCREASE_AUDIO, TripleMatchConstants.VOLUME_SCALE_SFX_2);
			}
		}
		
		/// <summary>
		/// _s the on gem clicked.
		/// </summary>
		/// <param name="gemView">Gem view.</param>
		private void _OnGemViewClicked (GemViewComponent gemView)
		{
			//
			if (_model.GameState == GameState.PLAYING && _model.IsInputEnabled)
			{

				if (_model.SelectedGemVO == null)
				{
					//	1. SELECT FIRST GEM IN A PAIR
					_controller.SelectedGemVO = gemView.GemVO;
				}
				else if (_model.SelectedGemVO == gemView.GemVO)
				{
					//	2. DESELECT FIRST GEM IN A PAIR
					_controller.SelectedGemVO = null;
					
				}
				else if (Model.AreGemVOsSwappable (_model.SelectedGemVO, gemView.GemVO))
				{
					//	3. SWAP FIRST & SECOND GEM IN A PAIR
					_AttemptSwapTwoGemVOs (_model.SelectedGemVO, gemView.GemVO);
					_controller.SelectedGemVO = null;

				}
				else 
				{
					//	4. DESELECTED ALL
					_controller.SelectedGemVO = null;
				}

			}
			else
			{

				if (AudioManager.IsInstantiated())
				{
					AudioManager.Instance.PlayAudioResourcePath (TripleMatchConstants.PATH_GEM_CLICK_FAIL_AUDIO, TripleMatchConstants.VOLUME_SCALE_SFX_2);
				}

			}
		}
		
		/// <summary>
		/// _s the on selected gem VO changed.
		/// </summary>
		/// <param name="gemVO">Gem V.</param>
		private void _OnSelectedGemVOChanged (GemVO gemVO)
		{
			if (_gemViews != null)
			{
				foreach (GemViewComponent gemView in _gemViews)
				{
					//	1. DESELECT ALL GEMS
					gemView.SetIsHighlighted (false);
				}
				
				if (gemVO != null && _GetGemViewForGemVo(gemVO) != null)
				{
					//	2. SELECT EXACTLY ONE GEM
					_GetGemViewForGemVo(gemVO).SetIsHighlighted (true);
				}
			}
		}
		
		
		/// <summary>
		/// _s the on gem V os marked for deletion changed.
		/// </summary>
		/// <param name="gemVOs">Gem V os.</param>
		private void _OnGemVOsMarkedForRewardAndRemovalChanged (List<List<GemVO>> gemVOListOfLists)
		{

			//	1. deselect all
			foreach (GemViewComponent gemView in _gemViews)
			{
				gemView.SetIsHighlighted (false);
			}

			//	2. select some
			foreach (List<GemVO> gemVOList in gemVOListOfLists)
			{
				//3. Inside here, also removes each from _gemViews
				_RewardOneMatchFromGemVOList (gemVOList);
			}


			//	3. IF THERE WAS 1 OR MORE REWARDS, THEN SHIFT GEMS FOR A FULL BOARD
			if (gemVOListOfLists.Count > 0)
			{
				CoroutineManager.Instance.WaitForSecondsToCall (_controller.DoFillGapsInGems, TripleMatchConstants.DURATION_DELAY_BEFORE_FILL_GAPS_IN_GEMS);

			}
			else
			{
				//NO MATCHES? ENABLE PLAY
				_model.IsInputEnabled = true;

			}
		}


		/// <summary>
		/// _s the on gem V os marked for deletion changed.
		/// </summary>
		/// <param name="gemVOs">Gem V os.</param>
		private void _OnGemVOsMarkedForRemovalChanged (List<GemVO> gemVOs)
		{
			GemViewComponent gemView;
			foreach (GemVO gemVO in gemVOs)
			{
				gemView = _GetGemViewForGemVo (gemVO);
				if (gemView != null)
				{
					_DoDestroyAndRemoveGemView (gemView);
				}
			}

		}



		/// <summary>
		/// _s the on gem V os marked for shifting down changed.
		/// </summary>
		/// <param name="gemVOs">Gem V os.</param>
		private void _OnGemVOsMarkedForShiftingDownChanged (List<GemVO> gemVOs)
		{
			
			//	2. select some
			float delayBeforeTweening_float;
			foreach (GemVO gemVO in gemVOs)
			{
				delayBeforeTweening_float = (TripleMatchConstants.MAX_ROWS - gemVO.RowIndex) * 0.1f;
				_GetGemViewForGemVo (gemVO).TweenToNewPositionDrop (delayBeforeTweening_float);
			}
		}

		
		/// <summary>
		/// Raises the gem V os added to fill gaps changed event.
		/// </summary>
		/// <param name="gemVOs">Gem V os.</param>
		private void _OnGemVOsAddedToFillGapsChanged (List<GemVO> gemVOs)
		{
			foreach (GemVO gemVO in gemVOs)
			{
				_DoCreateAndAddGemView (gemVO);
			}
		}






	}
}

