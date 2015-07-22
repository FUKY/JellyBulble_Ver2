using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour {

    public int countDelete;
    public int[] totalDelete;
    public GameObject[] list;
    public GameController gameController;
    public GameObject objTotalGem;
    public GameObject timeImgage;
    public GameObject pause;
    private Image fillAmount;
    public Text txtCountGem;
    public Text txtScore;
    public float timeGame = 60;
    public Text textTimeSecond;
    private float timeDelay = 0;

    public ButtonController buttonController;
    private float timeGameIT;
    private int scoreIT;
    private bool isPause;
    public bool isGameOver;
	// Use this for initialization
    void Start()
    {
        isGameOver = false;
        isPause = false;
        countDelete = 0;
        totalDelete = new int[6];
        timeGameIT = timeGame;
        if (gameController != null)
        {
            scoreIT = gameController.score;
        }
        fillAmount = objTotalGem.GetComponent<Image>();
        if (fillAmount != null)
        {
            fillAmount.fillAmount = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause == false)
        {
            if (timeDelay > 1 && timeGame > 0)
            {
                timeGame -= 1;//sau 1s thời gian giảm xuống
                if (timeGame <= 0)// nếu < 0 sẽ xuất hiện màn hình Game Over
                {
                    isGameOver = true;
                    GameOver();
                }
                timeDelay = 0;
            }
            if (timeGame != timeGameIT)
            {
                Scale("ChangleScaleTime");//hiệu ứng to nhỏ thời gian
                timeGameIT = timeGame;
            }
            if (gameController != null)
            {
                if (gameController.score != scoreIT)
                {
                    Scale("ChangleScaleScore");//hiệu ứng to nhỏ điểm
                    scoreIT = gameController.score;
                }
            }
            timeDelay += Time.deltaTime;
            ChangleFillAmount();
            UpdateTime();
        }
    }
    public void AddTime(int _timeAdd)
    {
        timeGame += _timeAdd;
        if (timeGame > 60)
        {
            timeGame = 60;
        }
    }
    void UpdateTime()
    {
        SetText();
        timeImgage.transform.localScale = new Vector3((timeGame * 0.0166f), 1f, 1f);
        timeImgage.GetComponent<Image>().color = Color.Lerp(Color.green, Color.red, 1 - (timeGame * 0.0166f));
    }
    public void SetText()
    {
        textTimeSecond.text = timeGame.ToString();
        txtCountGem.text = countDelete.ToString();
        buttonPause.GetComponentInChildren<Text>().text = "Pause";
        txtScore.text = System.String.Format("{0}", gameController.score);
    }

    public int timeAddValue = 0;
    void ChangleFillAmount()
    {        
        fillAmount.fillAmount = (1 - countDelete * 0.1f);
        if (fillAmount.fillAmount <= 0)
        {
            if (countDelete >= 10 && countDelete <= 12)
            {
                timeAddValue = 10;
            }
            if (countDelete > 12 && countDelete <= 15)
            {
                timeAddValue = 15;
            }
            if (countDelete > 15)
            {
                timeAddValue = 22;
            }
            gameController.activeAddtime = true;  
            
        }

    }
    public void RandomSpecial(int count, int i)
    {
        if (totalDelete[i] > 15)
        {
            int rand = UnityEngine.Random.Range(0, 100);

            if (rand < 20)
            {
                gameController.indexRandom = 2;
            }
            if (rand > 20 && rand < 60)
            {
                gameController.indexRandom = 1;
            }
            if (rand > 60)
            {
                gameController.indexRandom = 0;
            }
            totalDelete[i] = 0;

        }
    }
    public void GameOver()
    {        
        buttonController.GameOver();
    }
    public void GameRelay()
    {
        timeGame = 60;
        intPause = 0;
        isPause = false;
        isGameOver = false;
        timeGameIT = timeGame;
        totalDelete = new int[6];
        if (gameController != null)
        {
            scoreIT = gameController.score;
        }
        countDelete = 0;
        timeDelay = 0;
        SetText();
    }
    
    void Scale(String name)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
                   iT.ValueTo.from, 0f,
                   iT.ValueTo.to, 1f,
                   iT.ValueTo.time, 0.5f,
                   iT.ValueTo.onupdate, name,
                   iT.MoveTo.oncompletetarget, gameObject
                   ));

    }
    void ChangleScaleTime(float percent)
    {
        textTimeSecond.gameObject.transform.localScale = new Vector3(
            1 + (0.5f - Mathf.Abs(percent - 0.5f)) * (2f - 1),
             1 + (0.5f - Mathf.Abs(percent - 0.5f)) * (2f - 1),
             1
            );
    }
    void ChangleScaleScore(float percent)
    {
        txtScore.gameObject.transform.localScale = new Vector3(
            1 + (0.5f - Mathf.Abs(percent - 0.5f)) * (2f - 1),
             1 + (0.5f - Mathf.Abs(percent - 0.5f)) * (2f - 1),
             1
            );
    }
    int intPause = 0;
    public GameObject buttonPause;
    public void Pause()
    {
        intPause++;
        if (intPause % 2 != 0)
        {
            gameController.activeTimeHelp = false;
            buttonPause.GetComponentInChildren<Text>().text = "Resume";
            isPause = true;
            pause.SetActive(true);
            
        }
        else
        {
            gameController.activeTimeHelp = true;
            buttonPause.GetComponentInChildren<Text>().text = "Pause";
            isPause = false;
            pause.SetActive(false);
        }
    }
}
