using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{

    public GameState currentGameState = GameState.menu;
    public static GameManager shareInstance;
    // Start is called before the first frame update
    private PlayerController controller;

    public int collectableObject = 0;

    private void Awake() {
        if (shareInstance == null) {
            shareInstance = this;
        }    
    }

    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
    
        if(Input.GetButtonDown("Submit") && currentGameState != GameState.inGame){
            StartGame();
            Debug.Log("test");
        }
    }

    public void StartGame(){
        SetGameState(GameState.inGame);
    }

    public void GameOver(){
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu(){
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState){
        if (newGameState == GameState.menu){
            MenuManager.sharedInstance.ShowMainMenu();
           
        }else if (newGameState == GameState.inGame){
            LevelManager.sharedInstance.RemoveAllLevelBlocks();
             LevelManager.sharedInstance.GenerateInitialBlocks();
             MenuManager.sharedInstance.HideMainMenu();
             controller.StartGame();
        }else if (newGameState == GameState.gameOver){
            GameOverManager.sharedInstance.ShowGameOverCanvas();
        }

        this.currentGameState = newGameState;
    }

   public void CollectableObject(Collectables collectable){
       collectableObject+= collectable.value;
   }
}
