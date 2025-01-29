To cover service authentication, we need to make following steps:

1) Add to new service Reference to **`SystemMonitor.Auth`**
2) To `Program.cs` add extension method `AddJwtAuthentication()`
3) To `appsettings.json` add configuration the same as in `SystemMonitor.Auth` service
4) To `Program.cs` add `app.UseAuthentication();` and `app.UseAuthorization();` in `Configure` method
5) And finally, add `[Authorize]` attribute to controller or action


Future improvements:
- Add refresh token **!**
- Cover with unit tests
- Make centralized jwt token configuration in `SystemMonitor.Auth` service, don't use 
`appsettings.json` of any another services