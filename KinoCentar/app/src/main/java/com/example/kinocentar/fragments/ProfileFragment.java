package com.example.kinocentar.fragments;

import android.graphics.Color;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import androidx.fragment.app.Fragment;

import androidx.annotation.NonNull;
import androidx.appcompat.widget.AppCompatSpinner;

import com.example.kinocentar.R;
import com.example.kinocentar.data.Storage;
import com.example.kinocentar.helper.MyApiRequest;
import com.example.kinocentar.helper.MyDateTime;
import com.example.kinocentar.helper.MyRunnable;
import com.example.kinocentar.helper.MySession;
import com.example.kinocentar.viewmodels.UserViewModel;
import com.google.android.material.snackbar.Snackbar;

import java.util.ArrayList;

public class ProfileFragment extends Fragment {

    private UserViewModel user;

    private AppCompatSpinner SP_spol;
    private ArrayList<String> spol;

    private EditText usernameEditText;
    private EditText firstNameEditText;
    private EditText lastNameEditText;
    private EditText emailEditText;
    private EditText birthDateEditText;
    private Button saveButton;

    public ProfileFragment() {
        // Required empty public constructor
    }

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_profile, container, false);

        getActivity().setTitle("Moj profil");

        user = MySession.getUserData();

        usernameEditText = view.findViewById(R.id.ET_UserName);
        firstNameEditText = view.findViewById(R.id.ET_FirstName);
        lastNameEditText = view.findViewById(R.id.ET_LastName);
        emailEditText = view.findViewById(R.id.ET_Email);
        birthDateEditText = view.findViewById(R.id.ET_BirthDate);

        SP_spol = view.findViewById(R.id.SP_Spol);
        spol = Storage.getSpolList();

        final ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(getContext(), android.R.layout.simple_spinner_item, spol);
        dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        SP_spol.setAdapter(dataAdapter);

        SP_spol.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                ((TextView) adapterView.getChildAt(0)).setTextColor(Color.WHITE);
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        saveButton = view.findViewById(R.id.BTN_Snimi);
        saveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnSaveClick();
            }
        });
        
        popuniPodatke();

        return view;
    }

    private void do_btnSaveClick() {
        updatePodatke();
        MyApiRequest.put(getActivity(), "Korisnici/" + user.id, user, new MyRunnable<String>() {
            @Override
            public void run(String x) {
                MySession.setUserData(user);

                View parentLayout = getActivity().findViewById(android.R.id.content);
                Snackbar.make(parentLayout, "Uspješno ste snimi podatke", Snackbar.LENGTH_LONG).show();
            }
        });
    }

    private void popuniPodatke() {
        usernameEditText.setText(user.korisnickoIme);
        firstNameEditText.setText(user.ime);
        lastNameEditText.setText(user.prezime);
        emailEditText.setText(user.email);
        birthDateEditText.setText(MyDateTime.ReadJsonDateAsString(user.datumRodjenja));
        SP_spol.setSelection(0);
        if (user.spol != null) {
            if (user.spol.equals("M") || user.spol.equals("Musko") || user.spol.equals("Muško")) {
                SP_spol.setSelection(1);
            } else if (user.spol.equals("Ž") || user.spol.equals("Z") ||
                       user.spol.equals("Žensko") || user.spol.equals("Zensko")){
                SP_spol.setSelection(2);
            }
        }
    }

    private void updatePodatke() {
        user.korisnickoIme = usernameEditText.getText().toString();
        user.ime = firstNameEditText.getText().toString();
        user.prezime = lastNameEditText.getText().toString();
        user.email = emailEditText.getText().toString();
        user.datumRodjenja = MyDateTime.PrepareJsonDate(birthDateEditText.getText().toString());
        user.spol = SP_spol.getSelectedItem().toString();
    }
}