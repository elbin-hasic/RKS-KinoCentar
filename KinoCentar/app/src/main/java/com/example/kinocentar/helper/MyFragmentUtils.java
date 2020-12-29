package com.example.kinocentar.helper;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

public class MyFragmentUtils {
    public static void openAsReplace(FragmentManager fm, int id, Fragment fragment, Boolean addToBackStack) {
        FragmentManager fragmentManager = fm;
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentManager.getBackStackEntryCount();
        fragmentTransaction.replace(id, fragment);
        if (addToBackStack)
        {
            fragmentTransaction.addToBackStack(null);
        }
        fragmentTransaction.commit();
    }
}
