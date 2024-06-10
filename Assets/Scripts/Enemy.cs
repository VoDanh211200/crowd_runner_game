using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State { Idle, Running }

    public float searchRadius;
    public float moveSpeed;

    public static Action onRunnerDie;

    private State state;
    private Transform targetRunner;

    void Start()
    {
        state = State.Idle;
    }

    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                SearchForTarget();
                break;
            case State.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void RunTowardsTarget()
    {
        if (targetRunner == null) return;
        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetRunner.position) < .1f)
        {
            onRunnerDie?.Invoke();
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }

    private void SearchForTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                {
                    continue;
                }
                runner.SetTarget();
                targetRunner = runner.transform;
                StartRunningTowardsTarget();
            }
        }
    }

    private void StartRunningTowardsTarget()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Fast Run");
    }
}
