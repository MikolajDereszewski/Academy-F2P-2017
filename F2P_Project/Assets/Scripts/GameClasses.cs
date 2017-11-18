using UnityEngine;

namespace GameClasses
{
    public enum PlayerState
    {
        Running = 0,
        Jumping = 1,
        Falling = 2,
        Sliding = 3,
        Dead = 4
    }

    public enum GameState
    {
        BeforeStart = 0,
        Running = 1,
        Pause = 2,
        GameOver = 3
    }

    public static class DifficultyManager
    {
        private static float _startTime;
        private static float _levelStartTime;
        private static int _level;

        public static float GetPlatformLengthMultiplier()
        {
            return (1f + _level * 0.5f) * GetGameSpeed();
        }

        public static float GetGameSpeed()
        {
            return (1 + _level * 0.2f) * GameBehaviour.GameSpeed;
        }

        public static float GetGameTime(float currentTime)
        {
            return currentTime - _startTime;
        }

        public static float GetLevelTime(float currentTime)
        {
            return currentTime - _levelStartTime;
        }

        public static void SetStartTime(float currentTime)
        {
            _startTime = currentTime;
            _levelStartTime = currentTime;
            _level = 1;
        }

        public static void OnIncreaseLevel(float currentTime)
        {
            _level++;
            _levelStartTime = currentTime;
        }
    }
    
    public static class GameSkin
    {
        public static Sprite Background { get { return _background; } }
        public static Sprite Platform { get { return _platform; } }
        public static Sprite Tree { get { return _tree; } }

        private static Sprite _background;
        private static Sprite _platform;
        private static Sprite _tree;

        public static void SetGameSkin(Sprite background, Sprite platform, Sprite tree)
        {
            _background = background;
            _platform = platform;
            _tree = tree;
        }
    }

    public enum TapType
    {
        None = 0,
        Left = 1,
        Right = 2
    }

    public static class InputManager
    {
        public static TapType GetPlayerInput(bool hold = false)
        {
            if ((Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began || (hold && Input.GetTouch(0).phase == TouchPhase.Stationary))) || ((hold && Input.GetMouseButton(0)) || Input.GetMouseButtonDown(0)))
            {
                Vector2 position = (Input.touchCount > 0) ? Input.GetTouch(0).position : new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                if (position.y >= Screen.height * 3f / 4f)
                    return TapType.None;
                else return (position.x >= Screen.width / 2f) ? TapType.Right : TapType.Left;
            }
            else return TapType.None;
        }

        public static bool GetRequestedPlayerInput(TapType request, bool hold = false)
        {
            if (Input.touchCount == 0)
                return (GetPlayerInput(hold) == request);
            for(int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began || (hold && Input.GetTouch(0).phase == TouchPhase.Stationary))
                {
                    Vector2 position = (Input.touchCount > 0) ? Input.GetTouch(0).position : new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    if (position.y >= Screen.height * 3f / 4f)
                        continue;
                    if (((position.x >= Screen.width / 2f) ? TapType.Right : TapType.Left) == request)
                        return true;
                }
            }
            return false;
        }
    }
    
    public class PlatformProperties
    {
        public float Length { get { return _length; } }
        
        private float _length;
        private Sprite _sprite;

        public void Initialize()
        {
            Vector2 rand = GameBehaviour.PlatformSize;
            _length = UnityEngine.Random.Range(rand.x, rand.y);
            //_sprite = GameSkin.Platform;
        }
    }
}