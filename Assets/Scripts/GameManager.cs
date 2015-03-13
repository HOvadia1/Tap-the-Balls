﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject ball;
    public Text timer;
    public Text ballsLeft;
    public float time;

    [HideInInspector]
    public int level;

    public int ballModifier;
    public float timeModifier;

    [HideInInspector]
    public int bPL;

    int currentBalls;

    void Awake()
    {
        level = 1;
        bPL = level * ballModifier;
        time = bPL * timeModifier;
    }

    public void DrawBalls()
    {
        for (int i = 0; i < bPL; i++)
        {
            GameObject b = (GameObject)Instantiate(ball, new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-1.7f, 2.77f), 0.0f), Quaternion.identity);
            b.name = "ball " + i;
            b.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        }
        time = bPL * timeModifier;

        currentBalls = bPL;
    }

    public void NewLevel()
    {
        level++;
        bPL = level * ballModifier;
        DrawBalls();
    }

    void Start()
    {
        DrawBalls();
    }

    void Update()
    {
        bPL = level * ballModifier;

        time -= Time.deltaTime;

        float seconds = time % 60;
        if (time > 0)
            timer.text = string.Format("{0:00}", seconds);

        //Mouse Logic
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if(hit.collider)
            {
                if(hit.collider.tag == "ball")
                {
                    Destroy(hit.collider.gameObject);
                    currentBalls--;
                }
            }
        }

        if(currentBalls == 0)
        {
            NewLevel();
        }

        ballsLeft.text = "Balls Left: " + currentBalls;
    }
}