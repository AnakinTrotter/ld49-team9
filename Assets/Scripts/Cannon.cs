using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Sprite bulletSprite;
    [SerializeField] private bool facingLeft;
    [SerializeField] private float shotInterval = 3f;
    private float dirX;
    private float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = shotInterval;
        dirX = facingLeft ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
        } else {
            Shoot();
            timeLeft = shotInterval;
        }
    }
    
    void Shoot()
    {
        Debug.Log(bullet == null);
        bullet.GetComponent<SpriteRenderer>().sprite = bulletSprite;
        Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        bulletInstance.velocity = new Vector2(dirX * Bullet.speed, 0);
    }
}
