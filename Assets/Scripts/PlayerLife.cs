using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void TakeDamage(int x){
        Globals.lives -= x;
        if(Globals.lives <= 0){
            Die();
        }
    }
    private void Die(){

    }
    private void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
