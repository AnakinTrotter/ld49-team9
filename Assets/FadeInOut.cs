using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private float pos = 0f;
    [SerializeField] private float speed = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos += speed;
        setAlpha(Mathf.Abs(Mathf.Sin(pos)));
    }

    private void setAlpha(float a)
    {
        a = Mathf.Max(0, Mathf.Min(a, 1f));
        Image sprite = this.GetComponent<Image>();
        Color color = sprite.color;
        color.a = a;
        sprite.color = color;
    }
}
