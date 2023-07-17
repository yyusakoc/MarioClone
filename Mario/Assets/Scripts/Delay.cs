using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{
    [SerializeField] float delayTimer=3f; //unity �zerinde istedi�imiz zaman� verebilmek i�in ekledik

    public bool delayTime = true;

    #region Singleton
    public static Delay instance;//singleton

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    public void StartDelayTime()
    {
        StartCoroutine(DelayNewTime()); //bu fonksiyonun kendisini direk �a�iramad���m�z i�in IEnumeratorden dolay�, yeni bir fonksiyon olu�turup �yle �a��r�yoruz
    }

    IEnumerator DelayNewTime()
    {
        if (delayTime)
        {
            yield return new WaitForSeconds(delayTimer);
            LevelManager.instance.PlayerRespawn();            
        }
        
    }
}
