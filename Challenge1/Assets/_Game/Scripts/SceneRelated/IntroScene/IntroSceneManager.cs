using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public Image logo;

    // Start is called before the first frame update
    void Start()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(logo.DOFade(1, 1.2f));
        mySequence.Append(logo.DOFade(0, 1.2f));
        mySequence.Play().OnComplete(() => SceneManager.LoadScene(1));
    }

}
