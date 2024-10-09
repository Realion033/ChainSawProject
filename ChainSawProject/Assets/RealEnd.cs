using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealEnd : MonoBehaviour
{
    private MainGameManager _mainGameManager;
    private Transform _playerT;
    private PlayerDash _player;
    public List<LivingEntity> livingEntities; // List to store all LivingEntity objects

    private void Awake()
    {
        _mainGameManager = GameObject.Find("GameOwner").GetComponent<MainGameManager>();
        _playerT = GameObject.FindGameObjectWithTag("KPlayer").transform;
        _player = _playerT.GetComponent<PlayerDash>();

        // Find all LivingEntity objects in the scen
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KPlayer"))
        {
            // Check if all LivingEntities are dead
            if (AllEnemiesAreDead())
            {
                _mainGameManager.Thanks();
            }
        }
    }

    // Method to check if all LivingEntities are dead
    private bool AllEnemiesAreDead()
    {
        foreach (LivingEntity entity in livingEntities)
        {
            if (!entity.isDead) // Check if any LivingEntity is still alive
            {
                return false;
            }
        }
        return true; // Return true if all LivingEntities are dead
    }
}
