using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameClasses;

[System.Serializable]
class Skin
{
    public Sprite Background { get { return _background; } }
    public Sprite Tree { get { return _tree; } }
    public List<Sprite> Platforms { get { return _platforms; } }

    [SerializeField]
    private Sprite _background;
    [SerializeField]
    private Sprite _tree;
    [SerializeField]
    private List<Sprite> _platforms;
}

public class SkinCollection : MonoBehaviour {

    [SerializeField]
    private List<Skin> _skins;
    
    [SerializeField]
    private int _usedSkinsID = 0;

    private void Awake()
    {
        GameSkin.SetGameSkin(_skins[_usedSkinsID].Background, _skins[_usedSkinsID].Tree, _skins[_usedSkinsID].Platforms);
    }
}
