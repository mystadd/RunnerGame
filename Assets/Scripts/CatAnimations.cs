using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimations : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayFirstAnimation()
    {
        StartCoroutine(WaitFirstAnim());
    }

    public void PlayTwoAnimation()
    {  
        StartCoroutine(WaitTwoAnim());
    }

    public void PlayZeroAnimation()
    {
        StartCoroutine(WaitZeroAnim());
    }

    private IEnumerator WaitFirstAnim()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetInteger("state", 1);
    }

    private IEnumerator WaitTwoAnim()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetInteger("state", 2);
    }

    private IEnumerator WaitZeroAnim()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetInteger("state", 0);
    }
}
