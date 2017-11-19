using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Records
{
    public static class Statistics
    {
        public static int _totalCoins;
        public static int _currentCoins;
        public static int _totalNuts;
        public static int _totalEnergy;
        public static int _totalRockets;
        public static int _totalWebs;
        public static int _topScore;

        public static void LoadValuesFromPrefs()
        {
            _totalCoins = PlayerPrefs.GetInt("TOTAL_COINS");
            _currentCoins = PlayerPrefs.GetInt("CURRENT_COINS");
            _totalNuts = PlayerPrefs.GetInt("TOTAL_NUTS");
            _totalEnergy = PlayerPrefs.GetInt("TOTAL_ENERGY");
            _totalRockets = PlayerPrefs.GetInt("TOTAL_ROCKETS");
            _totalWebs = PlayerPrefs.GetInt("TOTAL_WEBS");
            _topScore = PlayerPrefs.GetInt("HIGHSCORE");
        }

        public static void SaveValuesToPrefs()
        {
            PlayerPrefs.SetInt("TOTAL_COINS", _totalCoins);
            PlayerPrefs.SetInt("CURRENT_COINS", _currentCoins);
            PlayerPrefs.SetInt("TOTAL_NUTS", _totalNuts);
            PlayerPrefs.SetInt("TOTAL_ENERGY", _totalEnergy);
            PlayerPrefs.SetInt("TOTAL_ROCKETS", _totalRockets);
            PlayerPrefs.SetInt("TOTAL_WEBS", _totalWebs);
            PlayerPrefs.SetInt("HIGHSCORE", _topScore);
            PlayerPrefs.Save();
        }
    }

    public class RecordContainer : MonoBehaviour
    {
        public static int cCoins;
        public static int cNuts;
        public static int cEnergy;
        public static int cRockets;
        public static int cWebs;

        [SerializeField]
        private Text _scoreText = null;
        [SerializeField]
        private Text _highscoreText = null;
        [SerializeField]
        private Text _coinText = null;
        [SerializeField]
        private Text _nutsText = null;
        [SerializeField]
        private Text _energyText = null;
        [SerializeField]
        private Text _rocketsText = null;
        [SerializeField]
        private Text _websText = null;

        public static RecordContainer _thisScript;

        private void Awake()
        {
            Statistics.LoadValuesFromPrefs();
            _thisScript = this;
        }

        public static void OnPlayerDied()
        {
            _thisScript._scoreText.text += cCoins.ToString();
            _thisScript._coinText.text += cCoins.ToString();
            _thisScript._nutsText.text = cNuts.ToString() + " / 10";
            _thisScript._energyText.text += cEnergy.ToString();
            _thisScript._rocketsText.text += cRockets.ToString();
            _thisScript._websText.text += cWebs.ToString();

            if (cCoins > Statistics._topScore)
                Statistics._topScore = cCoins;

            _thisScript._highscoreText.text += Statistics._topScore.ToString();

            Statistics._totalCoins += cCoins;
            Statistics._currentCoins += cCoins;
            Statistics._totalNuts += cNuts;
            Statistics._totalEnergy += cEnergy;
            Statistics._totalRockets += cRockets;
            Statistics._totalWebs += cWebs;

            Statistics.SaveValuesToPrefs();
        }
    }
}

