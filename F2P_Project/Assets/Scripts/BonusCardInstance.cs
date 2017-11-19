using UnityEngine;
using UnityEngine.UI;

public class BonusCardInstance : MonoBehaviour {

    [SerializeField]
    private BonusCard _cardScript = null;
    [SerializeField]
    private Text _cardTitle;
    [SerializeField]
    private Image _cardSprite;

    public void AddRandomBonus()
    {
        GetComponent<Animator>().Play("CardSwap");
        BonusProperties bonus = _cardScript.GetRandomBonus();
        if (bonus == null)
            return;
        _cardTitle.text = bonus.name;
        _cardSprite.sprite = bonus.sprite;
        ApplyBonus();
    }

    private void ApplyBonus()
    {
        //Save bonus into memory;
    }
}
