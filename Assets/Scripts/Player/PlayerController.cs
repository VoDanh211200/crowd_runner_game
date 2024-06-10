using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float moveSpeed;
    public float slideSpeed;
    public CrowdSystem crowdSystem;
    public float roadWidth;
    public PlayAnimator playAnimator;

    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;
    private bool canMove;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameManager.onGameStateChanged += GameStateChangeCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangeCallback;
    }

    private void GameStateChangeCallback(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Game) StartMoving();
        else if (state == GameManager.GameState.Gameover || state == GameManager.GameState.LevelComplete) StopMoving();
    }

    void Update()
    {
        if (canMove)
        {
            MoveForward();
            ManageControl();
        }
    }

    private void StartMoving()
    {
        canMove = true;
        playAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;
        playAnimator.Idle();
    }

    private void MoveForward()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.forward;
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;
            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;
            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;
            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(),
                roadWidth / 2 - crowdSystem.GetCrowdRadius());
            transform.position = position;
        }
    }
}
