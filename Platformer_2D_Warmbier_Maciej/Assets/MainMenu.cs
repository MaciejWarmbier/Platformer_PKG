using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] float waitTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            onLevel1ButtonPressed();
        }

    }

    IEnumerator StartGame(string levelName){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(levelName);
    }

    void onLevel1ButtonPressed(){
        StartCoroutine(StartGame("Level1"));
    }
}
