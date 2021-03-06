﻿using System.Collections.Generic;
using UnityEngine;
using GameClasses;

public class Platform : MonoBehaviour {

    public PlatformProperties Properties { get { return _properties; } }
    
    [SerializeField]
    private List<ScrollingObject> _obstaclePrefabs = null;
    [SerializeField]
    private List<ScrollingObject> _collectiblesPrefabs = null;
    [SerializeField]
    private List<ScrollingObject> _specialPrefabs = null;
    [SerializeField]
    private SpriteRenderer _renderer1, _renderer2;

    private PlatformProperties _properties;

    public void Initialize(Vector3 spawnPosition, bool createObstacles = true)
    {
        _properties = new PlatformProperties();
        _properties.Initialize();
        _renderer1.sprite = _properties.Sprite;
        _renderer2.sprite = _properties.Sprite;
        _renderer1.size = new Vector2(_properties.Length, 0.45f);
        _renderer2.size = new Vector2(_properties.Length, 0.45f);
        GetComponent<BoxCollider2D>().size = new Vector2(_properties.Length, 0.1f);
        transform.position = spawnPosition + Vector3.right * (_properties.Length * 0.5f) * 5f;
        if(createObstacles)
            CreateObstacles();
        CreateCollectibles();
        CreateSpecials();
    }

    private void CreateObstacles()
    {
        int obstaclesCount = Random.Range(1, (int)(_properties.Length/2f));
        for(int i = 0; i < obstaclesCount; i++)
        {
            int index = Random.Range(0, _obstaclePrefabs.Count);
            float randomizer = (_properties.Length * 0.5f * 5f) * 0.9f;
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, _obstaclePrefabs[index].transform.position.z);
            spawnPosition += Random.Range(-randomizer, randomizer) * Vector3.right;
            var obs = Instantiate(_obstaclePrefabs[index], spawnPosition, Quaternion.identity);
            obs.GetComponent<SpriteRenderer>().flipX = (Random.Range(0, 2) != 1 ? true : false);
        }
    }

    private void CreateCollectibles()
    {
        for(int i = 0; i < _properties.Length*2f; i+=2)
        {
            Vector3 spawnPosition = transform.position - Vector3.right * (_properties.Length / 2f) * 5f + Vector3.right * i * 2f + Vector3.up;
            Instantiate(_collectiblesPrefabs[Random.Range(0, _collectiblesPrefabs.Count)], spawnPosition, Quaternion.identity);
        }
    }

    private void CreateSpecials()
    {
        if (Random.Range(0, 100) > 20f)
            return;
        Vector3 spawnPosition = transform.position + Vector3.up;
        Instantiate(_specialPrefabs[Random.Range(0, _specialPrefabs.Count)], spawnPosition, Quaternion.identity);
    }

    public Vector3 GetEndPoint()
    {
        return transform.position + Vector3.right * (_properties.Length / 2f) * 5f;
    }
}
