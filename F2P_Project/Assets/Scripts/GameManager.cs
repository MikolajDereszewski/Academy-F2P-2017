using UnityEngine;
using GameClasses;
using System;

public class GameManager : MonoBehaviour {

    public event Action<float> IncreaseLevel;

    [SerializeField]
    private Platform _platformPrefab = null;
    [SerializeField]
    private GameObject _treePrefab = null;

    private GameState _gameState = GameState.BeforeStart;
    
    [SerializeField]
    private Platform _currentActivePlatform;
    private float _currentActiveGap;

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (Camera.main.WorldToViewportPoint(GetCurrentPlatformEndPoint()).x < 1)
            _currentActivePlatform = CreatePlatform();
    }

    private void InitializeGame()
    {
        _currentActivePlatform.Initialize(_currentActivePlatform.transform.position, false);
        IncreaseLevel += DifficultyManager.OnIncreaseLevel;
        DifficultyManager.SetStartTime(Time.time);
        _gameState = GameState.Running;
    }

    private Platform CreatePlatform()
    {
        _currentActiveGap = GetRandomGap();
        Vector3 endPoint = GetCurrentPlatformEndPoint();
        var newPlatform = Instantiate(_platformPrefab, endPoint, Quaternion.identity);
        Vector3 spawnPosition = Vector3.right * (_currentActiveGap + endPoint.x) + Vector3.up * GetPlatformSpawnY();
        newPlatform.Initialize(spawnPosition);
        return newPlatform;
    }

    private void CreateTree(float positionX)
    {
        Vector3 spawnPosition = new Vector3(positionX, _treePrefab.transform.position.y, _treePrefab.transform.position.z);
        Instantiate(_treePrefab, spawnPosition, Quaternion.identity);
    }

    private float GetRandomGap()
    {
        Vector2 rand = GameBehaviour.GapSize;
        float gap = UnityEngine.Random.Range(rand.x, rand.y);
        int treeCount = (int)(gap / 6f);
        for (int i = 1; i < treeCount; i++)
        {
            CreateTree(GetCurrentPlatformEndPoint().x + i * (gap / treeCount));
        }
        return gap;
    }

    private Vector3 GetCurrentPlatformEndPoint()
    {
        return (_currentActivePlatform == null) ? Vector3.zero : _currentActivePlatform.GetEndPoint();
    }

    private float GetPlatformSpawnY()
    {
        return (Camera.main.ViewportToWorldPoint(Vector3.zero).y + UnityEngine.Random.Range(0.5f, 3f));
    }
}
