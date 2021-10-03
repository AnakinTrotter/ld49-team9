using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanArrow : MonoBehaviour
{
    private float delay = 5f;
    public Transform pacifier, parent;
    private float alpha = 1f;
    private float fadeSpeed = .02f;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.gameObject.transform;
        setAlpha(alpha);
    }

    // Update is called once per frame
    void Update()
    {
        if(pacifier == null || alpha == 0f)
        {
            Destroy(this.gameObject);
            return;
        }
        delay -= Time.deltaTime;
        if(delay <= 0) {
            setAlpha(alpha - fadeSpeed);
        }
        Vector3 dir = pacifier.position - parent.position;
        this.transform.position = parent.position + dir / dir.magnitude * 2 - new Vector3(0,0,1);
        this.transform.rotation = Quaternion.Euler(0,0, Mathf.Rad2Deg * Mathf.Atan(dir.y / dir.x));
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
