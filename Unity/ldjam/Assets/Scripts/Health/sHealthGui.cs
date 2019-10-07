﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sHealthGui : MonoBehaviour {

    protected static sHealthGui singleton;



    private void Awake() {
        sHealthGui.singleton = this;
    }

    public static sHealthGui Access() {
        if (sHealthGui.singleton == null) throw new System.Exception("Health Gui singleton wasn't instanced, add it to the HealthGui-GO!");
        return sHealthGui.singleton;
    }



    [SerializeField] protected GameObject playerGui;
    [SerializeField] protected GameObject bossGui;



    private void Start() {
        this.ShowHealthPlayer(false);
        this.ShowHealthBoss(false);
    }



    public void SetHealthPlayer(float health, float before) {
        if (before == health + 1f) {
            setHeartEmpty(this.playerGui.transform.Find(before.ToString()).gameObject);
        }
    }

    public void SetHealthBoss(float health, float before) {
        if (before == health + 1f) {
            setHeartEmpty(this.bossGui.transform.Find(before.ToString()).gameObject);
        }
    }

    public void ShowHealthPlayer(bool show = true) {
        this.playerGui.SetActive(show);
    }

    public void ShowHealthBoss(bool show = true) {
        this.bossGui.SetActive(show);
    }



    protected void setHeartFull(GameObject heart) {
        heart.GetComponent<Animator>().Play("Idle");
    }

    protected void setHeartEmpty(GameObject heart) {
        heart.GetComponent<Animator>().Play("1to0");
    }

}
