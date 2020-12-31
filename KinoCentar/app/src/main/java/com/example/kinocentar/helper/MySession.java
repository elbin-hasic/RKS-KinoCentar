package com.example.kinocentar.helper;

import android.content.Context;
import android.content.SharedPreferences;

import com.example.kinocentar.viewmodels.LoginViewModel;
import com.example.kinocentar.viewmodels.UserViewModel;

public class MySession {
    private static final String PREFS_NAME = "DatotekaZaSharedPrefernces";
    private static String user_key = "KinoCentarUserDataKey";
    private static String login_key = "KinoCentarUserDataKey";

    public static UserViewModel getUserData()
    {
        SharedPreferences sharedPreferences = MyApp.getContext().getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE);
        String strJson = sharedPreferences.getString(user_key, "");
        if (strJson.length() == 0)
            return null;

        UserViewModel x = MyGson.build().fromJson(strJson, UserViewModel.class);
        return x;
    }

    public static void setUserData(UserViewModel x)
    {
        String strJson = (x != null) ? MyGson.build().toJson(x) : "";

        SharedPreferences sharedPreferences = MyApp.getContext().getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        editor.putString(user_key, strJson);
        editor.apply();
    }

    /*public static LoginViewModel getLoginData()
    {
        SharedPreferences sharedPreferences = MyApp.getContext().getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE);
        String strJson = sharedPreferences.getString(login_key, "");
        if (strJson.length() == 0)
            return null;

        LoginViewModel x = MyGson.build().fromJson(strJson, LoginViewModel.class);
        return x;
    }

    public static void setLoginData(LoginViewModel x)
    {
        String strJson = (x != null) ? MyGson.build().toJson(x) : "";

        SharedPreferences sharedPreferences = MyApp.getContext().getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        editor.putString(login_key, strJson);
        editor.apply();
    }*/
}
