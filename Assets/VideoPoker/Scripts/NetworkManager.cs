using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour {
    public string URL_API;
    public static NetworkManager _instance;
    public static NetworkManager Instance { get { return _instance; } }

    public delegate void OnLogin(User user);
    public static OnLogin onLogin;

    private void Awake()
    {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public IEnumerator Get(string url) {
        using(UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError) {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone) {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    Debug.Log(jsonResult);
                }
            }
        }
    }

    public void Login(string email,string password) {
        Debug.Log("test");
        LevelManager._instance.LoadingPanelShow();
        var usersRoute = URL_API+"users_login&email="+email+"&password="+password;
        RestClient.Get<User>(usersRoute).Then(response => {

            //cek apakah sukes login = 0 gagal, 1 = success
            if (response.success == "1")
            {
                //ambil data
                PlayerPrefs.SetString("token", response.token);
                onLogin(response);
                GameManager._instance.LoginProcess();
                
            }
            else
            {
                Debug.Log("login Error " + response);
                LevelManager._instance.LoadingPanelClose();
                LevelManager._instance.LoginPanelShake();
            }
        });
    }
    //token login/ autologin tanpa user&pass
    public void LoginToken(string token)
    {
        Debug.Log("Login Token");
        Debug.Log("token : " + token);
        var usersRoute = URL_API + "users_login_token&token=" + token;
        RestClient.Get<User>(usersRoute).Then(response => {
            Debug.Log("apakah terpanggil");
            Debug.Log("Response success "+response.success);
            //cek apakah sukes login = 0 gagal, 1 = success
            if (response.success == "1")
            {
                //ambil data
                Debug.Log("username : "+response.name);
                onLogin(response);
                GameManager._instance.LoginProcess();
                
            }
            else
            {
                Debug.Log("login Error");
                LevelManager._instance.LoginPanelShow();
                LevelManager._instance.LoadingPanelClose();
                LevelManager._instance.LoginPanelShake();
            }
        });
        Debug.Log("dia lewati");
    }
    //register new user
    public void RegisterUser(string username,string email,string password) {
        LevelManager._instance.LoadingPanelShow();
        Debug.Log("Register");
        var usersRoute = URL_API + "user_register&name="+username+"&email="+email+"&password="+password;
        RestClient.Get<User>(usersRoute).Then(response => {

            //cek apakah sukes login = 0 gagal, 1 = success
            if (response.success == "1")
            {
                //ambil data
                PlayerPrefs.SetString("token", response.token);
                GameManager._instance.LoginProcess();
                onLogin(response);
            }
            else
            {
                Debug.Log("login Error");
                LevelManager._instance.LoadingPanelClose();
                LevelManager._instance.RegisterPanelShake();
            }
        });
    }
    //Update koin di server
    public void UpdateCoin(string coin)
    {
        
        var usersRoute = URL_API + "user_coin_update&token=" + DataManager.Instance.dataUser.token+"&coin="+coin;
        RestClient.Get<User>(usersRoute).Then(response => {

            //cek apakah sukses login = 0 gagal, 1 = success
            if (response.success == "1")
            {

                Debug.Log("coin_update");
                Debug.Log("token login : coin : "+DataManager.Instance.dataUser.token);
            }
            else
            {
                Debug.Log("coin update error Error");
            }
        });
        
    }
    public void DailyCoinReward(DailyReward.RewardClick rewardClick)
    {
        var usersRoute = URL_API + "daily_coin_reward";
        int dailyCoinReward = 0;
        RestClient.Get<DailyCoin>(usersRoute).Then(response => {

            //cek apakah sukses login = 0 gagal, 1 = success
            if (response.success == "1")
            {
                rewardClick(response.daily_coin_reward);
                // Debug.Log("Daily coin : "+response.daily_coin_reward);
                dailyCoinReward = response.daily_coin_reward;

            }
            else
            {
                rewardClick(DataManager.Instance.DailyCoin);
                Debug.Log("daily coin Error");
                Debug.Log("response :" + response);
            }
        });



    }

    public class User
    {
        public int user_id;
        public string token;
        public string name;
        public string email;
        public string success;
        public string coin;
    }
    public class DailyCoin
    {
        public int daily_coin_reward;
        public string success;
    }
}
