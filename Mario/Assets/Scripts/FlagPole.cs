using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class FlagPole : MonoBehaviour
{
    int time = 0;
    int startScore;
    int endScore = 1000;
    int testScore;

    public float transitionDuration = 10000f; // Ge�i� s�resi
    public float startValue = 0f; // Ba�lang�� de�eri
    public float endValue = 100f; // Hedef de�er
    float lerpedValue;
    private float lerpedTime;
    private float elapsedTime = 0f; // Ge�en s�re
    public Transform flag;
    public Transform poleBottom;
    public Transform poleEnd;
 

    public Transform castle;
    public float speed = 6f;
  

    private void Start()
    {
       
    }
    private void Update()
    {
        startScore = PlayerPrefs.GetInt("Score");
        //   Debug.Log(endScore);
        //    Debug.Log(testScore);

        //    elapsedTime += Time.deltaTime; // Ge�en s�reyi g�ncelle

        //    // Lerp fonksiyonuyla de�eri yava��a artt�r

        //    // De�eri kullan veya uygula
        //    // �rne�in:

        //    // Ge�i� tamamland���nda s�f�rla veya durdur
        //    if (elapsedTime >= transitionDuration)
        //    {
        //        // Ge�i�i tamamla veya durdur
        //        elapsedTime = 0f;
        //        // �stedi�iniz bir �ey yapabilirsiniz
        //    }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreManager.instance.isGameFinish = true;

        endScore = startScore + 100 * (int)ScoreManager.instance.time;
        StartCoroutine(LerpValue(ScoreManager.score, endScore, ScoreManager.instance.time, 0));
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform player = collision.gameObject.GetComponent<Transform>();
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            flag.DOMoveY(poleBottom.position.y, 1f);
            SoundManager.instance.PlayWithIndex(8);
            playerRb.velocity = Vector2.zero;
            LevelEnd(player);
            
            
        }


        //StartCoroutine(MoveTo(flag, poleBottom.position));
        //StartCoroutine(LevelComplete(collision.transform));



    }

    IEnumerator LerpValue(float startValue, float endValue, float time, float endTime)// zaman eksilirken puan artmas� i�in yapt���m�z time mekani�i
    {
        float timeElapsed = 0;
        while (elapsedTime < transitionDuration)
        {
            float t = timeElapsed / transitionDuration;// ge�i� zaman� / b�t�n zaman olarak ald�k
            lerpedValue = Mathf.Lerp(startValue, endValue, t);// ba�lang�� de�erini biti� de�erini ge�i� s�res� zaman�nda verir
            lerpedTime = Mathf.Lerp(time, endTime, t);
            timeElapsed += Time.deltaTime;// ge�en zaman� ger�ek zamana atar
            Debug.Log(lerpedValue);
            ScoreManager.instance.scoreText.text = "SCORE : " + lerpedValue.ToString("F0");
            ScoreManager.instance.timerText.text = "TIME : " + lerpedTime.ToString("F0");
            if (PlayerPrefs.GetInt("High Score")<lerpedValue)
            {
                PlayerPrefs.SetInt("High Score", (int)lerpedValue);
            }

            yield return null;
        }
        lerpedValue = endValue;

    }
    void LevelEnd(Transform player)
    {
        Sequence mySqeu = DOTween.Sequence();

        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerBoundry>().enabled = false;

        mySqeu.Append(player.DOMove(poleBottom.position, 0.5f, snapping: false));
        mySqeu.Append(player.DOMove(poleEnd.position, 0.1f, snapping: false));
        mySqeu.Append(player.DOMove(castle.position, 2.5f, snapping: false));
  
        




    }

    //private IEnumerator LevelComplete(Transform player)
    //{
    //    player.GetComponent<PlayerMovement>().enabled = false;
    //    yield return MoveTo(player,poleBottom.position);
    //    yield return MoveTo(player, player.position+Vector3.right);
    //    yield return MoveTo(player,player.position+Vector3.right+Vector3.down);
    //    yield return MoveTo(player,castle.position);
    //    player.gameObject.SetActive(false);
    //}
    //private IEnumerator MoveTo(Transform subject, Vector3 destination)
    //{
    //    while (Vector3.Distance(subject.position,destination) > 0.125f) 
    //    {
    //        subject.position=Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
    //        yield return null;
    //    }
    //    subject.position = destination; 
    //}

}
