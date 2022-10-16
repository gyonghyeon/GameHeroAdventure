using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int stage;

    public Mobs mobs;
    public Heal heals;
    private void Start()
    {
      
    }
    public void setActiveHeals()
    {
        heals.setActive();
    }
    public void disableHeals()
    {
        heals.disable();
    }

    public void setActiveMobs()
    {
        mobs.setActive();
    }
    public void disableMobs()
    {
        mobs.disable();
    }
}
