package com.example.kinocentar;

import androidx.lifecycle.ViewModelProviders;

import android.content.Intent;
import android.os.Build;
import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.example.kinocentar.helper.MySession;
import com.example.kinocentar.viewmodels.LoginViewModel;

public class LoginActivity extends AppCompatActivity {

    private EditText usernameEditText;
    private EditText passwordEditText;
    private LoginViewModel loginViewModel;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        usernameEditText = findViewById(R.id.ET_UserName);
        passwordEditText = findViewById(R.id.ET_Password);

        final Button loginButton = findViewById(R.id.BTN_Prijava);
        loginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnLoginClick();
            }
        });
    }

    private void do_btnLoginClick() {
        // TODO: Login
        String strUsername = usernameEditText.getText().toString();
        String strPassword = passwordEditText.getText().toString();
        String deviceInfo = android.os.Build.DEVICE+" | " +  android.os.Build.VERSION.RELEASE + " | " + android.os.Build.PRODUCT + " | " + Build.MODEL;

        loginViewModel = new LoginViewModel(strUsername, strPassword, deviceInfo);

        MySession.setLoginData(loginViewModel);
        startActivity(new Intent(this, MainActivity.class));
    }
}