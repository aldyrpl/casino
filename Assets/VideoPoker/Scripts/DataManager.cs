using UnityEngine;
using System;
using System.Collections;

public class DataManager : MonoBehaviour 
{

	public static DataManager Instance;

	public int Coins
	{ 
		get { return _coins; }
		private set { _coins = value; }
	}
	public int FreeAdNumber
	{ 
		get { return _freeadnum; }
		private set { _freeadnum = value; }
	}
    public NetworkManager.User dataUser { get; set; }

    public static event Action<int> CoinsUpdated = delegate {};
	public static event Action<int> FreeAdNumberUpdated = delegate {};
	[SerializeField]
	int initialCoins = 1000;
	// Show the current coins value in editor for easy testing
	[SerializeField]
	int _coins;
	// key name to store high score in PlayerPrefs
	//	const string COINSGAMESTRING = "coins";
	[SerializeField]
	int initialFreeAdNumber = 10;
	[SerializeField]
	int _freeadnum;
	void Awake()
	{
//		PlayerPrefs.DeleteAll ();
		if (Instance)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start()
	{
		Reset();
        NetworkManager.onLogin += OnLogin;
	}

	public void Reset()
	{
		// Initialize coins
		Coins = PlayerPrefs.GetInt("coins", initialCoins);
	}

	public void AddCoins(int amount)
	{
		Coins += amount;
		// Store new coin value
		PlayerPrefs.SetInt("coins", Coins);
		// Fire event
		CoinsUpdated(Coins);
        UpdateCoinToServer();

    }

	public void RemoveCoins(int amount)
	{
		Coins -= amount;
		// Store new coin value
		PlayerPrefs.SetInt("coins", Coins);
		// Fire event
		CoinsUpdated(Coins);
        UpdateCoinToServer();

    }
    public void OnLogin(NetworkManager.User userInfo) {
        Coins = int.Parse(userInfo.coin);
        CoinsUpdated(Coins);
        dataUser = userInfo;
    }
    public void UpdateCoinToServer() {
        NetworkManager._instance.UpdateCoin(Coins.ToString());
    }
	//----------------------
}
