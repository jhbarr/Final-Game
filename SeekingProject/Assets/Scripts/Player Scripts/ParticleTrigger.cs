using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem ps;
    private Component component;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    public LayerMask enemyLayerMask;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        //List<ParticleSystem.Particle> enteredParticles = new List<ParticleSystem.Particle>();
        //int enterCount = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enteredParticles);

        ////GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //foreach (ParticleSystem.Particle particle in enteredParticles)
        //{
        //    Collider2D[] enemies = Physics2D.OverlapCircleAll(particle.position, particle.GetCurrentSize(ps) * 0.8f, enemyLayerMask);

        //    for (int i = 0; i < enemies.Length; i++)
        //    {
        //        enemies[i].GetComponentInParent<AgentHealth>().takeDamage(20);
        //    }
        //}


        component = ps.trigger.GetCollider(0);
        component.GetComponentInChildren<AgentHealth>().takeDamage(25);

        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(255, 0, 0, 255);
            enter[i] = p;
        }

        // Re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }

}
