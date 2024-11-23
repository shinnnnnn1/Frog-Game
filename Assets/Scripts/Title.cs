using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Title : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image fadeImage;

    [Header("Frog Animation")]
    [SerializeField] PlayerCtrl frog;
    [SerializeField] Transform[] movePos;
    [SerializeField] Rigidbody[] obj;
    bool objInstance;
    bool objCube;

    void Start()
    {
        StartCoroutine(MovingFrog());

        fadeImage.DOFade(0f, 5f).SetEase(Ease.InCubic).SetDelay(1f);

    }

    IEnumerator MovingFrog()
    {
        yield return new WaitForSeconds(1f);

        frog.transform.position = movePos[0].position;
        frog.transform.localEulerAngles = movePos[0].localEulerAngles;
        frog.anim.SetFloat("Move", 1f);
        frog.transform.DOMove(movePos[1].position, 2f).SetEase(Ease.Linear);
        SetObject();
        frog.canMove = false;

        yield return new WaitForSeconds(2f);
        frog.anim.SetFloat("Move", 0f);

        yield return new WaitForSeconds(1f);
        StartCoroutine(frog.Action());

        yield return new WaitForSeconds(1f);
        frog.canMove = false;
        frog.anim.SetFloat("Move", 1f);
        frog.transform.DOMove(movePos[2].position, 2f).SetEase(Ease.Linear);
        frog.transform.DORotate(movePos[2].localEulerAngles, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2f);
        StartCoroutine(frog.Action());

        yield return new WaitForSeconds(1f);
        frog.canMove = false;
        frog.transform.position = movePos[0].position;
        frog.transform.localEulerAngles = movePos[0].localEulerAngles;

        StartCoroutine(MovingFrog());
    }

    void SetObject()
    {
        if(!objInstance)
        {
            objInstance = !objInstance;
            return;
        }
        objCube = !objCube;

        int i = objCube ? 0 : 1;
        obj[i].velocity = Vector3.zero;
        obj[i].transform.position = movePos[3].position;
        obj[i].transform.localEulerAngles = Vector3.zero;
        obj[i].velocity = Vector3.zero;

        objInstance = false;
    }
}
