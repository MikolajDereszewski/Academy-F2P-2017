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
                RecordContainer.cCoins += (int)_collectible.Count;
                if (_collectible.Count == 5)
                    RecordContainer.cSilver++;
                else
                    RecordContainer.cGold++;
                break;
            case CollectibleType.Nut:
                RecordContainer.cNuts += (int)_collectible.Count;
                break;
            case CollectibleType.Mana:
                RecordContainer.cEnergy ++;
                break;
            case CollectibleType.Rocket:
                RecordContainer.cRockets++;
                break;
            case CollectibleType.Web:
                RecordContainer.cWebs++;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PLAYER")
        {
            if (collision.GetComponent<Player>().HasMaskOpened)
                return;
            switch(_collectible.Type)
            {
                case CollectibleType.Nut:
                    InterfaceRun.CollectMana(10);
                    CollectiblesCounter.AddCollectible(_collectible);
                    break;
                case CollectibleType.Coin:
                    CollectiblesCounter.AddCollectible(_collectible);
                    break;
                case CollectibleType.Mana:
                    InterfaceRun.CollectMana((int)_collectible.Count);
                    break;
                case CollectibleType.Rocket:
                case CollectibleType.Web:
                    GameBehaviour.GameBehaviourScript.StartCoroutine(GameBehaviour.ChangeSpeedMultiplier(_collectible.Count, 10f));
                    break;
            }

            if(_afterCollectPrefab != null)
                Instantiate(_afterCollectPrefab, transform.position, Quaternion.identity);

            AddToTotalScore();
            Destroy(gameObject);
        }
    }
}
