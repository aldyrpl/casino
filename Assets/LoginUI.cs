using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour {

    public InputField txtEmail;
    public InputField txtPassword;

    public void ButtonLogin() {
        NetworkManager._instance.Login(txtEmail.text,txtPassword.text);
    }

}

