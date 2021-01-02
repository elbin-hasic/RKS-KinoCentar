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
import com.example.kinocentar.helper.MyFragmentUtils;
import com.example.kinocentar.helper.MyImage;
import com.example.kinocentar.viewmodels.ProjectionMovieViewModel;

public class MovieDetailsFragment extends Fragment {

    private ProjectionMovieViewModel.Row _projection;

    private TextView FilmNaslov;
    private TextView FilmOpis;
    private TextView FilmCijena;
    private TextView FilmZanr;
    private Button FilmRezervisi;
    private ImageView FilmSlika;

    public MovieDetailsFragment() {
        // Required empty public constructor
    }

    public MovieDetailsFragment(ProjectionMovieViewModel.Row projection) {
        _projection = projection;
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
        FilmSlika = view.findViewById(R.id.IV_FilmSlika);

        if (_projection != null)
        {
            String _naslovUpper = _projection.naslov.toString().toUpperCase();
            getActivity().setTitle(_naslovUpper);

            FilmNaslov.setText(_naslovUpper);
            FilmOpis.setText(_projection.sadrzaj);
            FilmCijena.setText(_projection.cijena + " KM");

            if (_projection.zanr != null) {
                FilmZanr.setText(_projection.zanr.toUpperCase());
            } else {
                FilmZanr.setText("---");
            }

            if (_projection.plakat != null) {
                FilmSlika.setImageBitmap(MyImage.GenerateBase64(_projection.plakat));
            }

            FilmRezervisi.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    MyFragmentUtils.openAsReplace(getFragmentManager(), R.id.nav_host_fragment, new MovieTicketFragment(_projection), true);
                }
            });
        }

        return view;
    }
}