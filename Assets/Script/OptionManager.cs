using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public bool showControlText = true;
    public Toggle ShowControlToggle;
    public GameObject ControlText;
    
    private void Start()
    {
        ShowControlToggle.onValueChanged.
            AddListener(ControlTextControl);
    }
    public void ControlTextControl (bool isOn)
    {
        if (isOn)
        {
            ControlText.SetActive(true);
        }
        else
        {
            ControlText.SetActive(false);
        }
    }
}
