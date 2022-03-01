using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{    
    public Text timerText;
    public Text roomScore;
    public GameOverScreen GameOverScreen;

    private float timeRemaining=20F;
    private GameObject player;
    private bool gameEnd=false;

    // Update is called once per frame
    void Update()
    {
      if (timeRemaining > 0)
      {
          timeRemaining -= Time.deltaTime;
      }
      else
      {   
         
          timeRemaining = 0;
          if(!gameEnd){
            player = GameObject.Find("Player");
            player.SetActive(false);
            GameOverScreen.Setup(4-int.Parse(roomScore.text));
          }
          gameEnd=true;
      }
      if(gameEnd){
        timerText.text = "0.00";
      }else{
        timerText.text = timeRemaining.ToString("0.00");
      }
    }
}
