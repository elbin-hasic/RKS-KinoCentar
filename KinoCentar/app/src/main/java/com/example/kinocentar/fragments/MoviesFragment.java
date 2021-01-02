package com.example.kinocentar.fragments;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.TextView;

import androidx.fragment.app.Fragment;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;

import com.example.kinocentar.R;
import com.example.kinocentar.adapters.MoviesAdapter;
import com.example.kinocentar.data.Storage;
import com.example.kinocentar.helper.MyApiRequest;
import com.example.kinocentar.helper.MyRunnable;
import com.example.kinocentar.viewmodels.ProjectionMovieViewModel;

import java.util.ArrayList;

public class MoviesFragment extends Fragment {

    private ProjectionMovieViewModel mData;
    private SwipeRefreshLayout SRL_Filmovi;
    private RecyclerView recyclerViewFilmovi;
    private RecyclerView.Adapter filmoviAdapter;

    private TextView pretragaText;
    private ImageButton pretraga;

    public MoviesFragment() {
        // Required empty public constructor
    }

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_movies, container, false);

        getActivity().setTitle("Filmovi");

        mData = new ProjectionMovieViewModel();
        mData.rows = new ArrayList<>();

        popuniPodatke();

        SRL_Filmovi = view.findViewById(R.id.SRL_Filmovi);
        recyclerViewFilmovi = view.findViewById(R.id.RV_Filmovi);
        recyclerViewFilmovi.setHasFixedSize(true);

        pretragaText = view.findViewById(R.id.ET_Search);
        pretraga = view.findViewById(R.id.BTN_Pretraga);
        pretraga.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnPretragaClick();
            }
        });

        return view;
    }

    private void do_btnPretragaClick() {
        String query = pretragaText.getText().toString();
        popuniMainData(Storage.getProjectionsByName(query));
    }

    private void popuniPodatke() {
        MyApiRequest.get(getActivity(), "Projekcije/ActiveMovieList", new MyRunnable<ProjectionMovieViewModel>() {
            @Override
            public void run(ProjectionMovieViewModel x) {
                Storage.setProjections(x);

                popuniMainData(Storage.getProjections());
                filmoviAdapter = new MoviesAdapter(getParentFragmentManager(), getContext(), mData, false);
                recyclerViewFilmovi.setAdapter(filmoviAdapter);

                SRL_Filmovi.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
                    @Override
                    public void onRefresh() {
                        popuniMainData(Storage.getProjections());
                    }
                });
            }
        });
    }

    private void popuniMainData(ProjectionMovieViewModel storageData) {
        mData.rows.clear();
        for (ProjectionMovieViewModel.Row x: storageData.rows) {
            mData.rows.add(x);
        }
    }
}