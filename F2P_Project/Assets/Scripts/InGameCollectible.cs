﻿using UnityEngine;

public class InGameCollectible : MonoBehaviour
{
    [SerializeField]
    private Sprite _sprite = null;
    [SerializeField]
    private SpriteRenderer _renderer = null;
    [SerializeField]
    private Collectible _collectible = null;
    [SerializeField]
    private GameObject _afterCollectPrefab = null;

    private void Awake()
    {
        _renderer.sprite = _sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PLAYER")
        {
            if (collision.GetComponent<Player>().HasMaskOpened)
                return;
            CollectiblesCounter.AddCollectible(_collectible);
            Instantiate(_afterCollectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}