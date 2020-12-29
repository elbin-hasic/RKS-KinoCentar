package com.example.kinocentar;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;

import com.example.kinocentar.helper.MySession;
import com.example.kinocentar.viewmodels.LoginViewModel;

public class RootActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //MySession.setKorisnik(null);
        LoginViewModel x = MySession.getLoginData();

        if (x == null)
        {
            startActivity(new Intent(this, LoginActivity.class));
        }
        else
        {
            startActivity(new Intent(this, MainActivity.class));
        }
    }
}