package com.example.kinocentar.viewmodels;

import androidx.lifecycle.ViewModel;

public class UserViewModel extends ViewModel {
    public int id;
    public String korisnickoIme;
    public String token;
    public String lozinkaHash;
    public String lozinkaSalt;

    public String ime;
    public String prezime;
    public String email;
    public String spol;
    public String adresa;
    public String datumRodjenja;
}