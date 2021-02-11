/**
 • Copyright (C) 2005-2015 by Rivello Multimedia Consulting (RMC).                    
 • code [at] RivelloMultimediaConsulting [dot] com               
 */
// Marks the right margin of code ************************************************************************************

/*

//--------------------------------------------------------------------------------------------------------------------
//  I. OVERVIEW
//--------------------------------------------------------------------------------------------------------------------
• The 'Triple Match' game is complete. There are no warnings/errors at compiletime nor runtime. 
• Target resolution is 755 x 600.
• Playing? Entry point is the Scene "TripleMatchFullGame.unity"
• CodeReview? Entry point is the class ("com.rmc.projects.triple_match.mvc.TripleMatchCore.cs").


//--------------------------------------------------------------------------------------------------------------------
//  II. CODE OWNERSHIP
//--------------------------------------------------------------------------------------------------------------------
• Folder (/Assets/TripleMatch/) - All code is by Sam Rivello. Created from scratch for this project.
• Folder (/Assets/TripleMatch/) - All non-code (assets) were given by assigner or found by Sam Rivello
• Folder (/Assets/Standard Assets/) - All contents provided by Unity Technologies via Asset Store
• Folder (/Assets/Community Assets/) - All contents provided by 3rd Parties via Asset Store


//--------------------------------------------------------------------------------------------------------------------
//  III. FEATURES REQUIRED: By Assigners' Documentation
//--------------------------------------------------------------------------------------------------------------------
--
• 8x8 grid of objects - [Sam: Done!]
• The objects can swap place [Sam: Done!]
• If Swap makes no matches, reverse the swap. [Sam: Done!]
• Match is 3 or more horizontal or vertical. Remove matches & new objects fall from top [Sam: Done!]
--
• 1 minute long [Sam: Done!]
• 5 colours [Sam: Done!]
• Drag or click objects to swap [Sam: Done, via click.!]
• Use a game like Midas Miner for reference (www.king.com) [Sam: Done!]
--
• Submitting: Please send the test back as a .zip file or package to a Google doc link, [Sam: Done! I used Dropbox links.]
• Timeframe: 7 days, or request time via email. [Sam: Done! I'd requested 8 days (Dec 20 to 28, 2014) of part-time effort.]


//--------------------------------------------------------------------------------------------------------------------
//  IV. FEATURES NOT-REQUIRED: Added For Fun
//--------------------------------------------------------------------------------------------------------------------
• Added: MVC (Model-View-Controller) framework. I created it as a playful, custom architecture for prototyping :)
• Added: MVC Project Diagram in Folder (/Assets/TripleMatch/Documentation/). I call it "Loose MVC" :)
• Added: Particle effects 
• Added: Sound effects
• Added: Programmatic Animation (using 3rd party iTween)
• Added: Timeline Animation (using Native Animator/Animation)
• Added: Unity 4.3.x Sprites for all gameplay elements 
• Added: Unity 4.6.x 'New' Unity UI for all GUI elements 
• Added: Suspensful visual 'timer' of fuse, dynamite explosion 
• Added: Dynamic Instruction Text for better UX and to tease user's personal 'high score'
• Added: Ex. UnitTest. Open Unity->Menu->UnityTesting->UnitTestRunner! (See 'com.rmc.core.grid_system.GridSystemTest.cs')
• Added: Easy-to-edit text and gameplay/animation settings... (See 'com.rmc.projects.triple_match.TripleMatchConstants.cs')


//--------------------------------------------------------------------------------------------------------------------
//  V. TODO: Before Submission
//--------------------------------------------------------------------------------------------------------------------
• Nothing else.



//--------------------------------------------------------------------------------------------------------------------
//  VI. TODO: Theoretical Future-Features, These items have not been attempted nor completed.
//--------------------------------------------------------------------------------------------------------------------
• Architecture: I'd like to recode the game and try out uFrame Architecture (https://www.assetstore.unity3d.com/en/#!/content/14381)
• Production: Replace 100% of art. Just to revisit the views flexibility to handle different size, layouts, (# of gemtypes), etc...
• Optimization: Use 'pooling' to create between 64 and 128 Gem prefab instances upon game-start for reuse.
• Gameplay: Show a hint to user of what match is best to make next
• Gameplay: End the game if there are no possible matches that can be made via swap
• Gameplay: Add a 'combo meter' that drains slowly, but gains with each match made. A 100% meter will reward bonus for next matches
• Gameplay: Add 'com.rmc.core.grid_system.GridSystem.Frequency.Never' and allow game to initially render with zero matches.
• View: Handle common screen layouts (iPhone 6, 4, etc...) without simply resizing gracefully as currently happens.







*/
