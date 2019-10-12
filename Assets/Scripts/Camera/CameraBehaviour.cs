using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform pivot;
    public float speed = 1.0f;
    public float angularSpeed = 90.0f;
    protected PlayerBehaviour currentPlayer;

    public void FollowPlayer(PlayerBehaviour player)
    {
        currentPlayer = player;
    }

    public void Update()
    {
        if (currentPlayer != null)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            pivot.position = Vector3.MoveTowards(pivot.position, currentPlayer.transform.position, step);

            transform.rotation = Quaternion.LookRotation((currentPlayer.transform.position+ Vector3.up) - transform.position, Vector3.up);
        }
    }
}
