using Records;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {
    public Text golden;
    public Text silver;
    public Text mana;
    public Text nuts;
    public Text modeSpeed;
    public Text modeSlow;
    public Text allScore;

    void Start () {
        Statistics.LoadValuesFromPrefs();

        golden.text = Statistics._totalGoldenCoins.ToString();
        silver.text = Statistics._totalSilverCoins.ToString();
        allScore.text = Statistics._totalCoins.ToString();
        mana.text = Statistics._totalEnergy.ToString();
        nuts.text = Statistics._totalNuts.ToString();
        modeSlow.text = Statistics._totalWebs.ToString();
        modeSpeed.text = Statistics._totalRockets.ToString();
	}
}
