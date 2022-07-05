﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The difficulty menu
/// </summary>
public class DifficultyMenu : MonoBehaviour
{
    #region Fields

    // events invoked by the class
    public static event UnityAction<Difficulty> gameStarted;
    //GameStartedEvent gameStartedEvent = new GameStartedEvent();

    #endregion

    #region Unity methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
		// add invoker to event manager
		//EventManager.AddGameStartedInvoker(this);
	}

	#endregion

	#region Public methods

	/// <summary>
	/// Starts an easy game
	/// </summary>
	public void StartEasyGame()
	{
        gameStarted.Invoke(Difficulty.Easy);
        //gameStartedEvent.Invoke(Difficulty.Easy);
	}

	/// <summary>
	/// Starts a medium game
	/// </summary>
	public void StartMediumGame()
	{
        gameStarted.Invoke(Difficulty.Medium);
        //gameStartedEvent.Invoke(Difficulty.Medium);
	}

	/// <summary>
	/// Starts a hard game
	/// </summary>
	public void StartHardGame()
	{
        gameStarted.Invoke(Difficulty.Hard);
        //gameStartedEvent.Invoke(Difficulty.Hard);
	}

	/// <summary>
	/// Adds the given listener for the game started event
	/// </summary>
	/// <param name="listener">listener</param>
	// public void AddGameStartedListener(UnityAction<Difficulty> listener)
	// {
	// 	gameStartedEvent.AddListener(listener);
	// }

	#endregion
}
