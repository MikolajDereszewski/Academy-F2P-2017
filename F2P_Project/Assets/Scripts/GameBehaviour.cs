using System.Collections.Generic;
using UnityEngine;
using GameClasses;

[System.Serializable]
public class Level
{
    public string Name { get { return _name; } }
    public float Time { get { return _timeNeeded; } }
    public bool Trees { get { return _treesEnabled; } }
    public bool DoubleTrees { get { return _doubleTreesEnabled; } }

    [SerializeField]
    private string _name;
    [SerializeField]
    private float _timeNeeded = 0f;

    [SerializeField]
    private bool _treesEnabled;
    [SerializeField]
    private bool _doubleTreesEnabled;
}

public class GameBehaviour : MonoBehaviour {

    public static GameBehaviour GameBehaviourScript { get { return _thisObject; } }
    public static Vector2 GapSize { get { return GameBehaviourScript._gapSize * DifficultyManager.GetPlatformLengthMultiplier() / 5f; } }
    public static Vector2 PlatformSize { get { return GameBehaviourScript._platformSize * DifficultyManager.GetPlatformLengthMultiplier() / 5f; } }
    public static float GameSpeed { get { return GameBehaviourScript._gameSpeed; } }

    [SerializeField]
    private Vector2 _gapSize;
    [SerializeField]
    private Vector2 _platformSize;
    [SerializeField]
    private float _gameSpeed;
    [SerializeField]
    private List<Level> _levels;

    private static GameBehaviour _thisObject;

    private void Awake()
    {
        _thisObject = this;
    }

    public static Level GetCurrentLevelInfo()
    {
        return _thisObject._levels[DifficultyManager.CurrentLevel];
    }
}