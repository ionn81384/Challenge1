using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectableCube : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] spriteRenderers;

    public string spriteName;
    public Transform cube;
    private Color[] c = new Color[] {Color.yellow,Color.yellow, Color.yellow, Color.yellow};

    // Start is called before the first frame update
    void Start()
    {
        cube = this.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //var Reach = 0.1f;
        //var sides = new List<Vector3>() {
        //    new Vector3(0.5f,0,0),
        //    new Vector3(-0.5f,0,0),
        //    new Vector3(0,0,0.5f),
        //    new Vector3(0,0,-0.5f),
        //};
        //var hit = new RaycastHit();
        //var freeFaces = 0;
        //for (int i = 0; i < 4; i++)
        //{
        //    Debug.DrawRay(transform.position + sides[i], sides[i] * Reach, c[i]);
        //    var isHit = Physics.Raycast(transform.position + sides[i], sides[i], out hit, Reach);
        //    if (!(isHit && hit.transform.tag == "cube"))
        //    {
        //        c[i] = Color.red;
        //    }
        //    else
        //    {
        //        c[i] = Color.white;
        //    }
        //}
    }

    public void SetSprite(Sprite s)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = s;
        }
        spriteName = s.name;
        this.transform.tag = "cube";
    }

    private void OnMouseOver()
    {
        GameManager.Instance.MouseOverCube(this);
    }

    private void OnMouseExit()
    {
        StartCoroutine(ScaleObject(false));
    }

    private void OnMouseEnter()
    {
        StartCoroutine(ScaleObject(true));
    }

    private void OnMouseUp()
    {
        GameManager.Instance.MouseSelected(this);
    }

    private IEnumerator ScaleObject(bool isEnter)
    {
        //if (isEnter)
        //{
        //    this.transform.DOScale(1.1f, 0.5f);
        //}
        //else
        //{
        //    this.transform.DOScale(1f, 0.5f);
        //}

        yield return null;
    }

    public bool IsSelectable()
    {
        var Reach = 0.1f;
        var sides = new List<Vector3>() {
            new Vector3(0.5f,0,0),
            new Vector3(-0.5f,0,0),
            new Vector3(0,0,0.5f),
            new Vector3(0,0,-0.5f),
        };
        var hit = new RaycastHit();
        var freeFaces = 0;
        for (int i = 0; i < sides.Count; i++)
        {
            var isHit = Physics.Raycast(transform.position + sides[i], sides[i]*Reach, out hit);
            if (!(isHit && hit.transform.tag == "cube"))
            {
                freeFaces++;
            }

            if (freeFaces >= 2)
            {
                Debug.Log("is select");
                return true;
            }
        }
        Debug.Log("Is not selectable");
        return false;
    }
}
