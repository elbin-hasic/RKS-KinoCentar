package com.example.kinocentar;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.kinocentar.helper.MySession;
import com.example.kinocentar.viewmodels.LoginViewModel;

public class RegisterActivity extends AppCompatActivity {

    private EditText usernameEditText;
    private EditText passwordEditText;
    private EditText passwordConfirmEditText;
    private TextView loginEditText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        usernameEditText = findViewById(R.id.ET_UserName);
        passwordEditText = findViewById(R.id.ET_Password);
        passwordConfirmEditText = findViewById(R.id.ET_Password_Confirm);

        loginEditText = findViewById(R.id.TV_Login);
        loginEditText.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(getApplicationContext(), LoginActivity.class);
                startActivity(i);
            }
        });

        final Button registerButton = findViewById(R.id.BTN_Register);
        registerButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnRegisterClick();
            }
        });
    }

    private void do_btnRegisterClick() {
        // TODO: Register
        String strUsername = usernameEditText.getText().toString();
        String strPassword = passwordEditText.getText().toString();
        String strPasswordConfirm = passwordConfirmEditText.getText().toString();
        String deviceInfo = android.os.Build.DEVICE + " | " + android.os.Build.VERSION.RELEASE + " | " + android.os.Build.PRODUCT + " | " + Build.MODEL;

        if (strPassword.equals(strPasswordConfirm)) {

        }
    }
}