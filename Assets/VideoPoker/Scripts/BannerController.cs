using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CSFramework;

public class BannerController : MonoBehaviour {

    // Use this for initialization
    public BannerAnimation[] childBanner;
    public CustomSlot slot;
	void Start () {
        slot.callbacks.onActivated.AddListener(OnActivated);
    }
    public void OnActivated() {
        foreach (var banner in childBanner)
        {
            banner.FadeInAnim();
        }
    }
}
