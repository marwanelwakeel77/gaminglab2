using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    public int health = 3;
    public int lives=3;
    private float flickerTime=0f;
    public float flickerDuration=0.1f;
    private SpriteRenderer sr;
    public bool isImune = false;
    private float immunityTime=0f;
    public float immunityDuration=1.5f;
    public static int score=0;
    public TextMeshProUGUI scoreUI;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        sr=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      if(isImune==true)
      {
        SpriteFlicker();
        immunityTime=immunityTime+Time.deltaTime;
        if(immunityTime>=immunityDuration)
        {
            isImune=false;
            sr.enabled=true;
        }
      }
      scoreUI.text= " "+score;
    }
    void SpriteFlicker()
    {
     if(flickerTime<flickerDuration)
        {
         flickerTime=flickerTime+Time.deltaTime;
        }
        else if(flickerTime>=flickerDuration)
        {
            sr.enabled=!(sr.enabled);
            flickerTime=0;
        }
       
    }
    public void TakeDamage(int damage)
    {
        if(isImune==false)
        {
            health=health-damage;
            healthBar.fillAmount=this.health/3f;
             Debug.Log("lost health");
            if(health<=0)
            health=0;

            if(lives>0 && health==0)
            {
              FindObjectOfType<LevelManager>().RespawnPlayer();
              health=3;
              healthBar.fillAmount=this.health/3f;
               Debug.Log("lost a live");
              lives--;
            }
            else if(lives==0 && health==0)
            {
              Debug.Log("Game Over");
              Destroy(this.gameObject);
              SceneManager.LoadScene(1);
            }
            Debug.Log("Player Health: "+health.ToString());
            Debug.Log("Player Lives: "+lives.ToString());
        }
        isImune=true;
        immunityTime=0f;
    }
}
