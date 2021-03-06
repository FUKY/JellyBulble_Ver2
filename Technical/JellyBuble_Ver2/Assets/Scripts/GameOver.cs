﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {


    public GameController gameController;
    public Text yourScore;
    public Text hightScore;
	// Use this for initialization
	void Start () {
        SaveScore();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //lưu Hight Score cho người chơi
    public void SaveScore()
    {

        if (gameController != null)
        {
            if (gameController.score > PlayerPrefs.GetInt("Score"))//kiểm tra điểm người chơi có cao hơn HightScore
            {
                PlayerPrefs.SetInt("Score", gameController.score);
                PlayerPrefs.Save();//Lưu Hight Score
            }
        }
        SetText();
    }
    public void SetText()
    {
        if (gameController != null)
        {
            yourScore.text = gameController.score.ToString();
        }
        hightScore.text = "Hight Score: " + PlayerPrefs.GetInt("Score");
    }
    public int GetHightScore()
    {
        int _hightScore = PlayerPrefs.GetInt("Score");
        return _hightScore;
    }
}
