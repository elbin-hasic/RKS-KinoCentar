package com.example.kinocentar;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;

import com.example.kinocentar.helper.MySession;
import com.example.kinocentar.viewmodels.UserViewModel;

public class RootActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        UserViewModel x = MySession.getUserData();

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