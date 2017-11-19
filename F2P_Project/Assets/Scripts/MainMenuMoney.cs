using UnityEngine;
using Records;
using UnityEngine.UI;

public class MainMenuMoney : MonoBehaviour {

    [SerializeField]
    private Text _text = null;
    [SerializeField]
    private Text _textTopScore = null;

    void Start ()
    {
        Statistics.LoadValuesFromPrefs();
        _text.text = "Money: " + Statistics._currentCoins.ToString();
        _textTopScore.text = "Highscore: " + Statistics._topScore.ToString();
    }
}
