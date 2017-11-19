using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using GameClasses;

[System.Serializable]
public class Level
{
    public string Name { get { return _name; } }
    public float Time { get { return _timeNeeded; } }
    public bool Trees { get { return _treesEnabled; } }
    public bool DoubleTrees { get { return _doubleTreesEnabled; } }

    public Vector2 GapSize { get { return _gapSize; } }
    public Vector2 PlatformSize { get { return _platformSize; } }

    [SerializeField]
    private string _name;
    [SerializeField]
    private float _timeNeeded = 0f;

    [SerializeField]
    private bool _treesEnabled;
    [SerializeField]
    private bool _doubleTreesEnabled;

    [SerializeField]
    private Vector2 _gapSize;
    [SerializeField]
    private Vector2 _platformSize;
}

public class GameBehaviour : MonoBehaviour {

    public static GameBehaviour GameBehaviourScript { get { return _thisObject; } }
    public static float GameSpeed { get { return GameBehaviourScript._gameSpeed * GameBehaviourScript._multiplier; } }

    [SerializeField]
    private float _gameSpeed;
    [SerializeField]
    private List<Level> _levels;
    [SerializeField]
    private float _multiplier;

    private static GameBehaviour _thisObject;

    private void Awake()
    {
        _gameSpeed = 10f;
        _thisObject = this;
        _multiplier = 1f;
    }

    public static IEnumerator ChangeSpeedMultiplier(float amount, float time)
    {
        _thisObject._multiplier *= amount;
        yield return new WaitForSeconds(time);
        _thisObject._multiplier /= amount;
    }

    public static Level GetCurrentLevelInfo()
    {
        return _thisObject._levels[DifficultyManager.CurrentLevel];
    }

    public static void OnPlayerDied()
    {
        _thisObject._gameSpeed = 0f;
    }
}