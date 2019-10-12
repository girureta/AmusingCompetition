using System;
using UnityEngine;

public class GameRun : MonoBehaviour
{
    public PlayerBehaviour playerPrefab;
    public LevelBehaviour levelPrefab;
  
    protected PlayerBehaviour playerInstance;
    protected LevelBehaviour levelInstance;

    public GameScoreEvent OnGameScoreUpdate = new GameScoreEvent();
    public GameScoreEvent OnGameEnded = new GameScoreEvent();

    [System.Serializable]
    public enum State
    {
        idle,
        running
    }

    protected State currentState = State.idle;

    //Current position of the player in composite bezier space
    protected float currentT = 0.0f;

    public void Prepare()
    {
        playerInstance = Instantiate(playerPrefab);
        levelInstance = Instantiate(levelPrefab);
    }

    public void StartGame()
    {
        currentState = State.running;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.idle:
                break;
            case State.running:
                UpdatePlayerPosition();
                break;
            default:
                break;
        } 
    }

    protected void UpdatePlayerPosition()
    {
        float desiredDisplacement = playerInstance.GetDisplacement();

        Vector3 playerPosition = playerInstance.transform.position;
        Quaternion playerRotation = playerInstance.transform.rotation;

        levelInstance.MoveOnTheLevel(desiredDisplacement, ref currentT, ref playerPosition, ref playerRotation);

        playerInstance.transform.position = playerPosition;
        playerInstance.transform.rotation = playerRotation;
    }
}
