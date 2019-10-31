using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUI : MonoBehaviour {

    public InputField txtName;
    public InputField txtEmail;
    public InputField txtPassword;
    public void ButtonRegister() {
        if (txtName.text == "" || txtEmail.text == "" || txtPassword.text == "")
        {
          
            LevelManager._instance.RegisterPanelShake();
        }
        else {
            NetworkManager._instance.RegisterUser(txtName.text, txtEmail.text, txtPassword.text);
        }


        
    }
}
