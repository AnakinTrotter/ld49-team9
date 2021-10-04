using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageBar : MonoBehaviour
{
    private Image bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
        bar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // setColor();
        setSize();
    }

    public void setColor()
    {
        if (Globals.babyRage <= 3)
            bar.color = Color.green;
        if (Globals.babyRage > 3 && Globals.babyRage < 7)
            bar.color = Color.yellow;
        else
            bar.color = Color.red;

    }
    public void setSize()
    {
        bar.fillAmount = Globals.babyRage / 10f;
    }
}
