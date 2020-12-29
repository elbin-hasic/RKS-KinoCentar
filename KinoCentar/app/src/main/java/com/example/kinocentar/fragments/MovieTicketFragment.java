package com.example.kinocentar.fragments;

import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.kinocentar.R;
import com.example.kinocentar.viewmodels.MovieViewModel;

public class MovieTicketFragment extends Fragment {

    private MovieViewModel _movie;

    private TextView FilmNaslov;
    private TextView FilmCijena;
    private TextView NewBalance;
    private Button ConfirmPayment;
    private ImageView FilmBack;
    private ImageView FilmTicket;

    public MovieTicketFragment() {
        // Required empty public constructor
    }

    public MovieTicketFragment(MovieViewModel movie) {
        _movie = movie;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_movie_ticket, container, false);

        FilmNaslov = view.findViewById(R.id.TV_FilmNaslov);
        FilmCijena = view.findViewById(R.id.TV_FilmCijena);
        //NewBalance = v.findViewById(R.id.TV_NewBalance);
        /*FilmBack = v.findViewById(R.id.IV_Film_Back);
        FilmTicket = v.findViewById(R.id.IV_Film_Ticket);*/
        ConfirmPayment = view.findViewById(R.id.BTN_Confirm);

        if (_movie != null) {
            FilmNaslov.setText(_movie.Naslov);
            FilmCijena.setText(_movie.Cijena + " KM");
        }

        return view;
    }
}