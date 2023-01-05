using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TraceControl : MonoBehaviour
{
    public BoxCollider trigger;
    PlayerStat stat;
    Vector3 default_size;
  
    private void Awake()
    {
        stat = GetComponentInParent<PlayerStat>();
        trigger = this.gameObject.GetComponent<BoxCollider>();
    }
    
    private void Start()
    {
        default_size.Set(7.0f * stat.Stealth.GetStat() / 100, 0.2f, 7.0f * stat.Stealth.GetStat() / 100);

        ((BoxCollider)trigger).size = default_size;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponentInParent<Enemy_move>().trace = true;
        }

    }
    public void TriggerSizeUpdate()
    {
        ((BoxCollider)trigger).size = new Vector3(7.0f * stat.Stealth.GetStat() / 100, 0.2f, 7.0f * stat.Stealth.GetStat() / 100);
    }
}
