using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BannerAnimation : MonoBehaviour {

    public Vector2 endAnimationPosition;

    public float fadeInDuration;
    public Animator myAnimator;
    public Animator childAnimator;
    private void Start()
    {
        myAnimator = transform.GetComponent<Animator>();
        childAnimator = transform.GetChild(0).GetComponent<Animator>();
    }
    public void FadeInAnim()
    {
        myAnimator.SetBool("blink", true);
        gameObject.GetComponent<RectTransform>().DOAnchorPos(endAnimationPosition, fadeInDuration).OnComplete(() =>
        {
            childAnimator.SetBool("kedip", true);
            StartCoroutine(enumertor());
        });
    }
    IEnumerator enumertor()
    {
        yield return new WaitForSeconds(3);
        stopChildAnimation();
    }
    public void stopChildAnimation()
    {
        childAnimator.SetBool("kedip", false);
        myAnimator.SetBool("light_on", true);
    }
}
