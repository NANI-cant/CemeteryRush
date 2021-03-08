using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject[] enemies;
    private float timeBetweenWaves;
    private Vector2 enemyPositionNow;
    private GameObject enemyNow;
    public string direction;
    public float spawnRange = 6.5f;
    public int enemiesCount = 20;
    public UIController _UI;
    public GameObject boss;
    public GameManager gameManager;


    public void Initialize(GameObject[] newEnemies, float newTimeBetweenWaves,float time){
        enemies = newEnemies;
        timeBetweenWaves = newTimeBetweenWaves;
        _UI.SetWarning("The wave is coming from the "+direction);
        Invoke("SpawnEnemies", time);
    }

    public void SpawnBoss(float newTimeBetweenWaves,float time){
        timeBetweenWaves = newTimeBetweenWaves;
        _UI.SetWarning("Boss!!!");
        Invoke("Boss",time);
    }

    private void Boss(){
        gameManager.SetWaveStarted();
        _UI.DisableWarning();
        Instantiate(boss, transform.position,Quaternion.identity);
    }

    private void SpawnEnemies(){
        gameManager.SetWaveStarted();
        _UI.DisableWarning();
        StartCoroutine("Co_WaitForSeconds");
    }

    private IEnumerator Co_WaitForSeconds(){
        for(int i=0;i<enemiesCount;i++){
            float randomRange = Random.Range(0,spawnRange);
            float randomAngle = Random.Range(0,360f)*Mathf.Deg2Rad;
            Vector2 enemyPosition = new Vector2(transform.position.x+randomRange*Mathf.Cos(randomAngle), transform.position.y + randomRange*Mathf.Sin(randomAngle));
            if(i%2 == 0){
                Instantiate(enemies[0],enemyPosition,Quaternion.identity);
            } else{
                Instantiate(enemies[1],enemyPosition,Quaternion.identity);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
