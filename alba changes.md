# Changes made in the original Oqtane code base

### 2026.06.25
- Add OQTANE_APPSETTINGS_PATH
    - Project: Oqtane.Server
        - Program.cs: Added OQTANE_APPSETTINGS_PATH environment variable to allow for custom appsettings.json path
        - ConfigManager.cs: Updated to read appsettings.json from OQTANE_APPSETTINGS_PATH if set

### 2026.07.23
- Add sync event to notify others of new site
    - Project: Oqtane.Server.Controllers
        - InstallationController.cs: Added SyncEvent to send notification of new site creation
- Site install returns the first Alias in result
        - DatabaseManager.cs: Installation CreateSite(InstallConfig install) return value contains Alias
        - DatabaseManager.cs: private Installation InitializeSites(InstallConfig install)

### 2026.09.14
- Fix Blazor and Radzen component initialization error
    - Project: Oqtane.Server.Components
        - App.razor: Add the Radzen script to load every site and page
            <script src="_content/Radzen.Blazor/Radzen.Blazor.js?v=@(typeof(Radzen.Colors).Assembly.GetName().Version)"></script>
