using Records;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {
    public Text mana;
    public Text nuts;
    public Text modeSpeed;
    public Text modeSlow;
    public Text allScore;

    void Start () {
        Statistics.LoadValuesFromPrefs();

        allScore.text = Statistics._totalCoins.ToString();
        mana.text = Statistics._totalEnergy.ToString();
        nuts.text = Statistics._totalNuts.ToString();
        modeSlow.text = Statistics._totalWebs.ToString();
        modeSpeed.text = Statistics._totalRockets.ToString();
	}
}
