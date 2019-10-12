using UnityEngine.Events;

[System.Serializable]
public class GameScore
{
    public int points = 0;
    public bool finished = false;
}

[System.Serializable]
public class GameScoreEvent : UnityEvent<GameScore> { }
