using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f;           //Movement speed is 10m/s
    public float fireRate = 0.3f;       //Seconds/shot (Unused)
    public float health = 10;           //Damage needed to destroy this enemy
    public int score = 100;             //Points earned for destroying this

    //This is a Property: a method that acts like a field
    public Vector3 pos {
        get {
            return this.transform.position;
        }
        set {
            this.transform.position = value;
        }
    }

    void Update() {
        Move();
    }

    public virtual void Move() {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
}
