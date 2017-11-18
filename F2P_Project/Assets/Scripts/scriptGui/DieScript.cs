using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieScript : MonoBehaviour {
    const string PREF_NAME = MainMenu.NAME_PREF;

    public Text textScore;
    public Text textBestScore;
	
	void Start () {
        if (PlayerPrefs.HasKey(PREF_NAME))
            textBestScore.text = "Najlepszy wynik: " + PlayerPrefs.GetInt(PREF_NAME);
        else
            textBestScore.text = "Najlepszy wynik: " + 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
