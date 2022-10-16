using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressedBtn : MonoBehaviour
{
    [SerializeField] Sprite[] swapImage = new Sprite[2];
    public GameObject text;
    public void ClickDownChange()
    {
        GetComponent<Image>().sprite = swapImage[1];
        Vector2 textposition = text.GetComponent<RectTransform>().anchoredPosition;
        text.GetComponent<RectTransform>().anchoredPosition
            = new Vector2(textposition.x, textposition.y - 20);
    }
    public void ClickUpChange()
    {
        GetComponent<Image>().sprite = swapImage[0];
        Vector2 textposition = text.GetComponent<RectTransform>().anchoredPosition;
        text.GetComponent<RectTransform>().anchoredPosition
            = new Vector2(textposition.x, textposition.y + 20);
    }
}
