package com.example.kinocentar.adapters;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.fragment.app.FragmentManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.kinocentar.R;
import com.example.kinocentar.fragments.MovieDetailsFragment;
import com.example.kinocentar.helper.MyFragmentUtils;
import com.example.kinocentar.viewmodels.MovieViewModel;
import com.example.kinocentar.views.MovieView;

import java.util.List;

public class MoviesAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    private FragmentManager _fm;
    private List<MovieViewModel> _data;

    public MoviesAdapter(FragmentManager fragmentManager, List<MovieViewModel> data)
    {
        _fm = fragmentManager;
        _data = data;
    }

    @NonNull
    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                    .inflate(R.layout.one_film_layout, parent, false);
        return new MovieView(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RecyclerView.ViewHolder holder, final int position) {
        final MovieView _holder = (MovieView) holder;

        _holder.TV_FilmNaziv.setText(_data.get(position).Naslov);
        _holder.BTN_Prikazi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                MyFragmentUtils.openAsReplace(_fm, R.id.nav_host_fragment, new MovieDetailsFragment(_data.get(position)), true);
            }
        });
    }

    @Override
    public int getItemCount() {
        return _data.size();
    }
}
