package com.example.kinocentar;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.kinocentar.helper.MyApiRequest;
import com.example.kinocentar.helper.MyCryption;
import com.example.kinocentar.helper.MyDateTime;
import com.example.kinocentar.helper.MyRunnable;
import com.example.kinocentar.helper.MySession;
import com.example.kinocentar.viewmodels.UserViewModel;
import com.google.android.material.snackbar.Snackbar;

public class RegisterActivity extends AppCompatActivity {

    private EditText firstNameEditText;
    private EditText lastNameEditText;
    private EditText emailEditText;
    private EditText birthDateEditText;
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
        firstNameEditText = findViewById(R.id.ET_Ime);
        lastNameEditText = findViewById(R.id.ET_Prezime);
        emailEditText = findViewById(R.id.ET_Email);
        birthDateEditText = findViewById(R.id.ET_BirthDate);

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
        final String strUsername = usernameEditText.getText().toString();
        final String strPassword = passwordEditText.getText().toString();
        String strPasswordConfirm = passwordConfirmEditText.getText().toString();

        if (strPassword.equals(strPasswordConfirm)) {
            String strFirstName = firstNameEditText.getText().toString();
            String strLastName = lastNameEditText.getText().toString();
            String strEmail = emailEditText.getText().toString();
            String strBirthDate = MyDateTime.PrepareJsonDate(birthDateEditText.getText().toString());

            UserViewModel model = new UserViewModel(strUsername, strPassword, strFirstName, strLastName, strEmail, null, strBirthDate);

            if (model.isUserNameValid() && model.isPasswordValid()) {

                MyApiRequest.post(this, "Korisnici/Registracija", model, new MyRunnable<UserViewModel>() {
                    @Override
                    public void run(UserViewModel user) {
                        checkRegister(user, strUsername, strPassword);
                    }
                });
            }
            else {
                writeMessage("Pogrešan username/password");
            }
        } else {
            writeMessage("Pogrešan password");
        }
    }

    private void checkRegister(UserViewModel user, String strUsername, String strPassword) {
        if (user == null)
        {
            writeMessage("Greška prilikom registracije");
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