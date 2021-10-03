using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanArrow : MonoBehaviour
{
    public Transform pacifier, parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(pacifier == null)
        {
            Destroy(this.gameObject);
            return;
        }
        Debug.DrawLine(pacifier.position, parent.position);
        Vector3 dir = pacifier.position - parent.position;
        this.transform.position = parent.position + dir / dir.magnitude * 2;
        this.transform.rotation = Quaternion.Euler(0,0, Mathf.Rad2Deg * Mathf.Atan(dir.y / dir.x));
    }
}
