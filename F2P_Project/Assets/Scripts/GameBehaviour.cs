using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {

    public static GameBehaviour GameBehaviourScript { get { return _thisObject; } }
    public static Vector2 GapSize { get { return _gapSize; } }
    public static Vector2 PlatformSize { get { return _platformSize; } }

    [SerializeField]
    private static Vector2 _gapSize;
    [SerializeField]
    private static Vector2 _platformSize;

    private static GameBehaviour _thisObject;

    private void Awake()
    {
        _thisObject = this;
    }
}
