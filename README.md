# SpotifyRecommendations
 Simple recommendation app for Spotify tracks.

## How to start the project
You need to set the following user secret variables to be able to start the project:

```json
dotnet user-secrets set "SpotifyOptions:ClientId" "CLIENT_ID" --project src\Sp
otifyRecommendations.Web\SpotifyRecommendations.Web.csproj

dotnet user-secrets set "SpotifyOptions:ClientSecret" "CLIENT_SECRET" --project src\Sp
otifyRecommendations.Web\SpotifyRecommendations.Web.csproj
```

