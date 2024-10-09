using System;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Devlooped.Web;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace OpenAI.Chat;

public class ChatClientTypedExtensionsTests(ITestOutputHelper output)
{
    [SecretsFact("OpenAI:Key")]
    public async Task CanScrapPage()
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

        var client = new OpenAIClient(configuration["OpenAI:Key"]!);
        var chat = client.GetChatClient("gpt-4o");

        using var http = new HttpClient();
        http.DefaultRequestHeaders.AcceptLanguage.Add(new("en-US"));

        var html = await http.GetStringAsync("https://www.imdb.com/chart/moviemeter/?ref_=nv_mv_mpm&genres=thriller&user_rating=6%2C&sort=user_rating%2Cdesc&num_votes=50000%2C");
        // By parsing, we eliminate lots of noise and only keep the relevant parts (i.e. skip headers, scripts and styles).
        var doc = HtmlDocument.Parse(html);

        var response = await chat.CompleteChatAsync<Movie[]>([
            new SystemChatMessage(
                """
                You are an HTML page scraper. 
                You use exclusively the data in the following HTML page to parse and return a list of movies.
                You perform smart type conversion and parsing as needed to fit the result schema in JSON format.
                """),
            new UserChatMessage(doc.CssSelectElement("html")!.ToString(SaveOptions.DisableFormatting)),
        ]);

        Assert.Equal(16, response.Length);
        Assert.Contains(response, x => x.Title == "The Dark Knight");
        Assert.Contains(response, x => x.Title == "Joker");

        output.WriteLine(JsonSerializer.Serialize(response, new JsonSerializerOptions(JsonSerializerOptions.Web) { WriteIndented = true }));
    }

    public record Movie(string Title, int Year, TimeSpan Duration, string AgeRating, StarsRating Stars, string Url);

    public record StarsRating(double Stars, long Votes);
}
