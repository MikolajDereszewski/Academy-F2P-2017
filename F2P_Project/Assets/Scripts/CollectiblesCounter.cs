using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum CollectibleType
{
    Coin = 0,
    Nut = 1,
    Mana = 2,
    Rocket = 3,
    Web = 4
}

[System.Serializable]
public class Collectible
{
    public CollectibleType Type { get { return _type; } }
    public float Count { get { return _count; } }

    [SerializeField]
    private CollectibleType _type;
    [SerializeField]
    private float _count;

    public Collectible(CollectibleType type, float count = 0)
    {
        _type = type;
        _count = count;
    }

    public void AddCount(int number)
    {
        _count += number;
    }
}

[System.Serializable]
class Counter
{
    public Text Text;
    public CollectibleType Type;
}

public class CollectiblesCounter : MonoBehaviour {

    public List<Collectible> Collectibles { get { return _collectibles; } }

    [SerializeField]
    private List<Collectible> _collectibles;
    [SerializeField]
    private List<Counter> _counters;

    private static CollectiblesCounter _thisCounter;

    private void Awake()
    {
        _thisCounter = this;
    }

    private void Update()
    {
        foreach(Counter C in _counters)
        {
            Collectible col = _collectibles.FirstOrDefault(_ => _.Type == C.Type);
            if (col == null)
                continue;
            C.Text.text = col.Count.ToString();
        }
    }

    public static void AddCollectible(Collectible col)
    {
        Collectible counter = _thisCounter.Collectibles.FirstOrDefault(_ => _.Type == col.Type);
        if (counter == null)
            return;
        counter.AddCount((int)col.Count);
    }
}
