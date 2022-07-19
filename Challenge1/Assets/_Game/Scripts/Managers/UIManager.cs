using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text _scoreText;
    public Text _timeText;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "0";
        _timeText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isInPause || GameManager.Instance.isGameOver)
            return;

        if (GameManager.Instance.time > 0)
        {
            GameManager.Instance.time -= Time.deltaTime;
            DisplayTime(GameManager.Instance.time);
        }
        else if (!GameManager.Instance.isGameOver)
        {
            GameManager.Instance.GameOver();
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Scored(int score)
    {
        _scoreText.text = score.ToString();
    }


    public void Rotate(bool isLeft)
    {
        GameManager.Instance.RotateCube(isLeft);
    }
}
