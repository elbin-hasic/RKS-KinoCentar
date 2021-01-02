package com.example.kinocentar.fragments;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.fragment.app.Fragment;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;

import com.example.kinocentar.R;
import com.example.kinocentar.adapters.MoviesAdapter;
import com.example.kinocentar.data.Storage;
import com.example.kinocentar.helper.MyApiRequest;
import com.example.kinocentar.helper.MyRunnable;
import com.example.kinocentar.helper.MySession;
import com.example.kinocentar.viewmodels.ProjectionMovieViewModel;
import com.example.kinocentar.viewmodels.UserViewModel;

public class ReservationFragment extends Fragment {

    private UserViewModel _user;
    private ProjectionMovieViewModel mData;
    private SwipeRefreshLayout SRL_Filmovi;
    private RecyclerView recyclerViewFilmovi;
    private RecyclerView.Adapter filmoviAdapter;

    public ReservationFragment() {
        // Required empty public constructor
    }

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_reservation, container, false);

        getActivity().setTitle("Moje rezervacije");

        _user = MySession.getUserData();

        popuniPodatke();

        SRL_Filmovi = view.findViewById(R.id.SRL_Filmovi);
        recyclerViewFilmovi = view.findViewById(R.id.RV_Filmovi);
        recyclerViewFilmovi.setHasFixedSize(true);

        return view;
    }

    private void popuniPodatke() {
        MyApiRequest.get(getActivity(), "Rezervacije/GetMoviesByUserName/" + _user.korisnickoIme, new MyRunnable<ProjectionMovieViewModel>() {
            @Override
            public void run(ProjectionMovieViewModel x) {
                Storage.setReservations(x);

                mData = Storage.getReservations();
                if (mData != null) {
                    filmoviAdapter = new MoviesAdapter(getParentFragmentManager(), getContext(), mData, true);
                    recyclerViewFilmovi.setAdapter(filmoviAdapter);
                }

                SRL_Filmovi.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
                    @Override
                    public void onRefresh() {
                        mData = Storage.getReservations();
                    }
                });
            }
        });
    }
}