package com.example.kinocentar.viewmodels;

import android.util.Patterns;

import java.io.Serializable;

public class UserViewModel implements Serializable {
    public int id;
    public int tipKorisnikaId;
    public String korisnickoIme;
    public String token;
    public String lozinka;
    public String lozinkaHash;
    public String lozinkaSalt;

    public String ime;
    public String prezime;
    public String email;
    public String spol;
    public String datumRodjenja;

    public UserViewModel()
    {

    }

    public UserViewModel(String korisnickoIme, String lozinka, String ime, String prezime,
                         String email, String spol, String datumRodjenja)
    {
        this.korisnickoIme = korisnickoIme;
        this.lozinka = lozinka;
        this.ime = ime;
        this.prezime = prezime;
        this.email = email;
        this.spol = spol;
        this.datumRodjenja = datumRodjenja;
    }

    // A placeholder username validation check
    public boolean isUserNameValid() {
        if (this.korisnickoIme == null) {
            return false;
        }
        if (this.korisnickoIme.contains("@")) {
            return Patterns.EMAIL_ADDRESS.matcher(this.korisnickoIme).matches();
        } else {
            return !this.korisnickoIme.trim().isEmpty();
        }
    }

    // A placeholder password validation check
    public boolean isPasswordValid() {
        return this.lozinka != null && this.lozinka.trim().length() > 5;
    }
}