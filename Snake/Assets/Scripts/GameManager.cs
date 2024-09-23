using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    } 
    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
