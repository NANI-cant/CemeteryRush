using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController _playerController;
    public EnemySpawner[] allSpawners;
    public Transform[] allCageSpawnPoints;
    public GameObject[] Wave1;
    public GameObject[] Wave2;
    public GameObject[] Wave3;
    public int waveNumber = 1;
    public float timeBetweenWaves = 20.0f;
    public int enemiesAlive = 0;
    public UIController UI;
    public int personLevel = 1;
    public float damageBoost = 0;
    public float maxHealth = 3;
    public GameObject deathPanel;
    public GameObject RToRestart;
    public GameObject FadeInPanel;
    public GameObject FadeOutPanel;
    public GameObject cageObject;

    private bool isWaveStarted = false;
    private bool canRestart = false;
    public List<Transform> playerAndClones;

    private void Start(){
        FadeOutPanel.SetActive(true);
        TimerStart();
        StartWave();
    }

    private void FixedUpdate(){
        if(Input.GetKeyDown(KeyCode.R) && canRestart){
            FadeInPanel.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(isWaveStarted){
            ChechCountOfAliveEnemies();
        }
    }

    private void TimerStart(){
        UI.StartTimer(timeBetweenWaves);
    }

    private void StartWave(){
        _playerController.FullHeal();
        //isWaveStarted = true;
        enemiesAlive = 0;
        int randomSpawner = Random.Range(0,allSpawners.Length);
        switch (waveNumber){
            case 1: allSpawners[randomSpawner].Initialize(Wave1,timeBetweenWaves,timeBetweenWaves);
            break;
            case 2: allSpawners[randomSpawner].Initialize(Wave2,timeBetweenWaves,timeBetweenWaves);
            break;
            case 3: allSpawners[randomSpawner].Initialize(Wave3,timeBetweenWaves,timeBetweenWaves);
            break;
            case 4: allSpawners[4].SpawnBoss(timeBetweenWaves, timeBetweenWaves);
            break;
            case 5: FinishGame();
            break;
        }
    }

    public void SetWaveStarted(){
        isWaveStarted = true;
    }

    private void FinishGame(){
        UI.Escaped();
    }

    public void EnemyCreated(){
        enemiesAlive++;
    }
    public void EnemyKilled(){
        enemiesAlive--;
        //if(enemiesAlive <= 0 && isWaveStarted){
        //    isWaveStarted = false;
        //    CreateCage();
        //    waveNumber++;
        //    UI.NextWave(waveNumber);
        //    TimerStart();
        //    StartWave();
        //}
    }

    private void CreateCage(){
        int i = Random.Range(0,allCageSpawnPoints.Length);
        switch (i)
        {
            case 0:UI.SetCageWarning("A skeleton awaits release in the NW");
            break;
            case 1:UI.SetCageWarning("A skeleton awaits release in the NE");
            break;
            case 2:UI.SetCageWarning("A skeleton awaits release in the SE");
            break;
            case 3:UI.SetCageWarning("A skeleton awaits release in the SW");
            break;
            case 4:UI.SetCageWarning("A skeleton awaits release in the center");
            break;
        }
        Instantiate(cageObject,allCageSpawnPoints[i].position,Quaternion.identity);
    }

    public void PlayerTakeDamage(){
        UI.ClearOneHearth();
    }
    public void PlayerBoost(){
        UI.AddHearth();
    }
    public void PlayerFullHeal(){
        UI.SetFullHearths();
    }

    public void EndGame(){
        canRestart = true;
        deathPanel.SetActive(true);
        RToRestart.SetActive(true);
    }

    private void ChechCountOfAliveEnemies(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length==0){
            isWaveStarted = false;
            CreateCage();
            waveNumber++;
            UI.NextWave(waveNumber);
            TimerStart();
            StartWave();
        }
    }
}
