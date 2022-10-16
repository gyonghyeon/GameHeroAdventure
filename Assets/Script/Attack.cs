using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "wall" || collision.gameObject.tag == "Monster")
        {
            Destroy(gameObject);
           
        }
      
    }
}
