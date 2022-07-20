using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public List<GameObject> cubes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(4,4,4));
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

        // TODO change to use 6 sprites 
        // Get sprites 
        var sprites = new Sprite[(coulumn * rows * depth) / 4];
        Array.Copy(GameManager.Instance.sprites, sprites, (coulumn * rows * depth) / 4);
        var SpriteList = new List<Sprite>();
        for (int i = 0; i < 4; i++)
        {
            SpriteList.AddRange(sprites);
        }
        yield return null;

        var shuffledSprites = SpriteList.ToArray();
        Shuffle(shuffledSprites);
        yield return null;

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
                    script.SetSprite(shuffledSprites[currentSprite]);
                    currentSprite++;
                    cubes.Add(newObject);
                    if(currentSprite >= shuffledSprites.Length)
                        currentSprite = 0;
                }
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }

    public IEnumerator Respawn()
    {
        // TODO if the player gets stuck reposition cubes
        foreach (var cube in cubes)
        {
            if (cube.activeSelf)
            {
                // set diferent positions

                yield return null;
            }
        }
    }

    //for shuffle sprite from array
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
