using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private int count;
    private int closestPickupIndex;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI closestPickupDistanceText;

    public static LineRenderer lineRenderer;
    public static GameObject[] pickups;

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        toggleGameInfoVisibility();

        count = 0;
        pickups = getPickups();
        findClosestPickup();

        winText.text = "";
        SetCountText();
    }

    private void Update()
    {
        findClosestPickup();
        toggleGameInfoVisibility();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            pickups = getPickups();
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        scoreText.text = "Score: " + count.ToString();
        if (pickups.Length == 0)
        {
            winText.text = "You Win!";
            // Pause game
            Time.timeScale = 0;
        }
    }

    private void findClosestPickup()
    {
        if (pickups.Length > 0 && Game.GameModeController.gameMode == Game.GameMode.DISTANCE)
        {
            float minDistance = float.MaxValue;
            for (int i = 0; i < pickups.Length; i++)
            {
                GameObject pickup = pickups[i];
                float distance = (pickup.transform.position - transform.position).magnitude;
                minDistance = Mathf.Min(minDistance, distance);
                if (minDistance == distance)
                {
                    closestPickupIndex = i;
                }
                pickup.GetComponent<Renderer>().material.color = Color.white;
            }
            pickups[closestPickupIndex].GetComponent<Renderer>().material.color = Color.blue;
            closestPickupDistanceText.text = "Distance: " + minDistance.ToString("0.00");

            // render line to closest pick up
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, pickups[closestPickupIndex].transform.position);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
    }

    private GameObject[] getPickups()
    {
        return GameObject.FindGameObjectsWithTag("PickUp");
    }

    private void toggleGameInfoVisibility()
    {
        lineRenderer.enabled = Game.GameModeController.gameMode != Game.GameMode.NORMAL;
        closestPickupDistanceText.enabled = Game.GameModeController.gameMode == Game.GameMode.DISTANCE;
    }
}
