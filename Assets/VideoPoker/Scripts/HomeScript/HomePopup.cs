using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Holoville.HOTween;
using UnityEngine.SceneManagement;

public class HomePopup : MonoBehaviour
{
	public Text CoinTex;
	public Text CoinTex1;
    public Text username;
	public GameObject OptionsObj;
	public GameObject ExitGameObj;
	public EaseType easeTypeGoTo = EaseType.EaseInOutBack;
	public Transform target;
	public GameObject overlay;
	public GameObject SpinObj;
   

	Vector3 oriPos = Vector3.zero;
	bool isOpen = false;
    public void Awake()
    {
        NetworkManager.onLogin += UpdateUserData;
        GameManager.onUpdateUsername += SetUsername;
    }
    void Update()
	{
		//CoinTex.text = "" + DataManager.Instance.Coins;
	}
	public void Move(Transform tr) 
	{
		
		if (!isOpen)
		{
			if (tr.name == "Quit Game") 
			{
				ExitGameObj.SetActive (true);
				OptionsObj.SetActive (false);
				//ads
				GameObject.FindObjectOfType<AdManagerUnity>().ShowAd("video");
//				AdmobBannerController.Instance.ShowInterstitial ();
			}
			if (tr.name == "Setting") 
			{
				ExitGameObj.SetActive (false);
				OptionsObj.SetActive (true);
			}
			if (tr.name == "IAP") 
			{
				ExitGameObj.SetActive (false);
				OptionsObj.SetActive (false);
			}
			overlay.SetActive(true);
			oriPos = tr.position;
			TweenParms parms = new TweenParms().Prop("position", target.position).Ease(easeTypeGoTo);
			HOTween.To(tr, .7f, parms);
			isOpen = true;
			SoundController.Sound.PopupShow ();
		}
		else 
		{
			SoundController.Sound.CloseBtn ();
			overlay.SetActive(false);
			TweenParms parms = new TweenParms().Prop("position", oriPos).Ease(easeTypeGoTo).OnComplete(OnComplete);
			HOTween.To(tr, .7f, parms);
			ExitGameObj.SetActive (false);
			OptionsObj.SetActive (false);
		}
	}

	void OnComplete()
	{
		isOpen = false;
	}
	public void SpinToHomeScene()
	{
		SoundController.Sound.CloseBtn ();
		SceneManager.LoadScene ("HomeScene");
	}
    public void UpdateUserData(NetworkManager.User user) {
        username.text = user.name;
        CoinTex.text = user.coin;
    }
    public void SetUsername(string name) {
        username.text = DataManager.Instance.dataUser.name;
        CoinTex.text = DataManager.Instance.Coins.ToString();
    }
    private void OnDestroy()
    {
        NetworkManager.onLogin -= UpdateUserData;
        GameManager.onUpdateUsername -= SetUsername;
    }
}
