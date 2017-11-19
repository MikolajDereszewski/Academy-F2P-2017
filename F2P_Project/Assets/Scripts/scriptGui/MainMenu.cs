using Records;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text textScore;
    public Text textMoney;
    public Button buttonSound;
    public Sprite soundActive;
    public Sprite soundUnactive;

    private bool playSound;

    void Start()
    {
        Statistics.LoadValuesFromPrefs();
        if (Statistics._soundEnabled == 0)
        {
            playSound = false;
            buttonSound.image.sprite = soundUnactive;
            AudioListener.pause = true;
            AudioListener.volume = 0;
        }
        else
        {
            playSound = true;
            buttonSound.image.sprite = soundActive;
            AudioListener.volume = 1;
            Statistics._soundEnabled = 1;
        }
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
        Statistics.LoadValuesFromPrefs();

        if (playSound)
        {
            buttonSound.image.sprite = soundUnactive;
            playSound = false;
            AudioListener.pause = true;
            AudioListener.volume = 0;
            Statistics._soundEnabled = 0;
            Statistics.SaveValuesToPrefs();
        }
        else
        {
            buttonSound.image.sprite = soundActive;
            playSound = true;
            AudioListener.pause = false;
            AudioListener.volume = 1;
            Statistics._soundEnabled = 1;
            Statistics.SaveValuesToPrefs();
        }
    }
}
