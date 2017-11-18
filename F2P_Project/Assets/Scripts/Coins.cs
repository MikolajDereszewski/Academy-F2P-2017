using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private int score = 0;
    private int energia = 0;
    private int nut = 0;
    private bool modeSpeed = false;
    private bool modeSlow = false;
    private Image image;

    public Coin(int score, int energia, int nut, Image image)
    {
        this.score = score;
        this.energia = energia;
        this.nut = nut;
        this.image = image;
    }

    public Coin(bool modeSpeed, bool modeSlow, Image image)
    {
        this.modeSlow = modeSlow;
        this.modeSpeed = modeSpeed;
        this.image = image;
    }

    public Coin(int score, int energia, int nut, bool modeSpeed, bool modeSlow, Image image)
    {
        this.score = score;
        this.energia = energia;
        this.nut = nut;
        this.modeSpeed = modeSpeed;
        this.modeSlow = modeSlow;
        this.image = image;
    }
}
