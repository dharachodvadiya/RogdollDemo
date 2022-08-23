using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public GameObject objPlay;
    public GameObject objResult;

    public Player player;

    public Text txtResultMsg;

    public static bool isReplay = false;

    private void Start()
    {
        if(isReplay)
        {
            unloadAll();
            player.startGame();
        }
        else
        {
            loadPlay();
        }
        isReplay = false;
    }

    public void loadPlay()
    {
        objPlay.SetActive(true);
        objResult.SetActive(false);
    }
    
    public void loadResult(string message)
    {
        txtResultMsg.text = message;
        objPlay.SetActive(false);
        objResult.SetActive(true);
    }

    public void unloadAll()
    {
        objPlay.SetActive(false);
        objResult.SetActive(false);
    }

    #region Button_click

    public void btn_play()
    {
        player.startGame();
        unloadAll();
    }

    public void btnReplay()
    {
        isReplay = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
