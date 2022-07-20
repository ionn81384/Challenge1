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
    public Renderer material;

    private List<Vector3> Sides = new List<Vector3>() {
            new Vector3(0.5f,0,0),
            new Vector3(-0.5f,0,0),
            new Vector3(0,0,0.5f),
            new Vector3(0,0,-0.5f),
        };

    // Start is called before the first frame update
    void Start()
    {
        cube = this.transform;
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

    private void OnMouseDown()
    {
        GameManager.Instance.MouseSelected(this);
    }

    private void OnMouseDrag()
    {
        var rotationSpeed = 3f;
        // for mobile has to be different
        float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.parent.transform.Rotate(Vector3.down, XaxisRotation);
    }

    private IEnumerator ScaleObject(bool isEnter)
    {
        if (isEnter)
        {
            material.material.DOColor(Color.green, 1);
        }
        else
        {
            material.material.DOColor(Color.white, 1);
        }

        yield return null;
    }

    public bool IsSelectable()
    {
        var Reach = 0.1f;

        var hit = new RaycastHit();
        var freeFaces = 0;
        for (int i = 0; i < Sides.Count; i++)
        {
            var isHit = Physics.Raycast(transform.position + Sides[i], Sides[i] * Reach, out hit);
            if (!(isHit && hit.transform.tag == "cube"))
            {
                freeFaces++;
            }

            if (freeFaces >= 2)
            {
                return true;
            }
        }
        return false;
    }
}
