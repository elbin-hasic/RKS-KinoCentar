package com.example.kinocentar.data;

import com.example.kinocentar.viewmodels.ProjectionMovieViewModel;

import java.util.ArrayList;
import java.util.List;

public class Storage {
    // Filmovi
    private static ProjectionMovieViewModel _projections;

    public static ProjectionMovieViewModel getProjections()
    {
        return _projections;
    }

    public static void setProjections(ProjectionMovieViewModel projections)
    {
        _projections = projections;
    }

    public static ProjectionMovieViewModel getProjectionsByName(String query) {
        ProjectionMovieViewModel result = new ProjectionMovieViewModel();
        result.rows = new ArrayList<>();

        for (ProjectionMovieViewModel.Row x: _projections.rows) {
            if (x.naslov.toLowerCase().startsWith(query.toLowerCase())) {
                result.rows.add(x);
            }
        }

        return result;
    }

    private static ProjectionMovieViewModel _reservations;

    public static ProjectionMovieViewModel getReservations()
    {
        return _reservations;
    }

    public static void setReservations(ProjectionMovieViewModel reservations)
    {
        _reservations = reservations;
    }

    // Žanrovi
    private static ArrayList<String> zanrovi;
    public static ArrayList<String> getZanrList()
    {
        if (zanrovi == null)
        {
            zanrovi = new ArrayList<>();
            zanrovi.add("Komedija");
            zanrovi.add("Romantika");
        }

        return zanrovi;
    }

    // Spolovi
    private static ArrayList<String> spolovi;
    public static ArrayList<String> getSpolList()
    {
        if (spolovi == null)
        {
            spolovi = new ArrayList<>();
            spolovi.add(" ");
            spolovi.add("Muško");
            spolovi.add("Žensko");
        }

        return spolovi;
    }
}
