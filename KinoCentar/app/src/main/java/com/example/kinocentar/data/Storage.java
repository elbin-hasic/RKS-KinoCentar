package com.example.kinocentar.data;

import com.example.kinocentar.viewmodels.MovieViewModel;

import java.util.ArrayList;

public class Storage {
    // Filmovi
    private static ArrayList<MovieViewModel> movies;
    public static ArrayList<MovieViewModel> getMovies()
    {
        if (movies == null)
        {
            movies = new ArrayList<>();
            movies.add(new MovieViewModel(1, "Gori vatra", 2, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.", true, getZanrList()));
            movies.add(new MovieViewModel(2, "Avatar", 3.5, "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", false, getZanrList()));
            movies.add(new MovieViewModel(3, "Avengers", 2.5, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", false, getZanrList()));
            movies.add(new MovieViewModel(4, "Incsption", 2, "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", true, getZanrList()));
            movies.add(new MovieViewModel(5, "WALL-E", 3.5, "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", false, getZanrList()));
        }

        return movies;
    }

    public static ArrayList<MovieViewModel> getReservationMovies()
    {
        ArrayList<MovieViewModel> result = new ArrayList<>();

        for (MovieViewModel x : movies) {
            if (x.IsRezervisan) {
                result.add(x);
            }
        }

        return result;
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
            spolovi.add("Musko");
            spolovi.add("Zensko");
        }

        return spolovi;
    }
}
