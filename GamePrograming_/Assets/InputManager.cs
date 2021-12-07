using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject player;

    public Transform box;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        if (playerController == null) Debug.Log("ee");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                playerController.SetDestination(hit.point);
            }
        }
    }
}
