using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int MaxHealth = 60;
    public int TickLength = 1;
    public int LossPerTick = 2;

    public GameObject HealthBar;

    public int Health;
    private float lastTick;
    private RectTransform healthTransform;

    public void AddHealth(int amount)
    {
        Health = Math.Min(MaxHealth, Health + amount);
        UpdateHealthBar();
    }

    // Start is called before the first frame update

    void Start()
    {
        Health = MaxHealth;
        lastTick = Time.time;
        healthTransform = (RectTransform)HealthBar.transform;
    }

    // Update is called once per frame

    void Update()
    {
        if (GameController.Instance.Pause)
        {
            return;
        }

        if (Time.time > lastTick + TickLength && Health > 0)
        {
            lastTick = Time.time;
            Health -= LossPerTick;

            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        healthTransform.sizeDelta = new Vector2(400f * Health / MaxHealth, 40);
    }
}
