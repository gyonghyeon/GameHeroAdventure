using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    List<GameObject> heals = new List<GameObject>();
    GameObject heal;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            heal = transform.GetChild(i).gameObject;
            heals.Add((GameObject)heal);

        }
    }
    public void setActive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            heal = (GameObject)heals[i];
            heal.SetActive(true);
        }
    }
    public void disable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            heal = (GameObject)heals[i];
            heal.SetActive(false);
        }
    }
}
