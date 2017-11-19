using Records;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseHero : MonoBehaviour
{
    public Text bestScore;
    public Sprite[] listImage;
    public string[] listName;
    public Image imageSelected;
    public Text nameSelected;

    private int actualIndexHero;
    void Start()
    {
        if (PlayerPrefs.HasKey(BasePrefName.HERO_INDEX))
            actualIndexHero = PlayerPrefs.GetInt(BasePrefName.HERO_INDEX);
        else
            actualIndexHero = 0;

        Statistics.LoadValuesFromPrefs();
        bestScore.text = Statistics._totalCoins.ToString();

        UpdateHero();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateHero()
    {
        imageSelected.sprite = listImage[actualIndexHero];
        nameSelected.text = listName[actualIndexHero];
    }

    public void LeftClick()
    {
        if (actualIndexHero > 0)
            actualIndexHero--;
        else if (actualIndexHero == 0)
            actualIndexHero = listName.Length - 1;

        UpdateHero();
    }

    public void RightClick()
    {
        if (actualIndexHero < listName.Length - 1)
            actualIndexHero++;
        else if (actualIndexHero == listName.Length - 1)
            actualIndexHero = 0;

        UpdateHero();
    }
}
