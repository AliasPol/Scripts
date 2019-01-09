using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GameData : MonoBehaviour {

    public bool internetAvailable;
    public bool rateWindowVisible;
    public bool gameCanBeLoaded;
    public bool gameInstalled;
    public bool gameDataLoaded;
    public bool gameLaunched;
    public bool userSignedIn;
    public bool inMainMenu;
    public bool backFromGame;
    public bool backFromMap;
    public bool soundsMuted;
    public bool musicMuted;
    public float globalVolume = 0.4f;
    public int gameLaunchCounter;
    public int[] levelsPoints;
    public int lastUnlockedLevel;
    public int lastPlayedLevel;
    int levels = 60;
    public int[,] powersAmount;

    public float timeSpendInGame;

    public List<string> standardAchievements;
    public List<string> incrementalAchievements;

    bool countingGameTime;

    ISavedGameMetadata currentGame = null;

    private static GameData _Instance;
    public static GameData Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<GameData>();
               // DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        levelsPoints = new int[levels];
        SetPowersAmounts();
        LoadGameSettings();
    }

    void Start()
    {
        //CheckInternetConnectionOnStart();
    }

    void Update()
    {
        if (countingGameTime && !rateWindowVisible)
        {
            timeSpendInGame += Time.deltaTime;
        }
    }

    void SetPowersAmounts()
    {
        powersAmount = new int[, ]{ {0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0},
                                    {5, 0, 0, 0, 0, 0, 0, 0},
                                    {3, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 3, 0, 0, 0, 0, 0, 0},
                                    {0, 4, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 3, 0, 0, 0, 0, 0},
                                    {0, 0, 2, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 2, 0, 0, 0, 0},
                                    {0, 0, 0, 5, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 6, 0, 0, 0},
                                    {0, 0, 0, 0, 4, 0, 0, 0},
                                    {3, 0, 0, 0, 0, 3, 0, 0},
                                    {2, 0, 0, 0, 0, 3, 0, 0},
                                    {1, 0, 1, 0, 0, 0, 1, 0},
                                    {0, 2, 0, 0, 0, 0, 1, 0},
                                    {2, 0, 0, 0, 0, 0, 0, 1},
                                    {0, 0, 0, 0, 0, 0, 0, 3},
                                    {0, 0, 1, 0, 2, 0, 0, 1},
                                    {2, 0, 0, 0, 0, 0, 1, 1},
                                    {2, 1, 1, 1, 0, 0, 0, 0},
                                    {2, 0, 1, 0, 0, 0, 0, 1},
                                    {0, 1, 2, 1, 1, 0, 0, 0},
                                    {2, 0, 1, 0, 0, 0, 1, 0},
                                    {1, 1, 1, 0, 1, 0, 0, 0},
                                    {2, 0, 1, 2, 0, 1, 0, 0},
                                    {0, 0, 0, 1, 0, 0, 0, 0},
                                    {1, 1, 1, 0, 0, 0, 1, 0},
                                    {3, 0, 0, 1, 3, 0, 0, 0},
                                    {2, 1, 0, 1, 0, 0, 0, 0},
                                    {2, 0, 0, 2, 1, 1, 0, 0},
                                    {2, 0, 1, 1, 0, 0, 0, 1},
                                    {4, 1, 1, 2, 2, 0, 0, 0},
                                    {0, 0, 5, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 1},
                                    {3, 0, 1, 0, 0, 0, 0, 0},
                                    {2, 0, 0, 0, 1, 0, 0, 0},
                                    {0, 1, 0, 0, 0, 0, 0, 0},
                                    {2, 0, 0, 0, 0, 0, 0, 0},
                                    {1, 1, 1, 0, 0, 0, 0, 0},
                                    {2, 0, 2, 0, 0, 0, 0, 0},
                                    {2, 0, 0, 1, 0, 1, 0, 0},
                                    {4, 0, 0, 0, 3, 0, 0, 0},
                                    {2, 0, 0, 0, 0, 0, 0, 0},
                                    {4, 0, 0, 0, 0, 1, 0, 0},
                                    {2, 0, 0, 0, 0, 2, 0, 1},
                                    {4, 0, 0, 0, 0, 0, 0, 0},
                                    {1, 1, 1, 1, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 1},
                                    {0, 0, 0, 2, 1, 0, 0, 0},
                                    {0, 0, 0, 0, 4, 0, 0, 0},
                                    {3, 0, 1, 0, 0, 0, 0, 1},
                                    {0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 1, 0, 1},
                                    {1, 0, 0, 0, 2, 0, 0, 0},
                                    {3, 0, 0, 0, 3, 0, 0, 0},
                                    {3, 0, 0, 0, 0, 0, 0, 2},
                                    {2, 0, 0, 0, 2, 0, 0, 0},
                                    {4, 0, 0, 3, 0, 1, 0, 1},
        };
    }

    public void IncreasePowerAmount(Power power)
    {
        powersAmount[lastPlayedLevel, (int)power]++;
    }

    public void StartGame()
    {
        countingGameTime = true;
    }

    public void SavePlayerProgress()
    {
        SaveGameInCloud();
        AEDatabase.SetInt("gameLaunchCounter", gameLaunchCounter);
        AEDatabase.SetBool("rateWindowVisible", rateWindowVisible);
        AEDatabase.SetBool("gameInstalled", gameInstalled);
        AEDatabase.SetBool("soundsMuted", soundsMuted);
        AEDatabase.SetBool("musicMuted", musicMuted);
        AEDatabase.SetStringArray("standardAchievements", standardAchievements.ToArray());
        AEDatabase.SetStringArray("incrementalAchievements", incrementalAchievements.ToArray());
    }

    void LoadGameSettings()
    {
        soundsMuted = AEDatabase.HasKey("soundsMuted") ? AEDatabase.GetBool("soundsMuted") : false;
        musicMuted = AEDatabase.HasKey("musicMuted") ? AEDatabase.GetBool("musicMuted") : false;
    }

    void LoadPlayerProgress()
    {
        gameLaunchCounter = AEDatabase.HasKey("gameLaunchCounter") ? AEDatabase.GetInt("gameLaunchCounter") : 0;
        rateWindowVisible = AEDatabase.HasKey("rateWindowVisible") ? AEDatabase.GetBool("rateWindowVisible") : false;
        standardAchievements = new List<string>(AEDatabase.GetStringArray("standardAchievements"));
        incrementalAchievements = new List<string>(AEDatabase.GetStringArray("incrementalAchievements"));
        gameLaunchCounter++;
        gameDataLoaded = true;
    }

    public void SaveGameInCloud()
    {
        if (userSignedIn)
        {
            // Save game callback
            Action<SavedGameRequestStatus, ISavedGameMetadata> writeCallback =
                (SavedGameRequestStatus status, ISavedGameMetadata game) =>
                {

                };

            // Read binary callback
            Action<SavedGameRequestStatus, byte[]> readBinaryCallback =
                (SavedGameRequestStatus status, byte[] data) =>
                {

                };

            // Read game callback
            Action<SavedGameRequestStatus, ISavedGameMetadata> readCallback =
                (SavedGameRequestStatus status, ISavedGameMetadata game) =>
                {

                    // Check if read was successful
                    if (status == SavedGameRequestStatus.Success)
                    {

                        currentGame = game;
                        PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(game, readBinaryCallback);
                    }
                };

            // Create new save data
            SaveData saveData = new SaveData
            {
                gameLaunchCounter = gameLaunchCounter,
                rateWindowVisible = rateWindowVisible,
                standardAchievements = standardAchievements,
                incrementalAchievements = incrementalAchievements

            };

            // Replace "MySaveGame" with whatever you would like to save file to be called
            ReadSaveGame("MySaveGame", readCallback);
            WriteSaveGame(currentGame, SaveData.ToBytes(saveData), writeCallback);
        }
    }

    public void LoadGameFromCloud()
    {
        // Check if signed in
        if (userSignedIn)
        {

            // Read binary callback
            Action<SavedGameRequestStatus, byte[]> readBinaryCallback =
                (SavedGameRequestStatus status, byte[] data) =>
                {

                    // Check if read was successful
                    if (status == SavedGameRequestStatus.Success)
                    {

                        // Load game data
                        try
                        {
                            SaveData saveData = SaveData.FromBytes(data);

                            gameLaunchCounter = saveData.gameLaunchCounter;
                            rateWindowVisible = saveData.rateWindowVisible;
                            standardAchievements = saveData.standardAchievements;
                            incrementalAchievements = saveData.incrementalAchievements;
                            gameLaunchCounter++;
                            gameDataLoaded = true;
                            Debug.Log("load from cloud");
                        }

                        catch (Exception e)
                        {

                            Debug.LogError("Failed to read binary data: " + e.ToString());
                            LoadPlayerProgress();
                        }
                    }
                    else
                    {
                        LoadPlayerProgress();

                    }
                };

            // Read game callback
            Action<SavedGameRequestStatus, ISavedGameMetadata> readCallback =
                (SavedGameRequestStatus status, ISavedGameMetadata game) =>
                {

                    // Check if read was successful
                    if (status == SavedGameRequestStatus.Success)
                    {

                        currentGame = game;
                        PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(game, readBinaryCallback);
                    }
                    else
                    {
                        LoadPlayerProgress();
                    }
                };

            // Replace "MySaveGame" with whatever you would like to save file to be called
            ReadSaveGame("MySaveGame", readCallback);
        }
        else
        {
            LoadPlayerProgress();
        }
    }

    void ReadSaveGame(string filename, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {
        if (userSignedIn)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, callback);
        }
    }

    // Write the save game
    void WriteSaveGame(ISavedGameMetadata game, byte[] savedData, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {
        if (userSignedIn)
        {
            SavedGameMetadataUpdate updatedMetadata = new SavedGameMetadataUpdate.Builder()
                .WithUpdatedPlayedTime(TimeSpan.FromMinutes(game.TotalTimePlayed.Minutes + 1))
                .WithUpdatedDescription("Saved at: " + DateTime.Now)
                .Build();

            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.CommitUpdate(game, updatedMetadata, savedData, callback);
        }

    }

    public void UnlockOfflineAchievements()
    {
        for (int i = 0; i < standardAchievements.Count; i++)
        {
            Functions.UnlockAchievement(standardAchievements[i], i);
        }
        for (int i = 0; i < incrementalAchievements.Count; i++)
        {
            Functions.UnlockIncrementalAchievement(incrementalAchievements[i], 1, i);
        }
    }

    public void LoginAndUnlockOfflineAchievements()
    {
        StartCoroutine(TryLoginWhenInternet());
    }

    IEnumerator TryLoginWhenInternet()
    {
        gameInstalled = AEDatabase.HasKey("gameInstalled") ? AEDatabase.GetBool("gameInstalled") : false;
        if (gameInstalled)
        {
            Debug.Log("game installed load locally");
            if (!gameLaunched)
            {
                gameLaunched = true;
                LoadPlayerProgress();
            }

            while (!gameCanBeLoaded)
                yield return null;
            if (!userSignedIn && internetAvailable)
            {
                Social.localUser.Authenticate((bool succes) =>
                {
                    if (succes)
                    {
                        userSignedIn = true;
                        Debug.Log("user logged");
                        UnlockOfflineAchievements();
                    }
                    else
                    {
                        Debug.Log("user not logged");
                        userSignedIn = false;
                    }
                });
            }
            else if (userSignedIn)
                UnlockOfflineAchievements();
        }
        else
        {
            while (!gameCanBeLoaded)
                yield return null;

            if (internetAvailable)
            {
                Debug.Log("trying to login");
                if (!userSignedIn)
                    Social.localUser.Authenticate((bool succes) =>
                    {
                        if (succes)
                        {
                            userSignedIn = true;
                            Debug.Log("user logged");
                            UnlockOfflineAchievements();
                            if (!gameLaunched)
                            {
                                gameLaunched = true;
                                LoadGameFromCloud();
                            }
                            gameInstalled = true;
                        }
                        else
                        {
                            Debug.Log("user not logged");
                            userSignedIn = false;
                            if (!gameLaunched)
                            {
                                gameLaunched = true;
                                LoadPlayerProgress();
                            }
                            gameInstalled = true;
                        }
                    });
                else
                {
                    Debug.Log("user already logged");
                    UnlockOfflineAchievements();
                    if (!gameLaunched)
                    {
                        gameLaunched = true;
                        LoadPlayerProgress();
                    }
                    gameInstalled = true;
                }

            }
            else
            {
                Debug.Log("no internet");
                if (!gameLaunched)
                {
                    gameLaunched = true;
                    LoadPlayerProgress();
                }
                gameInstalled = true;
            }
        }
    }

    void CheckInternetConnectionOnStart()
    {
        StartCoroutine(Functions.CheckInternetConnection((isConnected) =>
        {
            if (isConnected)
            {
                internetAvailable = true;
                gameCanBeLoaded = true;
                InitPlayServices();
            }
            else
            {
                internetAvailable = false;
                gameCanBeLoaded = true;
            }
        }));
    }

    void InitPlayServices()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .EnableSavedGames()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    void OnApplicationQuit()
    {
        SavePlayerProgress();
    }
}
