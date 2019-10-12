using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmusingCompetitionBehaviour : MonoBehaviour
{
    public CameraBehaviour mainCamera;
    public LevelSet levelSet;
    public PlayerBehaviour selectedPlayerPrefab;
    public int currentLevel = 0;

    protected GameRun currentGameRun = null;

    public UnityEvent OnGameStarted = new UnityEvent();
    public GameScoreEvent OnGameEnded = new GameScoreEvent();
    public GameScoreEvent OnShowEndScreen = new GameScoreEvent();
    public UnityEvent OnShowAd = new UnityEvent();

    protected enum State
    {
        uninitialized,
        waitForTap,
        playingGame,
        showingEndScreen,
        showingAd
    }

    protected State currentState = State.uninitialized;

    // Start is called before the first frame update
    void Start()
    {
        PrepareLevel();
    }

    protected void PrepareLevel()
    {
        if (currentState != State.uninitialized)
            return;

        currentGameRun = new GameObject("GameRun").AddComponent<GameRun>();
        currentGameRun.playerPrefab = selectedPlayerPrefab;
        currentGameRun.levelPrefab = levelSet.levels[currentLevel];
        currentGameRun.Prepare();
        mainCamera.FollowPlayer(currentGameRun.GetPlayerInstance());
        currentState = State.waitForTap;
    }

    public void StartLevel()
    {
        if (currentState == State.waitForTap)
        {
            currentGameRun.OnGameEnded.AddListener(OnGameFinished);
            currentGameRun.StartGame();
            OnGameStarted.Invoke();
        }
    }

    protected void OnGameFinished(GameScore gameScore)
    {
        currentGameRun.OnGameEnded.RemoveListener(OnGameFinished);
        currentState = State.showingEndScreen;
        OnShowEndScreen.Invoke(gameScore);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.waitForTap:
                CheckTap();
                break;
        }
    }

    protected void CheckTap()
    {
        bool wasTapped = false;
        wasTapped = Input.touchCount > 0;
        wasTapped = wasTapped ||Input.anyKey;

        if (wasTapped)
        {
            StartLevel();
        }

        }

}
