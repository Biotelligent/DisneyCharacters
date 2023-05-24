using DisneyCharacters.Services;

namespace DisneyCharacters.ViewModel;

public partial class CharacterViewModel: BaseViewModel
{
    CharacterApiService characterApiService;
    IConnectivity connectivity;
    public ObservableCollection<CharacterModel> Characters { get; } = new();

    public CharacterViewModel(CharacterApiService disneyApiService, IConnectivity connectivity)
    {
        Title = "Disney Characters";
        this.characterApiService = disneyApiService;
        this.connectivity = connectivity;

        if (DebugConstants.AutoLoadData)
        {
            Task.Run(async () => await RefreshDataCommand.ExecuteAsync(null));
        }
    }
    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task RefreshDataAsync()
    {
        if (IsBusy)
            return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No network detected", $"Please check your internet connection and try again.", "OK");
                return;
            }

            IsBusy = true;
            var characterList = await characterApiService.GetCharacters();
            var characterModels = characterList.Select(character => new CharacterModel(character)).ToList().OrderByDescending(character => character.Popularity);
            if (Characters.Count != 0)
                Characters.Clear();
            foreach (var character in characterModels)
                Characters.Add(character);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get data: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }

    }
}
