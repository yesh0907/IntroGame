using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Vector3 _lastPos;
    private int mostDirectPickupIndex;
    private Quaternion pickupRotation = Quaternion.Euler(new Vector3(45, 45, 45));

    public TextMeshProUGUI playerVelocityText;
    public TextMeshProUGUI playerPositionText;

    // Start is called before the first frame update
    void Start()
    {
        _lastPos = transform.position;
        mostDirectPickupIndex = 0;
        toggleGameInfoVisibility();
    }

    private void Update()
    {
        toggleGameInfoVisibility();

        SetPlayerVelocity();
        SetPlayerPosition();
    }

    private void SetPlayerVelocity()
    {
        float magnitude = 0f;
        if (Time.deltaTime > 0)
        {
            Vector3 velocity = (transform.position - _lastPos) / Time.deltaTime;
            magnitude = velocity.magnitude;
            renderVelocityVectorLine(velocity);
            findMostDirectPickup(velocity);
        }
        playerVelocityText.text = "Velocity: " + magnitude.ToString("0.00");
        _lastPos = transform.position;
    }

    private void SetPlayerPosition()
    {
        playerPositionText.text = "Position: " + transform.position.ToString();
    }

    private void renderVelocityVectorLine(Vector3 velocity)
    {
        if (Game.GameModeController.gameMode == Game.GameMode.VISION)
        {
            PlayerPickup.lineRenderer.SetPosition(0, transform.position);
            PlayerPickup.lineRenderer.SetPosition(1, transform.position + velocity);
            PlayerPickup.lineRenderer.startWidth = 0.1f;
            PlayerPickup.lineRenderer.endWidth = 0.1f;
        }
    }

    private void findMostDirectPickup(Vector3 velocity)
    {
        if (PlayerPickup.pickups.Length > 0 && Game.GameModeController.gameMode == Game.GameMode.VISION)
        {
            float maxDotProduct = float.MinValue;
            for (int i = 0; i < PlayerPickup.pickups.Length; i++)
            {
                GameObject pickup = PlayerPickup.pickups[i];
                Vector3 vectorToPickup = pickup.transform.position - transform.position;
                Vector3 normalizedVectorToPickup = vectorToPickup.normalized;
                float dotProduct = Vector3.Dot(velocity, normalizedVectorToPickup);
                maxDotProduct = Mathf.Max(maxDotProduct, dotProduct);
                if (maxDotProduct == dotProduct)
                {
                    mostDirectPickupIndex = i;
                }
                pickup.GetComponent<Renderer>().material.color = Color.white;
                pickup.transform.rotation = pickupRotation;
            }
            GameObject mostDirectPickup = PlayerPickup.pickups[mostDirectPickupIndex];
            mostDirectPickup.GetComponent<Renderer>().material.color = Color.green;
            mostDirectPickup.transform.LookAt(transform.position);
        }
    }

    private void toggleGameInfoVisibility() {
        playerVelocityText.enabled = Game.GameModeController.gameMode != Game.GameMode.NORMAL;
        playerPositionText.enabled = Game.GameModeController.gameMode != Game.GameMode.NORMAL;
    }
}
