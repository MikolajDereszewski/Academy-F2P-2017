using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text textScore;
    public Text textMoney;
    public Sprite disabledSound;
    public Sprite enabledSound;
    public Button buttonSound;

    private bool playSound;

    void Start()
    {
        if (PlayerPrefs.HasKey(BasePrefName.BEST_SCORE))
            textScore.text = "Best score: " + PlayerPrefs.GetInt(BasePrefName.BEST_SCORE);
        else
            textScore.text = "Best score: " + PlayerPrefs.GetInt(BasePrefName.BEST_SCORE);

        if (PlayerPrefs.HasKey(BasePrefName.ALL_SCORE))
            textMoney.text = "Money: " + PlayerPrefs.GetInt(BasePrefName.ALL_SCORE);
        else
            textMoney.text = "Money: " + PlayerPrefs.GetInt(BasePrefName.ALL_SCORE);

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
