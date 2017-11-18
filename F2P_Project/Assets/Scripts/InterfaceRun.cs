using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceRun : MonoBehaviour
{
    public const float MAP_DISTANCE = 1000;

    public Slider slider;
    public Text coins;
    public Image headImage;
    public Transform player;

    private bool isAura = false;
    private float time;
    private float timeDeltaTime = 0;
    private float screenWidth;
    private float converter;


    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.wholeNumbers = false;
        slider.value = 100;

        headImage.transform.position = new Vector2(2, -8);
        screenWidth = Screen.width;
        converter = screenWidth / MAP_DISTANCE;
        headImage.transform.position = new Vector2(player.position.x * converter, -8);
    }

    void Update()
    {
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
                slider.value -= 0.5f;
            }
        }
    }

    public void AuraKeyDown()
    {
        isAura = true;
    }

    public void AuraKeyUp()
    {
        isAura = false;
    }

    public void ThrowLine()
    {
        slider.value -= 5;
    }
}
