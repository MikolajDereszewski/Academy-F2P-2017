using Records;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public Text[] countItem;
    public Text itemName;
    public Text itemDescription;
    public ItemClass[] items;

    void Start()
    {
        Statistics.LoadValuesFromPrefs();

        for (int i = 0; i < countItem.Length; i++)
        {
            countItem[i].text = PlayerPrefs.GetInt("SLOT_" + i).ToString();
        }

        items = new ItemBase().items;
    }

    public void ClickItem(int index)
    {
        itemName.text = items[index].Name;
        itemDescription.text = items[index].Description();
    }
}

public class ItemClass
{
    private string name;
    public string Name { get { return name; } }

    private int speed;
    private int jump;

    public ItemClass(string name, int speed, int jump)
    {
        this.name = name;
        this.speed = speed;
        this.jump = jump;
    }

    public string Description()
    {
        return "+" + speed + " speed\n+" + jump + " jump";
    }

}

public class ItemBase
{
    public ItemClass[] items = {
        new ItemClass("Hat", 5, 3),
        new ItemClass("Rocket", 3, 2),
        new ItemClass("Cash", 2, 2),
        new ItemClass("Bomb", 4, 6),
        new ItemClass("Shoes", 6, 6),
        new ItemClass("Star", 1, 3)
    };
}