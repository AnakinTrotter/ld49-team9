using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonscript : MonoBehaviour
{
    public Transform player;
    public Transform baby;
    public Transform firepoint;
    public Transform firepoint2;
    public Transform cannon;
    public GameObject bullet;
    float timebetween;
    public float starttimebetween;
    // Start is called before the first frame update
    void Start()
    {
      timebetween = starttimebetween;  
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x <= baby.position.x + 3 && player.position.x >= baby.position.x -3 ){
            if(timebetween <= 0){
                if(player.position.x > cannon.position.x)
                {
                    Instantiate(bullet,firepoint.position,firepoint.rotation);
                }
                else
                {
                    Instantiate(bullet,firepoint2.position,firepoint2.rotation);
                }
                
                timebetween = starttimebetween;
    
            }
            else
            {
                timebetween -= Time.deltaTime;
            }

        }
      
    }
}
