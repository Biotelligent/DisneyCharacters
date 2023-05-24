namespace DisneyCharacters;

public static class ApiConstants
{
    /**
     * Refer https://disneyapi.dev/docs/
     * e.g. https://api.disneyapi.dev/character?page=1&pageSize=7450
     */
    public static string CharacterApiBaseUrl = "https://api.disneyapi.dev/character";
    public static string CharacterApiGetCharacter = $"{CharacterApiBaseUrl}/{0}";
}

