using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    
    public static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public delegate void OnUpdateUsername(string username);
    public static OnUpdateUsername onUpdateUsername;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
    public void Start()
    {
        
        GoHome();
    }
    public void GoHome() {

        if (PlayerPrefs.HasKey("token") == false)
        {
            LevelManager.Instance.LoginPanelShow();
            Debug.Log("tidak Login");
        }
        else
        {
            //loading ambil data ke server dan masukkan ke game
            NetworkManager._instance.LoginToken(PlayerPrefs.GetString("token"));
        }
    }
    public void Register() {
        LevelManager._instance.LoginPanelClose();
        LevelManager._instance.RegisterPanelShow();
    }
    public void Login() {
        LevelManager._instance.RegisterPanelClose();
        LevelManager._instance.LoginPanelShow();
    }
    public void LoginProcess() {
        Debug.Log("Login Prosesssss");
        LevelManager._instance.LoadingPanelClose();
        LevelManager._instance.RegisterPanelClose();
        LevelManager._instance.LoginPanelClose();
        StartCoroutine(LevelManager._instance.InLoadScene());
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HomeScene") {
            Debug.Log("on Update Username : "+DataManager.Instance.dataUser.name);
            if (onUpdateUsername != null) {
                onUpdateUsername(DataManager.Instance.dataUser.name);
            }
            
            StartCoroutine(LevelManager._instance.InLoadScene());
        }
    }


}
