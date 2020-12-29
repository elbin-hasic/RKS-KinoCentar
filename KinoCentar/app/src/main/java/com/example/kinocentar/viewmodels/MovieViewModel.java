package com.example.kinocentar.viewmodels;

import androidx.lifecycle.ViewModel;

import java.util.List;

public class MovieViewModel extends ViewModel {
    public int Id;
    public String Naslov;
    public double Cijena;
    public String Opis;
    public boolean IsRezervisan;
    public List<String> Zanr;

    public MovieViewModel(int id, String naslov, double cijena, String opis, boolean isRezervisan, List<String> zanr) {
        this.Id = id;
        this.Naslov = naslov;
        this.Cijena = cijena;
        this.Opis = opis;
        this.IsRezervisan = isRezervisan;
        this.Zanr = zanr;
    }
}