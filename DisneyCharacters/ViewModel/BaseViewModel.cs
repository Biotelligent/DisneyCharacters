﻿namespace DisneyCharacters.ViewModel;

public partial class BaseViewModel: ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    bool isError;

    [ObservableProperty]
    string errorMessage;
}
