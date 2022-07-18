using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public SaveManager SaveManager { get; private set; }
    public UIManager UIManager { get; private set; }


    public GameObject PrefabSelectableCube;
    public GameObject CubeHolder;

    [SerializeField] public Queue<SelectableCube> selectedCubes = new Queue<SelectableCube>();
    [SerializeField] public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        SaveManager = GetComponent<SaveManager>();
        UIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        if(cube.cube.transform == selectedCubes.Peek().cube.transform)
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
        if(CubeHolder!= null)
        {
            CubeHolder.transform.DOBlendableRotateBy(new Vector3(0, isLeft ? 90 : -90, 0), 1f);
        }
    }
    
}
