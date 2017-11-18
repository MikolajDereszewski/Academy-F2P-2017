using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseHero : MonoBehaviour
{
    const string MONEY = BasePrefName.MONEY;
    const string PREF_NAME = BasePrefName.HERO_INDEX;

    public Text bestScore;
    public Sprite[] listImage;
    public string[] listName;
    public Image imageSelected;
    public Text nameSelected;

    private int actualIndexHero;
    void Start()
    {
        if (PlayerPrefs.HasKey(PREF_NAME))
            actualIndexHero = PlayerPrefs.GetInt(PREF_NAME);
        else
            actualIndexHero = 0;

        if (PlayerPrefs.HasKey(MONEY))
            bestScore.text = "Najlepszy wynik: " + PlayerPrefs.GetInt(MONEY);
        else
            bestScore.text = "Najlepszy wynik: " + PlayerPrefs.GetInt(MONEY);

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
