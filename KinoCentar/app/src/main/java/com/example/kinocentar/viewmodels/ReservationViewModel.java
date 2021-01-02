package com.example.kinocentar.viewmodels;

import java.io.Serializable;

public class ReservationViewModel implements Serializable {
    public int id;
    public int korisnikId;
    public int projekcijaId;
    public int projekcijaTerminId;
    public int brojSjedista;
    public double cijena;
    public String datum;
    public String datumProjekcije;

    public ReservationViewModel(int korisnikId, int projekcijaId, int projekcijaTerminId,
                          double cijena, String datum)
    {
        this.korisnikId = korisnikId;
        this.projekcijaId = projekcijaId;
        this.projekcijaTerminId = projekcijaTerminId;
        this.brojSjedista = 0;
        this.cijena = cijena;
        this.datum = datum;
        this.datumProjekcije = datum;
    }
}
