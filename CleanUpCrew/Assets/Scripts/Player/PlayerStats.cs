using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private PlayerHUD hud;
    private UIManager ui;
    private Score score;
    private void Start()
    {
        GetRefrences();
        InitVariables();
    }
    private void GetRefrences()
    {
        hud = GetComponent<PlayerHUD>();
        ui = GetComponent<UIManager>();
        score = GetComponentInChildren<Score>();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health, maxHealth);
    }
    public override void Die()
    {
        base.Die();
        score.SaveTime();
        ui.SetActiveHud(false);
    }
}
