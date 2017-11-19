using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameClasses;

public class InterfaceRun : MonoBehaviour
{
    public const float MAP_DISTANCE = 1000;

    public static InterfaceRun ThisScript { get { return _thisScript; } }
    public static float Mana { get { return (_thisScript != null) ? _thisScript.slider.value : 100f; } }
    public static float ManaLaunchMinimum { get { return 5f * ((_thisScript != null) ? _thisScript._auraTakerMultiplier : 2); } }

    public Slider slider;
    public Text coins;
    public Image headImage, headTarget;
    public Image panelPause;

    private static bool isAura = false;
    private float time;
    private float timeDeltaTime = 0;
    private float screenWidth;
    private float converter;
    
    [SerializeField]
    private float _auraTakerMultiplier = 1f;

    private static InterfaceRun _thisScript;

    private float distanceHead;

    void Start()
    {
        _thisScript = this;

        slider.minValue = 0;
        slider.maxValue = 100;
        slider.wholeNumbers = false;
        slider.value = 100;

        distanceHead = (headTarget.transform.position.x - headImage.transform.position.x) / 6f;

        screenWidth = Screen.width;
        converter = screenWidth / MAP_DISTANCE;
    }

    void Update()
    {
        headImage.transform.position += new Vector3((distanceHead / GameBehaviour.GetCurrentLevelInfo().Time) * Time.deltaTime, 0);

        if (!isAura)
        {
            timeDeltaTime += Time.deltaTime;
            if (timeDeltaTime > 0.125f)
            {
                timeDeltaTime -= 0.125f;
                slider.value += 0.25f;
            }
        }
        else
        {
            timeDeltaTime += Time.deltaTime;
            if (timeDeltaTime > 0.1f)
            {
                timeDeltaTime -= 0.1f;
                slider.value -= 0.5f * _auraTakerMultiplier;
            }
        }
    }

    public static void AuraKeyDetection(bool pressed)
    {
        isAura = pressed;
    }

    public static void ThrowLine()
    {
        _thisScript.slider.value -= 5 * _thisScript._auraTakerMultiplier;
    }

    public static void CollectMana(int value)
    {
        _thisScript.slider.value += value;
    }

    public static void OnPlayerDied()
    {
        _thisScript.enabled = false;
    }

    public void HidePanelPause()
    {
        if (panelPause.isActiveAndEnabled)
            panelPause.enabled = true;
        else
            panelPause.enabled = false;
    }
}
