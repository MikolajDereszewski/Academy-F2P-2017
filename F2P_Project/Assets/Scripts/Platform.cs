﻿using UnityEngine;
using GameClasses;

public class Platform : MonoBehaviour {

    public PlatformProperties Properties { get { return _properties; } }
    
    [SerializeField]
    private ScrollingObject _obstaclePrefab = null;

    private PlatformProperties _properties;

    public void Initialize(Vector3 spawnPosition, bool createObstacles = true)
    {
        _properties = new PlatformProperties();
        _properties.Initialize();
        transform.localScale = new Vector3(_properties.Length, transform.localScale.y, transform.localScale.z);
        transform.position = spawnPosition + Vector3.right * (_properties.Length * 0.5f);
        if(createObstacles)
            CreateObstacles();
    }

    private void CreateObstacles()
    {
        int obstaclesCount = Random.Range(1, (int)(_properties.Length / 7));
        for(int i = 0; i < obstaclesCount; i++)
        {
            float randomizer = (_properties.Length * 0.5f - (_obstaclePrefab.transform.localScale.x * 0.5f));
            Vector3 spawnPosition = new Vector3(Random.Range(-randomizer, randomizer) + transform.position.x, _obstaclePrefab.transform.position.y, _obstaclePrefab.transform.position.z);
            Instantiate(_obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }

    public Vector3 GetEndPoint()
    {
        return transform.position + Vector3.right * (_properties.Length / 2f);
    }
}
