using System.Web;
using Microsoft.AspNetCore.Components;

namespace Hedgehog.UI.Infrastructure.Shared;

public class ThemeService
{
    public class Theme
    {
        public string? Text { get; init; }
        
        public string? Value { get; init; }
    }

    public static readonly Theme[] Themes = 
    {
        new() { Text = "Default Theme", Value = "default"},
        new() { Text = "Dark Theme", Value="dark" },
        new() { Text = "Software Theme", Value = "software"},
        new() { Text = "Humanistic Theme", Value = "humanistic" },
        new() { Text = "Standard Theme", Value = "standard" }
    };

    private const string DefaultTheme = "standard";

    private const string QueryParameter = "theme";

    public string CurrentTheme { get; set; } = DefaultTheme;

    public void Initialize(NavigationManager navigationManager)
    {
        var uri = new Uri(navigationManager.ToAbsoluteUri(navigationManager.Uri).ToString());
        var query = HttpUtility.ParseQueryString(uri.Query);
        var value = query.Get(QueryParameter);

        if (value != null && Themes.Any(theme => theme.Value == value))
        {
            CurrentTheme = value;
        }
    }

    public void Change(NavigationManager navigationManager, string theme)
    {
        var url = navigationManager.GetUriWithQueryParameter(QueryParameter, theme);

        navigationManager.NavigateTo(url, true);
    }
}