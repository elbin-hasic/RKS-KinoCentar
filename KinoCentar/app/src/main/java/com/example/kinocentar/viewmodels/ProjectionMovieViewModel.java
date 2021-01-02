package com.example.kinocentar.viewmodels;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class ProjectionMovieViewModel implements Serializable
{
     public static class Row implements Serializable
     {
          public int projekcijaId;
          public int filmId;
          public String naslov;
          public String sadrzaj;
          public double cijena;
          public String zanr;
          public String plakat;
          public String plakatThumb;
          public String datum;
          public String vrijediOd;
          public String vrijediDo;

          public String termin;
          public ArrayList<TermViewModel> termini;
     }

     public ArrayList<Row> rows;
}
