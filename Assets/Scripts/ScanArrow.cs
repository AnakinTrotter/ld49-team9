using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanArrow : MonoBehaviour
{
    public Transform pacifier, parent;
    private float alpha, delay = 200, fadeSpeed = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.gameObject.transform;
        orient();
        setAlpha(1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(pacifier == null || alpha <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
        orient();

        if(delay-- <= 0)
            setAlpha(alpha - fadeSpeed);

    }

    private void setAlpha(float a)
    {
        alpha = Mathf.Max(0, a);
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

    private void orient()
    {
        Vector3 dir = pacifier.position - parent.position;
        this.transform.position = parent.position + dir / dir.magnitude * 2 + new Vector3(0,0,1);
        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan(dir.y / dir.x));
    }
}
