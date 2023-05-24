using System.Net.Http.Json;
using System.Text.Json;
namespace DisneyCharacters.Services;

public class CharacterApiService
{
    HttpClient httpClient;
    List<CharacterDataDto> characterList = new List<CharacterDataDto>();

    public CharacterApiService()
    {
        this.httpClient = new HttpClient();
    }

    public async Task<List<CharacterDataDto>> GetCharacters()
    {

        if (characterList?.Count > 0)
            return characterList;

        if (DebugConstants.UseSampleData)
        {
            characterList = await LoadFromSampleData();
        }
        else
        {

            // Online
            var response = await httpClient.GetAsync(ApiConstants.CharacterApiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"DisneyApi returned: {response.Content}");

                var characterListModel = await response.Content.ReadFromJsonAsync<CharacterListDto>();
                Debug.WriteLine($"DisneyApi returned: {characterListModel.Info.Count} {characterListModel.Info.PreviousPage} {characterListModel.Info.NextPage}");

                if (characterListModel.Info.Count == 0 || characterListModel.Data.Count == 0)
                {
                    Debug.WriteLine($"No data returned: {characterListModel}");
                }

                characterList = characterListModel.Data;
            }
            else
            {
                Debug.WriteLine($"DisneyApi response: {response.StatusCode} {response.ReasonPhrase}");
            }
        }
        // await Shell.Current.DisplayAlert("Disney characters", $"Disney Api returned {response.StatusCode} items", "Righto");
        return characterList;
    
    }

    async Task<List<CharacterDataDto>> LoadFromSampleData()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(DebugConstants.SAMPLEDATA_FILE);
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        return JsonSerializer.Deserialize<List<CharacterDataDto>>(contents);
    }
}
