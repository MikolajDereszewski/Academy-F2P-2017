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
        public static int _totalGoldenCoins;
        public static int _totalSilverCoins;
        public static int _totalNuts;
        public static int _totalEnergy;
        public static int _totalRockets;
        public static int _totalWebs;
        public static int _topScore;
        public static bool _soundEnabled;
        public static int _slot_0;
        public static int _slot_1;
        public static int _slot_2;
        public static int _slot_3;
        public static int _slot_4;
        public static int _slot_5;

        public static void LoadValuesFromPrefs()
        {
            _totalCoins = PlayerPrefs.GetInt("TOTAL_COINS");
            _currentCoins = PlayerPrefs.GetInt("CURRENT_COINS");
            _totalGoldenCoins = PlayerPrefs.GetInt("TOTAL_GOLDEN_COINS");
            _totalSilverCoins = PlayerPrefs.GetInt("TOTAL_SILVER_COINS");
            _totalNuts = PlayerPrefs.GetInt("TOTAL_NUTS");
            _totalEnergy = PlayerPrefs.GetInt("TOTAL_ENERGY");
            _totalRockets = PlayerPrefs.GetInt("TOTAL_ROCKETS");
            _totalWebs = PlayerPrefs.GetInt("TOTAL_WEBS");
            _topScore = PlayerPrefs.GetInt("HIGHSCORE");
            _soundEnabled = PlayerPrefs.HasKey("SOUND") ? (PlayerPrefs.GetInt("SOUND") == 0 ? false : true) : true;
            _slot_0 = PlayerPrefs.GetInt("SLOT_0");
            _slot_1 = PlayerPrefs.GetInt("SLOT_1");
            _slot_2 = PlayerPrefs.GetInt("SLOT_2");
            _slot_2 = PlayerPrefs.GetInt("SLOT_3");
            _slot_2 = PlayerPrefs.GetInt("SLOT_4");
            _slot_2 = PlayerPrefs.GetInt("SLOT_5");
        }

        public static void SaveValuesToPrefs()
        {
            PlayerPrefs.SetInt("TOTAL_COINS", _totalCoins);
            PlayerPrefs.SetInt("CURRENT_COINS", _currentCoins);
            PlayerPrefs.SetInt("TOTAL_GOLDEN_COINS", _totalGoldenCoins);
            PlayerPrefs.SetInt("TOTAL_SILVER_COINS", _totalSilverCoins);
            PlayerPrefs.SetInt("TOTAL_NUTS", _totalNuts);
            PlayerPrefs.SetInt("TOTAL_ENERGY", _totalEnergy);
            PlayerPrefs.SetInt("TOTAL_ROCKETS", _totalRockets);
            PlayerPrefs.SetInt("TOTAL_WEBS", _totalWebs);
            PlayerPrefs.SetInt("HIGHSCORE", _topScore);
            PlayerPrefs.SetInt("SOUND", _soundEnabled ? 1 : 0);
            PlayerPrefs.SetInt("SLOT_0", _slot_0);
            PlayerPrefs.SetInt("SLOT_1", _slot_1);
            PlayerPrefs.SetInt("SLOT_2", _slot_2);
            PlayerPrefs.SetInt("SLOT_3", _slot_2);
            PlayerPrefs.SetInt("SLOT_4", _slot_2);
            PlayerPrefs.SetInt("SLOT_5", _slot_2);
            PlayerPrefs.Save();
        }
    }

    public class RecordContainer : MonoBehaviour
    {
        public static int cCoins;
        public static int cGold, cSilver;
        public static int cNuts;
        public static int cEnergy;
        public static int cRockets;
        public static int cWebs;

        [SerializeField]
        private Text _scoreText = null;
        [SerializeField]
        private Text _highscoreText = null;
        [SerializeField]
        private Text _coinGoldenText = null;
        [SerializeField]
        private Text _coinSilverText = null;
        [SerializeField]
        private Text _nutsSliderText = null;
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
            _thisScript._coinGoldenText.text = cGold.ToString();
            _thisScript._coinSilverText.text = cSilver.ToString();
            _thisScript._nutsSliderText.text = (cNuts + Statistics._totalNuts).ToString() + " / 10";
            _thisScript._nutsText.text = cNuts.ToString();
            _thisScript._energyText.text = cEnergy.ToString();
            _thisScript._rocketsText.text = cRockets.ToString();
            _thisScript._websText.text = cWebs.ToString();

            if (cCoins > Statistics._topScore)
                Statistics._topScore = cCoins;

            _thisScript._highscoreText.text += Statistics._topScore.ToString();

            Statistics._totalCoins += cCoins;
            Statistics._totalGoldenCoins += cGold;
            Statistics._totalSilverCoins += cSilver;
            Statistics._currentCoins += cCoins;
            Statistics._totalNuts += cNuts;
            Statistics._totalEnergy += cEnergy;
            Statistics._totalRockets += cRockets;
            Statistics._totalWebs += cWebs;

            cCoins = 0;
            cGold = 0;
            cSilver = 0;
            cNuts = 0;
            cEnergy = 0;
            cRockets = 0;
            cWebs = 0;

            Statistics.SaveValuesToPrefs();
        }
    }
}

