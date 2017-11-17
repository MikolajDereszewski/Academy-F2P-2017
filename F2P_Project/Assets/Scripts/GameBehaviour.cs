using UnityEngine;

public class GameBehaviour : MonoBehaviour {

    public static GameBehaviour GameBehaviourScript { get { return _thisObject; } }
    public static Vector2 GapSize { get { return GameBehaviourScript._gapSize; } }
    public static Vector2 PlatformSize { get { return GameBehaviourScript._platformSize; } }
    public static float GameSpeed { get { return GameBehaviourScript._gameSpeed; } }

    [SerializeField]
    private Vector2 _gapSize;
    [SerializeField]
    private Vector2 _platformSize;
    [SerializeField]
    private float _gameSpeed;

    private static GameBehaviour _thisObject;

    private void Awake()
    {
        _thisObject = this;
    }
}