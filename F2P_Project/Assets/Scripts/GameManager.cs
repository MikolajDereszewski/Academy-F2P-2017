using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameClasses;
using System;

public class GameManager : MonoBehaviour {

    public event Action<float> IncreaseLevel;

    [SerializeField]
    private Platform _platformPrefab = null;

    private GameState _gameState = GameState.BeforeStart;

    private Platform _currentActivePlatform;

    private void Awake()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        IncreaseLevel += DifficultyManager.OnIncreaseLevel;
        DifficultyManager.SetStartTime(Time.time);
        _gameState = GameState.Running;
    }

    private Platform CreatePlatform()
    {
        Vector3 spawnPosition = _currentActivePlatform.GetEndPoint();
        var newPlatform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity);
        newPlatform.Initialize();
    }

    private Vector3 GetRandomGap()
    {

    }
}
