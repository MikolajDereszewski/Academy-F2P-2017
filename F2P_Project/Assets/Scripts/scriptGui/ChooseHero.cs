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

        if (PlayerPrefs.HasKey(BasePrefName.BEST_SCORE))
            bestScore.text = "Best score: " + PlayerPrefs.GetInt(BasePrefName.BEST_SCORE);
        else
            bestScore.text = "Best score: " + PlayerPrefs.GetInt(BasePrefName.BEST_SCORE);

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
