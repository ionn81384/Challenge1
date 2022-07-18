using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    List<GameObject> cubes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(4,4,4));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spawn(int coulumn, int rows, int depth)
    {
        if ((coulumn * rows * depth) % 2 != 0)
        {
            // TODO do somethig but there is no match on all cubes
            Debug.LogError("Invalid rows and columns!");
        }

        // Get prefab
        var prefab = GameManager.Instance.PrefabSelectableCube;

        var sprites = new Sprite[(coulumn * rows * depth) / 2];
        Array.Copy(GameManager.Instance.sprites, sprites, (coulumn * rows * depth) / 2);
        var cubeSymbolSprites = new Sprite[sprites.Length*2];
        Array.Copy(sprites, cubeSymbolSprites, sprites.Length);
        Array.Copy(sprites, 0, cubeSymbolSprites, sprites.Length, sprites.Length);
        Shuffle(cubeSymbolSprites);

        var currentSprite = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < coulumn; j++)
            {
                for (int k = 0; k < depth; k++)
                {
                    var newObject = Instantiate(prefab, new Vector3(-1.5f+i, j, -1.5f+k), Quaternion.identity);
                    newObject.transform.SetParent(gameObject.transform, false);
                    var script = newObject.GetComponent<SelectableCube>();
                    script.SetSprite(cubeSymbolSprites[currentSprite]);
                    currentSprite++;
                    cubes.Add(newObject);
                }
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }

    public IEnumerator Respawn()
    {
        // TODO if the player get's stuck reposition cubes
        foreach (var cube in cubes)
        {
            if (cube.active)
            {
                // set diferent positions
                yield return null;
            }
        }
    }

    public void SpawnFormat(bool[][][] cube)
    {
        // TODO defined by rows
    }

    //for shuffle number from array
    void Shuffle(Sprite[] array)
    {
        System.Random _random = new System.Random();
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(1, n);
            Sprite t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }
}
