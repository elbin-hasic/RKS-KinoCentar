package com.example.kinocentar.fragments;

import android.os.Bundle;

import androidx.core.content.ContextCompat;
import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.kinocentar.R;
import com.example.kinocentar.helper.MyFragmentUtils;
import com.example.kinocentar.viewmodels.MovieViewModel;

public class MovieDetailsFragment extends Fragment {

    private MovieViewModel _movie;

    private TextView FilmNaslov;
    private TextView FilmOpis;
    private TextView FilmCijena;
    private TextView FilmZanr;
    private Button FilmRezervisi;
    private ImageView FilmSlika;

    public MovieDetailsFragment() {
        // Required empty public constructor
    }

    public MovieDetailsFragment(MovieViewModel movie) {
        _movie = movie;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_movie_details, container, false);

        FilmNaslov = view.findViewById(R.id.TV_FilmNaslov);
        FilmOpis = view.findViewById(R.id.TV_Opis);
        FilmCijena = view.findViewById(R.id.TV_FilmCijena);
        FilmZanr = view.findViewById(R.id.TV_Zanr);
        FilmRezervisi = view.findViewById(R.id.BTN_Rezervisi);

        /*
        FilmSlika = view.findViewById(R.id.IV_FilmSlika);
        */

        if (_movie != null)
        {
            String _naslovUpper = _movie.Naslov.toString().toUpperCase();
            getActivity().setTitle(_naslovUpper);

            FilmNaslov.setText(_naslovUpper);
            FilmOpis.setText(_movie.Opis);
            FilmCijena.setText(_movie.Cijena + " KM");

            if (_movie.Zanr != null && _movie.Zanr.size() > 0) {
                FilmZanr.setText(_movie.Zanr.get(0).toUpperCase());
            } else {
                FilmZanr.setText("---");
            }

            if (_movie.IsRezervisan) {
                FilmRezervisi.setEnabled(false);
                FilmRezervisi.setClickable(false);
                FilmRezervisi.setBackground(ContextCompat.getDrawable(getContext(), R.drawable.rounded_gray_btn));
                FilmRezervisi.setTextColor(getResources().getColor(R.color.colorGreyDark));
            } else {
                FilmRezervisi.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        MyFragmentUtils.openAsReplace(getFragmentManager(), R.id.nav_host_fragment, new MovieTicketFragment(_movie), true);
                    }
                });
            }

            //Glide.with(Objects.requireNonNull(getContext())).load(_film.ThumbnailURL).into(FilmSlika);
        }

        return view;
    }
}