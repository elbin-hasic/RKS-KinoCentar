package com.example.kinocentar.viewmodels;

import androidx.lifecycle.ViewModel;

import android.util.Patterns;

public class LoginViewModel extends ViewModel {

    public String UserName;
    public String Password;
    public String DeviceInfo;

    public LoginViewModel(String username, String password, String deviceInfo)
    {
        this.UserName = username;
        this.Password = password;
        this.DeviceInfo = deviceInfo;
    }

    // A placeholder username validation check
    private boolean isUserNameValid(String username) {
        if (username == null) {
            return false;
        }
        if (username.contains("@")) {
            return Patterns.EMAIL_ADDRESS.matcher(username).matches();
        } else {
            return !username.trim().isEmpty();
        }
    }

    // A placeholder password validation check
    private boolean isPasswordValid(String password) {
        return password != null && password.trim().length() > 5;
    }
}