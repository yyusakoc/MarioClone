using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundry : MonoBehaviour
{
   
    float xMove;

    // Update is called once per frame
    void Update()
    {
        xMove = Mathf.Clamp(transform.position.x, 0, 210);// player�n x ekseni boyunca + ve- de�erleri aras�nda kalmas� i�in..
        transform.position = new Vector2(xMove,transform.position.y);// y ekseninde yukar� ��kma s�n�r�.
        
    }

}
