package com.example.kinocentar.viewmodels;

import android.util.Patterns;

public class LoginViewModel {

    public String userName;
    public String password;

    public LoginViewModel(String userName, String password)
    {
        this.userName = userName;
        this.password = password;
    }

    // A placeholder username validation check
    public boolean isUserNameValid() {
        if (this.userName == null) {
            return false;
        }
        if (this.userName.contains("@")) {
            return Patterns.EMAIL_ADDRESS.matcher(this.userName).matches();
        } else {
            return !this.userName.trim().isEmpty();
        }
    }

    // A placeholder password validation check
    public boolean isPasswordValid() {
        return this.password != null && this.password.trim().length() > 5;
    }
}