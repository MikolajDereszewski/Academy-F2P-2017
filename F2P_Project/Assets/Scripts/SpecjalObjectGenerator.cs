using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class SpecialItem
{
    public InGameCollectible Prefab { get { return _prefab; } }
    public int Chance { get { return _chance; } }

    [SerializeField]
    private InGameCollectible _prefab;

    [Range(0, 100)]
    [SerializeField]
    private int _chance;
}

public class SpecjalObjectGenerator : MonoBehaviour {

    [SerializeField]
    private List<SpecialItem> _specialItems;
    [SerializeField]
    private Rect _spawnRect;

    private void Start()
    {
        int randomItem = Random.Range(0, _specialItems.Count);
        if(Random.Range(0, 100) <= _specialItems[randomItem].Chance)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(_spawnRect.x, _spawnRect.x + _spawnRect.width), Random.Range(_spawnRect.y, _spawnRect.y + _spawnRect.height), 0f);
            Instantiate(_specialItems[randomItem].Prefab, spawnPosition, Quaternion.identity);
        }
        Destroy(this);
    }
}
