package com.example.kinocentar.viewmodels;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

public class MoviesViewModel extends ViewModel {

    private MutableLiveData<String> mText;

    public MoviesViewModel() {
        mText = new MutableLiveData<>();
        mText.setValue("This is movies fragment");
    }

    public LiveData<String> getText() {
        return mText;
    }
}