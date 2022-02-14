# SpotifyRecommendations
Simple recommendation app of Spotify tracks: https://spotifyrecommendations.yrasin.se/

It was built as an ASP.NET Core Web Application (MVC) in .NET 6 using the minimal hosting model. The project structure is inspired by the proposed recommended project structure Clean Architecture (https://github.com/jasontaylordev/CleanArchitecture).

The project is built using Command Query Responsibility Segregation and the Mediator pattern. Thus the application logic is enclosed and seperated from the Web-project and could be reused for a .NET Web API with similar functionality requirements.

It uses the search and recommendation API provided by Spotify (https://developer.spotify.com/documentation/web-api/reference/#/). The integration is developed using the standard HttpClient with the repository pattern.

## How to start the project
The Spotify integration requires a Client Id and a Client Secret in order to run. These are not stored in the repository and needs to be inserted through for example user secrets or environment variables.

Here is how you set it as a user secret:

```json
dotnet user-secrets set "SpotifyOptions:ClientId" "CLIENT_ID" --project src\SpotifyRecommendations.Web\SpotifyRecommendations.Web.csproj

dotnet user-secrets set "SpotifyOptions:ClientSecret" "CLIENT_SECRET" --project src\SpotifyRecommendations.Web\SpotifyRecommendations.Web.csproj
```

## How to use
1. Search for 1-5 tracks of your musical taste and add them to your list of liked songs. 
2. Then press the 'Get recommendations' button to generate new songs similar to your taste.
3. See your recommended tracks, and refine your recommendations further using the sliders.

## Missing features
If given more time, these feature would be next in line:
* Adding Fluent Validation package for validation of Query and Command requests.
* Adding error logging middleware.
* Adding cache of commonly used queries. Mainly the GetGenres query.
* Adding a mapper package, for encapsulating the mapping of DTO-models and application models.
* Adding pagination functionality of search and recommendation views.
* Switching out the custom IHttpClientFactory for the one now provided in .NET 6.
* Adding unit tests on SpotifyRepository.
* Adding a mocked SpotifyRepository for integration tests and acceptance tests.
* Adding integration tests for testing the application behaviour from the HomeController down to the Spotify integration.