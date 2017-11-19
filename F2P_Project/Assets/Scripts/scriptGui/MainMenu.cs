﻿using System.Collections;
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
