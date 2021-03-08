using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Text timer;
    public Text waveNumber;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image[] hearts;
    public Text warnings;
    public Text cageWarning;
    public GameObject escape;
    private bool isEsc = false;
    public GameObject finish;

    private float timeRemaining;
    public int maxHearth;
    public int nowHearth;
    private bool canRestart = false;

    private void Start(){
        nowHearth = maxHearth;
        timer.gameObject.SetActive(false);
        waveNumber.text = "Wave 1";
    }

    private void FixedUpdate(){
        timeRemaining -= Time.deltaTime;
        timer.text = timeRemaining.ToString("#.");
        if(timeRemaining <= 0){
            StopTimer();
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            isEsc = !isEsc;
            escape.SetActive(isEsc);
        }

        if(Input.GetKeyDown(KeyCode.R) && canRestart){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void ExitGame(){
        Application.Quit();
    }

    public void NextWave(int newWaveNumber){
        waveNumber.text = "Wave " + newWaveNumber.ToString();
    }

    public void StartTimer(float startTime){
        timeRemaining = startTime;
        timer.gameObject.SetActive(true);
        timer.text = startTime.ToString();
    }

    private void StopTimer(){
        //Animation
        timer.gameObject.SetActive(false);
    }

    public void AddHearth(){
        maxHearth++;
        nowHearth++;
        hearts[maxHearth-1].gameObject.SetActive(true);
        for(int i=maxHearth-1;i>0;i--){
            hearts[i].sprite = hearts[i-1].sprite;
        }
        hearts[0].sprite = fullHeart;
    }

    public void ClearOneHearth(){
        nowHearth--;
        hearts[nowHearth].sprite = emptyHeart;
    }

    public void SetFullHearths(){
        nowHearth = maxHearth;
        for(int i=0; i<maxHearth;i++){
            hearts[i].sprite = fullHeart;
        }
    }

    public void SetWarning(string warningText){
        warnings.gameObject.SetActive(true);
        warnings.text = warningText;
    }
    public void DisableWarning(){
        warnings.gameObject.SetActive(false);
    }

    public void SetCageWarning(string warningText){
        cageWarning.gameObject.SetActive(true);
        cageWarning.text = warningText;
        Invoke("DisableCageWarning", 5f);
    }
    public void DisableCageWarning(){
        cageWarning.gameObject.SetActive(false);
    }

    public void Escaped(){
        finish.SetActive(true);
        canRestart = true;
    }
}
