using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState{ 
    GS_PAUSEMENU, 
    GS_GAME, 
    GS_GAME_OVER,
    GS_LEVELCOMPLETED
}

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI enemyText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Canvas inGameCanvas;
    [SerializeField] Image[] keysTab;
    [SerializeField] Image[] heartsTab;
    public GameState currentGameState;
    public static GameManager instance;
    int coins;
    int keys=0;
    int enemies = 0;
    int hearts = 3;
    float timer = 0f;
    
    

    private void Awake() {
        instance = this;
        foreach(Image key in keysTab){
            key.color = Color.gray;
        }
        for(int i=hearts; i<heartsTab.Length;i++){
            heartsTab[i].enabled = false;
        }
    }
    void Start()
    {
        
        PauseMenu();
    }

     private void Update() {
         if(instance.currentGameState == GameState.GS_PAUSEMENU){
             if(Input.GetKeyDown(KeyCode.P)){
                 InGame();
             }
         }
         timer += Time.deltaTime;
         timeText.text = string.Format("{0:00}:{1:00}",timer/60, timer%60);
     }
    
    void SetGameState(GameState newGameState){
        currentGameState = newGameState;
    }

    void InGame(){
        inGameCanvas.enabled = true;
        SetGameState(GameState.GS_GAME);
    }

    void GameOver(){
        inGameCanvas.enabled = false;
        SetGameState(GameState.GS_GAME_OVER);
    }

    void PauseMenu(){
        inGameCanvas.enabled = false;
        SetGameState(GameState.GS_PAUSEMENU);
    }

    void LevelCompleted(){
        inGameCanvas.enabled = false;
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    public void AddCoins(int amount){
        coins += amount;
        if(coins<10){
            coinText.text = "0" + coins.ToString();
        }
        else {
            coinText.text = coins.ToString();
        }
    }
    public void AddKeys(){
        keysTab[keys].color = Color.white;
        keys += 1;
        
    }
    public void AddHearts(){
        heartsTab[hearts].enabled = true;
        hearts += 1;
    }
    public void DepleteHearts(){
        if(hearts >= 1){
        heartsTab[hearts-1].enabled = false;
        hearts -= 1;
        }
    }

    public void AddEnemies(){
        enemies += 1;
        enemyText.text = enemies.ToString();
    }
}
