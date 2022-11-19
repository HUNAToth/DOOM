using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnvironmentDamagePlayer : MonoBehaviour
    {
        public int DamageValue = 5;

        float Timer = 0f;

        public float TimerMax = 2.0f;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.transform.tag == "Player")
            {
                if (Timer <= 0)
                {
                    other.GetComponent<PlayerStats>().TakeDamage(DamageValue);
                    Timer = TimerMax;
                }
                else
                {
                    Timer -= Time.deltaTime;
                }
            }
        }
    }
}
