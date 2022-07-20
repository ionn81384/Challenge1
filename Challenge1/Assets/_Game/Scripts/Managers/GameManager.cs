using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public SaveManager SaveManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public CubeSpawner cubeSpawner { get; private set; }


    public GameObject PrefabSelectableCube;
    public GameObject CubeHolder;

    [SerializeField] public Queue<SelectableCube> selectedCubes = new Queue<SelectableCube>();
    [SerializeField] public Sprite[] sprites;

    public bool isInPause = false;
    public bool isGameOver = false;
    public float time = 300;
    public int currentScore = 0;

    public Coroutine multiplierRoutine;
    public int multiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        SaveManager = GetComponent<SaveManager>();
        UIManager = FindObjectOfType<UIManager>();
        cubeSpawner = FindObjectOfType<CubeSpawner>();
    }

    public void GameOver(bool playerWon = false)
    {
        isGameOver = true;
        UIManager.ShowWinOrLose(playerWon);
    }

    public void MouseOverCube(SelectableCube cube)
    {
        // TODO add highlight
    }

    public bool MouseSelected(SelectableCube cube)
    {
        if (!cube.IsSelectable())
        {
            return false;
        }

        if (selectedCubes.Count == 0)
        {
            selectedCubes.Enqueue(cube);
            return false;
        }
        else if (selectedCubes.Count == 2)
        {
            selectedCubes.Dequeue();
        }

        if (cube.cube.transform == selectedCubes.Peek().cube.transform)
        {
            return false;
        }

        // 2 cubes of same
        if (cube.spriteName == selectedCubes.Peek().spriteName)
        {
            // is it blocked?
            cube.IsSelectable();
            if (cube.IsSelectable())
            {
                cube.cube.transform.gameObject.SetActive(false);
                selectedCubes.Peek().cube.transform.gameObject.SetActive(false);
                selectedCubes.Clear();
                AddScore();
                IsGameOver();
            }
        }
        else
        {
            selectedCubes.Enqueue(cube);
        }

        return true;
    }

    public void RotateCube(bool isLeft = false)
    {
        if (CubeHolder != null)
        {
            CubeHolder.transform.DOBlendableRotateBy(new Vector3(0, isLeft ? 90 : -90, 0), 1f);
        }
    }

    private void AddScore()
    {
        currentScore += 100 * multiplier;
        UIManager.Instance.Scored(currentScore);
        if (multiplierRoutine != null)
            StopCoroutine(multiplierRoutine);
        multiplierRoutine = StartCoroutine(Multiplier(true));
    }

    private void IsGameOver()
    {
        // one is active then is still on
        foreach (var cube in cubeSpawner.cubes)
        {
            if (cube.activeSelf)
                return;
        }
        GameOver(true);
    }

    public void Restart()
    {
        isInPause = false;
        isGameOver = false;
        time = 300;
        currentScore = 0;
        if (multiplierRoutine != null)
            StopCoroutine(multiplierRoutine);
        foreach (var cube in cubeSpawner.cubes)
        {
            Object.Destroy(cube);
        }
        cubeSpawner.cubes.Clear();
        StartCoroutine(cubeSpawner.Spawn(4, 4, 4));
    }

    IEnumerator Multiplier(bool addValue)
    {
        if (multiplier >= 1 && addValue)
        {
            multiplier++;
        }
        UIManager.Instance.ChangedMultiplier(multiplier);
        yield return new WaitForSeconds(3);

        if (multiplier > 1)
        {
            multiplier--;
            UIManager.Instance.ChangedMultiplier(multiplier);
            // TODO this is nested coroutine... refactor
            multiplierRoutine = StartCoroutine(Multiplier(false));
        }
    }
}
