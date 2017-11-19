using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLoadScene : MonoBehaviour {

    [SerializeField]
    private float _time;
    [SerializeField]
    private string _level;

    private void Start()
    {
        Invoke("Load", _time);
    }

    private void Load()
    {
        Application.LoadLevel(_level);
    }
}
