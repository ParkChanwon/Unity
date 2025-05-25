using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
		public Transform player;
        public float dist = 3.0f;
        public float height = 2.0f;
        public float dampTrace = 20.0f;

        void LateUpdate(){
            transform.position = Vector3.Lerp(transform.position, player.position -(player.forward*dist)+ (Vector3.up*height), Time.deltaTime*dampTrace);
            transform.LookAt(player);

        }
	}
