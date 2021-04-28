using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //PREFAB
    public GameObject ballPrefab;
    public GameObject paddlePrefab;

    //TEXT
    public Text scoreText;
    public Text ballsText;
    public Text levelText;
    public Text highScore;

    //GameObjects
    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject GameLevelCompleted;
    public GameObject GameOver;
    GameObject currentBall;





    //STATE
    public enum State { MENU, INIT, Play, LEVELCOMPLETED, LOADLEVEL, GAMEOVER }
    State _state;
    GameObject cLevel;
    bool _isSwitchingState;

    //ARRAY

    public GameObject[] levels;
    




    public static GameManager Instance
    {
        get;
        private set;
    }


    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
    }



    public void PlayClicked()
    {

        SwitchState(State.INIT);

    }



    private int _score;
    public int Score
    {
        get
        { return _score; }
        set
        {
            _score = value;
            scoreText.text = "Score " + _score;


        }


    }

    private int _level;
    public int Level
    {
        get
        { return _level; }
        set { _level = value;
            levelText.text = "Level " + _level;
        }


    }

    private int _ball;
    public int Ball
    {
        get
        { return _ball; }
        set { _ball = value;

            ballsText.text = "Ball " + _ball;
        }


    }

    public void SwitchState(State newstate, float delay = 0)

    {
        StartCoroutine(StateDelay(newstate, delay));

    }

    IEnumerator StateDelay(State newstate, float delay)
    {
        _isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newstate;
        BeginState(newstate);
        _isSwitchingState = false;



    }


    void BeginState(State newstate)
    {
        switch (newstate)
        {
            case State.MENU:
                Cursor.visible = true;
                highScore.text = "HIGHSCORE " + PlayerPrefs.GetInt("highscore");
                panelMenu.SetActive(true);
                break;

            case State.INIT:
                Cursor.visible = false;
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Ball = 3;
                if (cLevel != null)
                    Destroy(cLevel);

                Instantiate(paddlePrefab);
                SwitchState(State.LOADLEVEL);
                break;

            case State.LOADLEVEL:
                if (Level >= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    cLevel = Instantiate(levels[Level]);
                    SwitchState(State.Play);
                }
                break;

            case State.LEVELCOMPLETED:
                Destroy(currentBall);
                Destroy(cLevel);
                Level++;


                GameLevelCompleted.SetActive(true);
                SwitchState(State.LOADLEVEL, 2f);
                break;

            case State.Play:
                break;

            case State.GAMEOVER:
                if(Score>PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore",Score);
                }
                GameOver.SetActive(true);
                break;

           

        }
    }



        // Update is called once per frame
        void Update()
    {
        switch(_state)
        {
            case State.MENU:
                break;
            case State.LOADLEVEL:
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.Play:
                if(currentBall==null)
                {
                    if (Ball > 0)
                    {
                        currentBall = Instantiate(ballPrefab);

                    }
                    else
                        SwitchState(State.GAMEOVER);
                }
                if(cLevel!=null & cLevel.transform.childCount==0 && !_isSwitchingState)
                {
                    SwitchState(State.LEVELCOMPLETED);
                }
                break;
            case State.GAMEOVER:
                if(Input.anyKey)
                {
                    SwitchState(State.MENU);
                }
                break;
            case State.INIT:
                break;

        }

    }

   


    void EndState()
    {
        switch(_state)
        {
            case State.MENU:
                panelMenu.SetActive(false); 
                break;
            case State.LOADLEVEL:
                break;
            case State.LEVELCOMPLETED:
                GameLevelCompleted.SetActive(false);
                break;
            case State.Play:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                GameOver.SetActive(false);
                break;
            case State.INIT:
                break;

        }


    }

    



}
