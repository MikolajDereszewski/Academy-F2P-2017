using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    const string NAME_PREF = BasePrefName.BEST_SCORE;
    const string MONEY = BasePrefName.MONEY;

    public Text textScore;
    public Text textMoney;
    public Sprite disabledSound;
    public Sprite enabledSound;
    public Button buttonSound;

    private bool playSound;

    void Start()
    {
        if (PlayerPrefs.HasKey(NAME_PREF))
            textScore.text = "Njlepszy wynik: " + PlayerPrefs.GetInt(NAME_PREF);
        else
            textScore.text = "Njalepszy wynik: " + PlayerPrefs.GetInt(NAME_PREF);

        if (PlayerPrefs.HasKey(NAME_PREF))
            textMoney.text = "Pieniądze: " + PlayerPrefs.GetInt(MONEY);
        else
            textMoney.text = "Pieniądze: " + PlayerPrefs.GetInt(MONEY);

    }

    public void OpenScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void EnabledSound()
    {
        if (playSound)
        {
            buttonSound.image.sprite = disabledSound;
            playSound = false;
        }
        else
        {
            buttonSound.image.sprite = enabledSound;
            playSound = true;
        }
    }
}
