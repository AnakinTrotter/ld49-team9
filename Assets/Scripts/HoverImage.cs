using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverImage : MonoBehaviour
{
    public Sprite normal;
    public Sprite hovered;
    private bool hover = false;

    private void Update()
    {
        if(hover)
            this.GetComponent<Image>().sprite = hovered;
    }

    private void OnMouseEnter()
    {
        this.GetComponent<Image>().sprite = hovered;
        hover = true;
    }
    private void OnMouseExit()
    {
        this.GetComponent<Image>().sprite = normal;
        hover = false;
    }
}
