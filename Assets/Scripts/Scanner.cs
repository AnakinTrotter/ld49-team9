using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private static readonly float fadeSpeed = 0.05f;
    private static readonly float growSpeed = 0.5f;
    private static readonly float maxSize = 50;
    public GameObject scanArrow;
    private float size, alpha;

    // Start is called before the first frame update
    void Start()
    {
        size = 0f;
        setAlpha(0.5f);
        this.gameObject.transform.localScale = new Vector3(size, size, 0);
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if (size < maxSize)
        {
            size += growSpeed;
            this.gameObject.transform.localScale = new Vector3(size, size, 0);
            setAlpha(alpha - fadeSpeed / 10);
        }
        else if (alpha > 0)
            setAlpha(alpha - fadeSpeed);
        else
        {
            scanArea();
            Destroy(this.gameObject);
        }
    }

    private GameObject[] scanArea()
    {
        Collider2D[] pacifiers = Physics2D.OverlapCircleAll(this.transform.position, maxSize / 2, 1 << 3);
        GameObject player = GameObject.FindWithTag("Player");
        foreach (Collider2D pacifier in pacifiers)
        {
            scanArrow.GetComponent<ScanArrow>().pacifier = pacifier.transform;
            Instantiate(scanArrow, player.transform);
        }
        return null;
    }

    private void setAlpha(float a)
    {
        alpha = Mathf.Max(0, a);
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }
}
