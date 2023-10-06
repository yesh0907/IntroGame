using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Vector3 _lastPos;
    public TextMeshProUGUI playerVelocityText;
    public TextMeshProUGUI playerPositionText;

    // Start is called before the first frame update
    void Start()
    {
        _lastPos = transform.position;
    }

    private void Update()
    {
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
        }
        playerVelocityText.text = "Velocity: " + magnitude.ToString("0.00");
        _lastPos = transform.position;
    }

    private void SetPlayerPosition()
    {
        playerPositionText.text = "Position: " + transform.position.ToString();
    }
}
