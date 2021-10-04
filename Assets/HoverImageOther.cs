using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverImageOther : MonoBehaviour
{
    public Sprite normal;
    public Sprite hovered;
    public GameObject other;
    private bool hover = false;

    private void Update()
    {
        if (hover)
            other.GetComponent<Image>().sprite = hovered;
    }

    private void OnMouseEnter()
    {
        other.GetComponent<Image>().sprite = hovered;
        hover = true;
    }
    private void OnMouseExit()
    {
        other.GetComponent<Image>().sprite = normal;
        hover = false;
    }
}
