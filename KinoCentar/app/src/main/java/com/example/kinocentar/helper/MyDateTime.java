package com.example.kinocentar.helper;

import java.text.ParseException;
import java.text.SimpleDateFormat;

public class MyDateTime
{
    public static java.util.Date ReadJsonDate(String dateTime) {

        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");

        java.util.Date date = null;

        try {
            date = format.parse(dateTime);
        } catch (ParseException e) {
            e.printStackTrace();
        }

        return date;
    }

    public static String ReadJsonDateAsString(String dateTime) {

        SimpleDateFormat format = new SimpleDateFormat("dd.MM.yyyy");

        java.util.Date date = ReadJsonDate(dateTime);

        String convertedDate = format.format(date);

        return convertedDate;
    }

    public static String ReadJsonDateTimeAsString(String dateTime) {

        SimpleDateFormat format = new SimpleDateFormat("dd.MM.yyyy HH:mm");

        java.util.Date date = ReadJsonDate(dateTime);

        String convertedDate = format.format(date);

        return convertedDate;
    }

    public static String PrepareJsonDate(java.util.Date date) {

        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        return format.format(date);
    }

    public static String PrepareJsonDate(String dateTime) {

        SimpleDateFormat format = new SimpleDateFormat("dd.MM.yyyy");

        java.util.Date date = null;

        try {
            date = format.parse(dateTime);
        } catch (ParseException e) {
            e.printStackTrace();
        }

        return PrepareJsonDate(date);
    }
}
