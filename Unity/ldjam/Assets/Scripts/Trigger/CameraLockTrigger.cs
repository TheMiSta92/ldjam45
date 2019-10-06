﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockTrigger : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] private float centerspeed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Camera cam = GameObject.FindObjectOfType<Camera>();
            CameraMovement move = cam.gameObject.GetComponent<CameraMovement>();
            move.DoFollow(true);
            move.DoFollowX(true);
            StartCoroutine(ResetYAfterAirborne(move));
        }
    }
    private IEnumerator ResetYAfterAirborne(CameraMovement move)
    {
        yield return new WaitWhile(()=>GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IsAirborne());
        move.DoFollowY(false);
    }
}
