package com.example.kinocentar.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.fragment.app.FragmentManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.kinocentar.R;
import com.example.kinocentar.fragments.MovieDetailsFragment;
import com.example.kinocentar.helper.MyFragmentUtils;
import com.example.kinocentar.helper.MyImage;
import com.example.kinocentar.viewmodels.ProjectionMovieViewModel;
import com.example.kinocentar.views.MovieView;

public class MoviesAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    private FragmentManager _fm;
    private Context _ctx;
    private ProjectionMovieViewModel _data;
    private Boolean _isReservation;

    public MoviesAdapter(FragmentManager fragmentManager, Context ctx, ProjectionMovieViewModel data, Boolean isReservation)
    {
        _fm = fragmentManager;
        _ctx = ctx;
        _data = data;
        _isReservation = isReservation;
    }

    @NonNull
    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view;
        if (_isReservation) {
            view = LayoutInflater.from(parent.getContext())
                    .inflate(R.layout.one_film_layout_2, parent, false);
        } else {
            view = LayoutInflater.from(parent.getContext())
                    .inflate(R.layout.one_film_layout, parent, false);
        }
        return new MovieView(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RecyclerView.ViewHolder holder, final int position) {
        final MovieView _holder = (MovieView) holder;

        ProjectionMovieViewModel.Row _projection = _data.rows.get(position);

        _holder.TV_FilmNaziv.setText(_projection.naslov);
        if (_projection.plakatThumb != null) {
            _holder.IV_Film.setImageBitmap(MyImage.GenerateBase64(_projection.plakatThumb));
        }

        if (_isReservation) {
            _holder.TV_FilmTermin.setText("Termin: " + _projection.termin);
        }

        _holder.BTN_Prikazi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                MyFragmentUtils.openAsReplace(_fm, R.id.nav_host_fragment, new MovieDetailsFragment(_data.rows.get(position)), true);
            }
        });
    }

    @Override
    public int getItemCount() {
        return _data.rows.size();
    }
}
