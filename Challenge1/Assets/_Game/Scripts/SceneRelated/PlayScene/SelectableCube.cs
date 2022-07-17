using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableCube : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] spriteRenderers;

    public string spriteName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprite(Sprite s)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = s;
        }
        spriteName = s.name;
    }

    private void OnMouseOver()
    {
        GameManager.Instance.MouseOverCube(this);
    }

    private void OnMouseUp()
    {
        GameManager.Instance.MouseSelected(this);
    }
}
