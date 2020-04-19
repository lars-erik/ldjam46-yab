using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private const int MaxHealth = 60;

    public GameObject HealthBar;

    private int health = MaxHealth;
    private float lastTick;
    private RectTransform healthTransform;

    public void AddHealth(int amount)
    {
        health = Math.Min(MaxHealth, health + amount);
        UpdateHealthBar();
    }

    // Start is called before the first frame update

    void Start()
    {
        lastTick = Time.time;
        healthTransform = (RectTransform)HealthBar.transform;
    }

    // Update is called once per frame

    void Update()
    {
        if (Time.time > lastTick + 1 && health > 0)
        {
            lastTick = Time.time;
            health--;

            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        healthTransform.sizeDelta = new Vector2(400f * health / MaxHealth, 40);
    }
}
