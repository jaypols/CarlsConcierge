using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen: MonoBehaviour {

  public Text scoreText;

  public void Setup(int score) {

    gameObject.SetActive(true);
    scoreText.text = score.ToString() + " Room Points";

  }

  public void PlayAgainButton() {
    SceneManager.LoadScene("Game");

  }

  public void HomeButton() {
    SceneManager.LoadScene("Home");

  }
}