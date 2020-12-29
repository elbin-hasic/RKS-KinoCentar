package com.example.kinocentar.fragments;

import android.graphics.Color;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import androidx.fragment.app.Fragment;

import androidx.annotation.NonNull;
import androidx.appcompat.widget.AppCompatSpinner;
import androidx.lifecycle.ViewModelProviders;

import com.example.kinocentar.R;
import com.example.kinocentar.data.Storage;
import com.example.kinocentar.viewmodels.UserViewModel;

import java.util.ArrayList;
import java.util.List;

public class ProfileFragment extends Fragment {

    private UserViewModel userViewModel;

    private AppCompatSpinner SP_spol;
    private List<String> spol;

    public ProfileFragment() {
        // Required empty public constructor
    }

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_profile, container, false);

        getActivity().setTitle("Moj profil");

        SP_spol = root.findViewById(R.id.SP_Spol);
        spol = Storage.getSpolList();

        final ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(getContext(), android.R.layout.simple_spinner_item, spol);
        // Drop down layout style
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

        return root;
    }
}