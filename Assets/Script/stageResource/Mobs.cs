using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs : MonoBehaviour
{
    List<GameObject> mobs = new List<GameObject>();
    GameObject mob;
    private void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            mob = transform.GetChild(i).gameObject;
            mobs.Add((GameObject)mob);
            
        }
    }
    public void setActive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            mob = (GameObject)mobs[i];
            mob.SetActive(true);
        }
    }
    public void disable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            mob = (GameObject)mobs[i];
            mob.SetActive(false);
        }
    }
}
