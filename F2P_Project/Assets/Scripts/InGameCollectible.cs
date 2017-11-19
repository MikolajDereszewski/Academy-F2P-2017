using UnityEngine;
using Records;

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

    private void AddToTotalScore()
    {
        switch(_collectible.Type)
        {
            case CollectibleType.Coin:
                RecordContainer.cCoins += _collectible.Count;
                break;
            case CollectibleType.Nut:
                RecordContainer.cNuts += _collectible.Count;
                break;
            case CollectibleType.Mana:
                RecordContainer.cEnergy += _collectible.Count;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PLAYER")
        {
            if (collision.GetComponent<Player>().HasMaskOpened)
                return;
            if (_collectible.Type == CollectibleType.Mana)
                InterfaceRun.CollectMana(_collectible.Count);
            else
                CollectiblesCounter.AddCollectible(_collectible);

            if(_afterCollectPrefab != null)
                Instantiate(_afterCollectPrefab, transform.position, Quaternion.identity);

            AddToTotalScore();
            Destroy(gameObject);
        }
    }
}
