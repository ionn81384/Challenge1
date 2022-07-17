using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public SaveManager SaveManager { get; private set; }
    public UIManager UIManager { get; private set; }


    public GameObject PrefabSelectableCube;


    public Queue<SelectableCube> selectedCubes;
    [SerializeField]
    public Sprite[] sprites;

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

    public void MouseSelected(SelectableCube cube)
    {
        if (selectedCubes.Count == 0)
        {
            selectedCubes.Enqueue(cube);
            return;
        }
        else if (selectedCubes.Count == 2)
        {
            selectedCubes.Dequeue();
        }

        // 2 cubes of same
        if (cube.spriteName == selectedCubes.Peek().spriteName)
        {
            // is it blocked?
            var match = false;
            if (match)
            {
                selectedCubes.Clear();
            }
        }
        else
        {
            selectedCubes.Enqueue(cube);
        }
    }
}
