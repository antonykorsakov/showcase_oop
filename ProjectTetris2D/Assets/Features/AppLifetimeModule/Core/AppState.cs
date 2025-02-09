namespace Features.AppLifetimeModule.Core
{
    public enum AppState
    {
        Undefined = 0,

        AppInitializing,    // StartingSplashScreen
        Gdpr,               // (General Data Protection Regulation)

        OutOfGame,      // MainScreen,
        PrepareGame,    // LoadingScreen (Create or Clear something),
        Game,           // GameScreen
        GameOver,       // ResultScreen + ReGame button
        Pause,          // ResultScreen + Continue button
        AfterPause,     // 3.. 2.. 1.. Game
    }
}