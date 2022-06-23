using System.Net.Http.Json;

namespace MonkeyFinder2.Services;

public class MonkeyService
{
    HttpClient httpClient;

    public MonkeyService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    List<Monkey> monkeyList = new ();
    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeyList.Count > 0)
            return monkeyList;

        //var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";
        //var response = await httpClient.GetAsync(url);

        //if (response.IsSuccessStatusCode)
        //{
        //    monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        //}

        using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        using var reader = new StreamReader(stream);
        string content = reader.ReadToEnd();
        monkeyList = JsonSerializer.Deserialize<List<Monkey>>(content);

        return monkeyList;
    }
}
