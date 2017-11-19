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
        public static int _nutsLevel;

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
            _soundEnabled = PlayerPrefs.GetInt("SOUND") == 0 ? false : true;
            _nutsLevel = PlayerPrefs.GetInt("NUTS_LEVEL");
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
            PlayerPrefs.SetInt("NUTS_LEVEL", _nutsLevel);
            PlayerPrefs.Save();
        }

        public static void ResetAllPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void IncreaseNutsLevel()
        {
            _nutsLevel++;
        }

        public static int GetNutsThreshold(int level)
        {
            return 3 + level;
        }

        public static int GetTotalNeededNuts(int level)
        {
            if (level < 0)
                return 0;
            if (level == 0)
                return 3;
            return GetNutsThreshold(_nutsLevel) + GetTotalNeededNuts(level - 1);
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
        [SerializeField]
        private Slider _nutsSlider = null;

        [SerializeField]
        private Animator _nutCardAnimator = null;

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
            int val = (cNuts + Statistics._totalNuts);
            int trsh = Statistics.GetNutsThreshold(Statistics._nutsLevel);
            int ttl = Statistics.GetTotalNeededNuts(Statistics._nutsLevel);
            _thisScript._nutsSliderText.text = (val >= ttl ? val - ttl + trsh : val - Statistics.GetTotalNeededNuts(Statistics._nutsLevel - 1)).ToString() + " / " + trsh.ToString();
            _thisScript._nutsSlider.value = (val - Statistics.GetTotalNeededNuts(Statistics._nutsLevel - 1)) / (float)trsh;
            _thisScript._nutsText.text = cNuts.ToString();
            _thisScript._energyText.text = cEnergy.ToString();
            _thisScript._rocketsText.text = cRockets.ToString();
            _thisScript._websText.text = cWebs.ToString();

            if (Statistics.GetTotalNeededNuts(Statistics._nutsLevel) <= (cNuts + Statistics._totalNuts))
                _thisScript.CollectedNeededNuts();

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

        private void CollectedNeededNuts()
        {
            _nutCardAnimator.Play("GameOverSliding");
            Statistics.IncreaseNutsLevel();
        }
    }
}

