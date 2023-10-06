using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private int count;
    private int closestPickupIndex;
    private GameObject[] pickups;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI closestPickupDistanceText;

    private void Start()
    {
        count = 0;
        pickups = getPickups();
        findClosestPickup();

        winText.text = "";
        SetCountText();
    }

    private void Update()
    {
        findClosestPickup();
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
        if (pickups.Length > 0)
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
        }
    }

    private GameObject[] getPickups()
    {
        return GameObject.FindGameObjectsWithTag("PickUp");
    }
}
