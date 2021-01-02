package com.example.kinocentar;

import android.content.Intent;
import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.kinocentar.helper.MyApiRequest;
import com.example.kinocentar.helper.MyCryption;
import com.example.kinocentar.helper.MyRunnable;
import com.example.kinocentar.helper.MySession;
import com.example.kinocentar.viewmodels.LoginViewModel;
import com.example.kinocentar.viewmodels.UserViewModel;
import com.google.android.material.snackbar.Snackbar;

public class LoginActivity extends AppCompatActivity {

    private EditText usernameEditText;
    private EditText passwordEditText;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        usernameEditText = findViewById(R.id.ET_UserName);
        passwordEditText = findViewById(R.id.ET_Password);

        TextView registerEditText = findViewById(R.id.TV_Register);
        registerEditText.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent i = new Intent(getApplicationContext(), RegisterActivity.class);
                startActivity(i);
            }
        });

        Button loginButton = findViewById(R.id.BTN_Prijava);
        loginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnLoginClick();
            }
        });
    }

    private void do_btnLoginClick()
    {
        final String strUsername = usernameEditText.getText().toString();
        final String strPassword = passwordEditText.getText().toString();
        LoginViewModel model = new LoginViewModel(strUsername, strPassword);

        if (model.isUserNameValid() && model.isPasswordValid()) {

            MyApiRequest.post(this, "Korisnici/CheckLogin", model, new MyRunnable<UserViewModel>() {
                @Override
                public void run(UserViewModel user) {
                    checkLogin(user, strUsername, strPassword);
                }
            });
        }
        else {
            writeMessage("Pogrešan username/password");
        }
    }

    private void checkLogin(UserViewModel user, String strUsername, String strPassword) {
        if (user == null)
        {
            writeMessage("Pogrešan username/password");
        }
        else
        {
            user.token = MyCryption.GenerateBase64(strUsername + ":" + strPassword);
            MySession.setUserData(user);
            startActivity(new Intent(this, MainActivity.class));
        }
    }

    private void writeMessage(String text) {
        View parentLayout = findViewById(android.R.id.content);
        Snackbar.make(parentLayout, text, Snackbar.LENGTH_LONG).show();
    }
}