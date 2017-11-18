using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieScript : MonoBehaviour {
    const string PREF_NAME = BasePrefName.BEST_SCORE;
    const string NAME_NUTS = BasePrefName.NUTS_PROGRESS;

    public Slider slider;

    public Text textScore;
    public Text textBestScore;
    public Text textOnSlider;
    public Text itemSilverMoney;
    public Text itemGoldMoney;
    public Text itemNuts;
    public Text itemMana;
    public Text itemModeSpeed;
    public Text itemModeSlow;

    private int maxNut = 10;
    private int actualNuts;

    public DieScript(int score, int nutsProgress, int silverMoney, int goldMoney, int mana, int modeSpeed, int modeSlow)
    {
        //Score and BestScore
        if (PlayerPrefs.HasKey(PREF_NAME))
        {
            if (score > PlayerPrefs.GetInt(PREF_NAME))
            {
                textScore.text = "Score: " + score;
                textBestScore.text = "Best score: " + score;
                PlayerPrefs.SetInt(BasePrefName.ALL_SCORE, PlayerPrefs.GetInt(BasePrefName.ALL_SCORE) + score);
                PlayerPrefs.SetInt(BasePrefName.BEST_SCORE, score);
            }
            else
            {
                textScore.text = "Score: " + score;
                textBestScore.text = "Best score: " + PlayerPrefs.GetInt(PREF_NAME);
                PlayerPrefs.SetInt(BasePrefName.ALL_SCORE, PlayerPrefs.GetInt(BasePrefName.ALL_SCORE) + score);
            }
        }
        else
        {
            textBestScore.text = "Best score: " + 0;
            textScore.text = "Score: " + score;
            PlayerPrefs.SetInt(BasePrefName.ALL_SCORE, PlayerPrefs.GetInt(BasePrefName.ALL_SCORE) + score);
            PlayerPrefs.SetInt(BasePrefName.BEST_SCORE, score);
        }

        //Slider and Progrees
        if (PlayerPrefs.HasKey(NAME_NUTS))
        {
            actualNuts = PlayerPrefs.GetInt(NAME_NUTS) + nutsProgress;
            if (actualNuts < 10)
                PlayerPrefs.SetInt(BasePrefName.NUTS_PROGRESS, actualNuts);
            else
            {
                actualNuts -= 10;
                PlayerPrefs.SetInt(BasePrefName.NUTS_PROGRESS, actualNuts);
                //TODO implement tree random card
            }
        }
        else
        {
            actualNuts = nutsProgress;
            PlayerPrefs.SetInt(BasePrefName.NUTS_PROGRESS, actualNuts);
        }

        textOnSlider.text = actualNuts + " / " + maxNut;
        slider.minValue = 0;
        slider.maxValue = maxNut;
        slider.wholeNumbers = true;
        slider.value = actualNuts;

        //Silver money
        itemSilverMoney.text = silverMoney.ToString();
        PlayerPrefs.SetInt(BasePrefName.SILVER_MONEY, PlayerPrefs.GetInt(BasePrefName.SILVER_MONEY) + silverMoney);

        //Gold money
        itemGoldMoney.text = goldMoney.ToString();
        PlayerPrefs.SetInt(BasePrefName.GOLD_MONEY, PlayerPrefs.GetInt(BasePrefName.GOLD_MONEY) + goldMoney);

        //Nuts
        itemNuts.text = nutsProgress.ToString();
        PlayerPrefs.SetInt(BasePrefName.NUTS, PlayerPrefs.GetInt(BasePrefName.NUTS) + nutsProgress);

        //Mana
        itemMana.text = mana.ToString();
        PlayerPrefs.SetInt(BasePrefName.MANA, PlayerPrefs.GetInt(BasePrefName.MANA) + mana);

        //Mode speed
        itemModeSpeed.text = modeSpeed.ToString();
        PlayerPrefs.SetInt(BasePrefName.MODE_SPEED, PlayerPrefs.GetInt(BasePrefName.MODE_SPEED) + modeSpeed);

        //Mode slow
        itemModeSlow.text = modeSlow.ToString();
        PlayerPrefs.SetInt(BasePrefName.MODE_SLOW, PlayerPrefs.GetInt(BasePrefName.MODE_SLOW) + modeSlow);

        PlayerPrefs.Save();
    }

	void Start () {
    }
}
