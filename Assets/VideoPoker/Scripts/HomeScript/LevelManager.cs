using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using DG.Tweening;
#if UNITY_ANDROID
//using GooglePlayGames;
#endif

public class LevelManager : MonoBehaviour
{
	public static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
    Animator animconvert;
	public GameObject bgOverlay;
    public RectTransform LoginPanel;
    public RectTransform RegisterPanel;
    public RectTransform LoadingPanel;
    private void Awake()
    {
        _instance = this;
        animconvert = GetComponentInChildren<Animator>();
    }
    void Start () 
	{
		

        animconvert.SetTrigger("First");
        bgOverlay.SetActive(true);
        //

        //StartCoroutine (InLoadScene ());

    }
	void Update()
	{

	}

	//-------------AUTO LOAD SCENE WHEN FIRST SCENCE------------------
	public IEnumerator InLoadScene()
	{
		animconvert.SetTrigger ("First");
		bgOverlay.SetActive(true);
		SoundController.Sound.Out ();
		yield return new WaitForSeconds (1f);
		animconvert.SetTrigger ("Out");
		SoundController.Sound.In ();
		MusicController.Music.BG_Home ();
		bgOverlay.SetActive(false);
	}

    public void RegisterPanelShow() {
        RegisterPanel.DOAnchorPos(new Vector2(0, 0), 0.3f);
    }
    public void RegisterPanelClose() {
        RegisterPanel.DOAnchorPos(new Vector2(0, -755), 0.3f);
        
    }
    public void RegisterPanelShake() {
        RegisterPanel.DOShakeAnchorPos(0.5f, 20, 10, 1);
    }
    public void LoginPanelShow() {
        LoginPanel.DOAnchorPos(new Vector2(0, 0), 0.3f);
    }
    public void LoginPanelClose() {
        LoginPanel.DOAnchorPos(new Vector2(0, 743), 0.3f);
    }
    public void LoginPanelShake() {
        LoginPanel.DOShakeAnchorPos(0.5f,20,10,1);
    }
    public void LoadingPanelShow() {
        LoadingPanel.gameObject.SetActive(true);
    }
    public void LoadingPanelClose() {
        LoadingPanel.gameObject.SetActive(false);
    }
	public void VideoPokerScene()
	{
		MusicController.Music.BG_VideoPoker ();
		SoundController.Sound.ClickBtn ();
		SceneManager.LoadScene ("JackOrBetter");
		Debug.Log ("VideoPoker Scene");
	}
	public void BlackJackScene()
	{
		MusicController.Music.BG_BlackJack ();
		SoundController.Sound.ClickBtn ();
		SceneManager.LoadScene ("Blackjack");
	}
	public void SlotMachineScene()
	{
		MusicController.Music.BG_SlotMachine ();
		SoundController.Sound.ClickBtn ();
		SceneManager.LoadScene ("SlotMachine");
		Debug.Log ("SlotMachine Scene");
	}

	public void HomeScene()
	{
		MusicController.Music.BG_Home ();
		SoundController.Sound.ClickBtn ();
		SceneManager.LoadScene ("HomeScene");
	}


}
