using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public float expOnDeath;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public override void Die ()
    {
        player.AddExperience(expOnDeath);
        base.Die();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
