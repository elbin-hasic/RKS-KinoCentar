package com.example.kinocentar.views;

import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.recyclerview.widget.RecyclerView;

import com.example.kinocentar.R;

public class MovieView extends RecyclerView.ViewHolder{
    private View mView;
    public ImageView IV_Film;
    public TextView TV_FilmNaziv;
    public TextView TV_FilmTermin;
    public ConstraintLayout CL_root;
    public Button BTN_Prikazi;
    public MovieView(@NonNull View itemView) {
        super(itemView);
        mView = itemView;
        IV_Film = mView.findViewById(R.id.IV_FilmSlika);
        TV_FilmNaziv = mView.findViewById(R.id.TV_FilmNaziv);
        TV_FilmTermin = mView.findViewById(R.id.TV_FilmTermin);
        BTN_Prikazi = mView.findViewById(R.id.BTN_Prikazi);
        CL_root = mView.findViewById(R.id.CL_root_one);
    }
}
