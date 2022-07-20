using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text _scoreText;
    public Text _timeText;
    public Text _multiplierText;

    public GameObject _canvasPlay;
    public GameObject _canvasPause;
    public GameObject _canvasMenu;
    public GameObject _canvasWin;
    public GameObject _canvasLose;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "0";
        _timeText.text = "0";
        GameManager.Instance.isInPause = true;
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

        if (GameManager.Instance.time <= 0)
        {
            GameManager.Instance.GameOver();
        }

    }

    public void ChangedMultiplier(int value)
    {
        _multiplierText.text = "X "+value.ToString();
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
        if (GameManager.Instance.isInPause || GameManager.Instance.isGameOver)
            return;
        GameManager.Instance.RotateCube(isLeft);
    }

    public void Pause_Click()
    {
        GameManager.Instance.isInPause = !GameManager.Instance.isInPause;
        _canvasPause.SetActive(!IsActiveObject(_canvasPause));
        _canvasPlay.SetActive(!IsActiveObject(_canvasPlay));
    }

    public void Play_Click()
    {
        GameManager.Instance.isInPause = false;
        _canvasMenu.SetActive(!IsActiveObject(_canvasMenu));
        _canvasPlay.SetActive(true);
    }

    public void SoundOn_Click()
    {
        // so it's saved later on
        GameManager.Instance.SaveManager._gameData.IsSoundOn = !GameManager.Instance.SaveManager._gameData.IsSoundOn;
    }

    public void Replay_Click()
    {
        _canvasPlay.SetActive(true);
        _canvasPause.SetActive(false);
        _canvasMenu.SetActive(false);
        _canvasWin.SetActive(false);
        _canvasLose.SetActive(false);
        GameManager.Instance.Restart();
    }

    public void ShowWinOrLose(bool isWin = true)
    {
        if (isWin)
        {
            _canvasWin.SetActive(true);
        }
        else
        {
            _canvasLose.SetActive(true);
        }
    }

    public bool IsActiveObject(GameObject objectToCheck)
    {
        if (objectToCheck == null)
            return false;

        return objectToCheck.activeSelf;
    }
}
