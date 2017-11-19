using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum BonusType
{
    Coins = 0,
    Cap = 1,
    Star = 2
}

[System.Serializable]
public class BonusProperties
{
    public string name;
    public BonusType type;
    public Sprite sprite;
    public int amount;
}

public class BonusCard : MonoBehaviour
{
    [SerializeField]
    private List<BonusProperties> _bonusList;

    public BonusProperties GetRandomBonus()
    {
        return _bonusList.FirstOrDefault(_ => _.type == GetRandomBonusType());
    }

    private BonusType GetRandomBonusType()
    {
        int rand = Random.Range(0, 100);
        if (rand <= 60)
            return BonusType.Coins;
        else if (rand <= 85)
            return BonusType.Star;
        else return BonusType.Cap;
    }
}
