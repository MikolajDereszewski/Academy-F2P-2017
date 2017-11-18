﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameClasses;

public class InterfaceRun : MonoBehaviour
{
    public const float MAP_DISTANCE = 1000;

    public Slider slider;
    public Text coins;
    public Image headImage;

    private static bool isAura = false;
    private float time;
    private float timeDeltaTime = 0;
    private float screenWidth;
    private float converter;

    [SerializeField]
    private float _mapModifierDistance = 1f;
    [SerializeField]
    private float _auraTakerMultiplier = 1f;

    private static InterfaceRun _thisScript;

    void Start()
    {
        _thisScript = this;

        slider.minValue = 0;
        slider.maxValue = 100;
        slider.wholeNumbers = false;
        slider.value = 100;

        headImage.transform.position = new Vector2(2, -8);
        screenWidth = Screen.width;
        converter = screenWidth / MAP_DISTANCE;
        headImage.transform.position = new Vector2(DifficultyManager.GetGameTime(Time.time) * converter, -8);
    }

    void Update()
    {
        headImage.transform.position += new Vector3(DifficultyManager.GetGameSpeed() * _mapModifierDistance * Time.deltaTime, 0);

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
}