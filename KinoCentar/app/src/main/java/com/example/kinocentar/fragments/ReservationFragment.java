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
import com.example.kinocentar.viewmodels.MovieViewModel;

import java.util.ArrayList;

public class ReservationFragment extends Fragment {

    private ArrayList<MovieViewModel> mData;
    private SwipeRefreshLayout SRL_Filmovi;
    private RecyclerView recyclerViewFilmovi;
    private RecyclerView.Adapter filmoviAdapter;

    public ReservationFragment() {
        // Required empty public constructor
    }

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_reservation, container, false);

        getActivity().setTitle("Moje rezervacije");

        mData = Storage.getReservationMovies();
        filmoviAdapter = new MoviesAdapter(getParentFragmentManager(), mData);

        SRL_Filmovi = view.findViewById(R.id.SRL_Filmovi);
        recyclerViewFilmovi = view.findViewById(R.id.RV_Filmovi);
        recyclerViewFilmovi.setHasFixedSize(true);
        recyclerViewFilmovi.setAdapter(filmoviAdapter);

        SRL_Filmovi.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                mData = Storage.getReservationMovies();
            }
        });

        return view;
    }
}