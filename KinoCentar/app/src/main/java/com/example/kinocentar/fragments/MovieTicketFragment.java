package com.example.kinocentar.fragments;

import android.graphics.Color;
import android.os.Bundle;

import androidx.appcompat.widget.AppCompatSpinner;
import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.kinocentar.R;
import com.example.kinocentar.data.Storage;
import com.example.kinocentar.helper.MyApiRequest;
import com.example.kinocentar.helper.MyDateTime;
import com.example.kinocentar.helper.MyFragmentUtils;
import com.example.kinocentar.helper.MyImage;
import com.example.kinocentar.helper.MyRunnable;
import com.example.kinocentar.helper.MySession;
import com.example.kinocentar.viewmodels.LoginViewModel;
import com.example.kinocentar.viewmodels.ProjectionMovieViewModel;
import com.example.kinocentar.viewmodels.ReservationViewModel;
import com.example.kinocentar.viewmodels.TermViewModel;
import com.example.kinocentar.viewmodels.UserViewModel;
import com.google.android.material.snackbar.Snackbar;

import java.util.ArrayList;
import java.util.Calendar;

public class MovieTicketFragment extends Fragment {

    private ProjectionMovieViewModel.Row _projection;
    private UserViewModel _user;

    private int _selected_termin_id;

    private TextView FilmNaslov;
    private TextView FilmCijena;
    private Button ConfirmPayment;
    private ImageView FilmBack;
    private ImageView FilmTicket;

    private AppCompatSpinner SP_Termin;
    private ArrayList<String> termini;

    public MovieTicketFragment() {
        // Required empty public constructor
    }

    public MovieTicketFragment(ProjectionMovieViewModel.Row projection)
    {
        _projection = projection;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_movie_ticket, container, false);

        _user = MySession.getUserData();

        FilmNaslov = view.findViewById(R.id.TV_FilmNaslov);
        FilmCijena = view.findViewById(R.id.TV_FilmCijena);
        FilmBack = view.findViewById(R.id.IV_Film_Back);
        FilmTicket = view.findViewById(R.id.IV_Film_Ticket);
        ConfirmPayment = view.findViewById(R.id.BTN_Confirm);

        ConfirmPayment.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnSaveClick();
            }
        });

        SP_Termin = view.findViewById(R.id.SP_Termin);

        if (_projection != null) {
            _selected_termin_id = 0;

            FilmNaslov.setText(_projection.naslov);
            FilmCijena.setText(_projection.cijena + " KM");

            if (_projection.plakat != null) {
                FilmBack.setImageBitmap(MyImage.GenerateBase64(_projection.plakat));
                FilmTicket.setImageBitmap(MyImage.GenerateBase64(_projection.plakat));
            }

            termini = new ArrayList<>();
            for (int i = 0; i < _projection.termini.size(); i++)
            {
                if (i == 0)
                {
                    _selected_termin_id = _projection.termini.get(i).id;
                }
                termini.add(_projection.termini.get(i).termin);
            }

            final ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(getContext(), android.R.layout.simple_spinner_item, termini);
            dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            SP_Termin.setAdapter(dataAdapter);

            SP_Termin.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                @Override
                public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                    _selected_termin_id = _projection.termini.get(i).id;
                    ((TextView) adapterView.getChildAt(0)).setTextColor(Color.WHITE);
                }

                @Override
                public void onNothingSelected(AdapterView<?> adapterView) {

                }
            });
        }

        return view;
    }

    private void do_btnSaveClick() {
        ReservationViewModel model = new ReservationViewModel(_user.id, _projection.projekcijaId, _selected_termin_id,
                _projection.cijena, MyDateTime.PrepareJsonDate(Calendar.getInstance().getTime()));

        MyApiRequest.post(getActivity(), "Rezervacije", model, new MyRunnable<ReservationViewModel>() {
            @Override
            public void run(ReservationViewModel x) {
                View parentLayout = getActivity().findViewById(android.R.id.content);
                Snackbar.make(parentLayout, "Uspješno ste rezervisali film", Snackbar.LENGTH_LONG).show();
                MyFragmentUtils.openAsReplace(getFragmentManager(), R.id.nav_host_fragment, new MovieDetailsFragment(_projection), true);
            }
        });
    }
}