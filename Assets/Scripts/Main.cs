using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static private Main S;      //private singleton for Main

    [Header("Inscribed")]
    public GameObject[] prefabEnemies;          //Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f;    //# Enemies spawn/second
    public float enemyInsetDefault = 1.5f;      //Inset from the sides
    public float gameRestartDelay = 2;

    private BoundsCheck bndCheck;

    void Awake() {
        S = this;
        //Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();

        //Invoke SpawnEnemy() once
        Invoke( nameof(SpawnEnemy), 1f/enemySpawnPerSecond );
    }

    public void SpawnEnemy() {
        //Pick a random Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>( prefabEnemies[ndx]);

        //Position the Enemy above the screen with a random x position
        float enemyInset = enemyInsetDefault;
        if(go.GetComponent<BoundsCheck>() != null) {
            enemyInset = Mathf.Abs( go.GetComponent<BoundsCheck>().radius );
        }

        //Set the initial position for the spawned enemy
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyInset;
        float xMax = bndCheck.camWidth - enemyInset;
        pos.x = Random.Range( xMin, xMax );
        pos.y = bndCheck.camHeight + enemyInset;
        go.transform.position = pos;

        //Invoke SpawnEnemy() again
        Invoke( nameof(SpawnEnemy), 1f/enemySpawnPerSecond );
    }

    void DelayedRestart() {
        //Invoke the Restart() method in gameRestartDelay seconds
        Invoke( nameof(Restart), gameRestartDelay );
    }

    void Restart() {
        //Reload the scene to restart
        SceneManager.LoadScene("__Scene_0");
    }

    static public void HERO_DIED() {
        S.DelayedRestart();
    }
}
