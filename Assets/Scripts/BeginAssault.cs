using System;
using System.Collections;
using UnityEngine;

public class BeginAssault : MonoBehaviour
{
    [SerializeField] float timeBetweenLaunch = 3f;
    [SerializeField] GameObject[] projectiles; //array to hold game objects

    void OnEnable()
    {
        StartCoroutine(LaunchProjectiles());
    }

    private IEnumerator LaunchProjectiles()
    {
        // check to make sure projectiles still exists (not already destroyed)
        if ((projectiles == null) || (projectiles.Length == 0))
        {
            Debug.Log("All projectiles destroyed, ending assault...");
            yield break;
        }

        // otherwise continue assault:
        foreach (GameObject projectile in projectiles)
        {
            if (projectile != null)
            {
                projectile.SetActive(true);
                Debug.Log(projectile.gameObject.name + " activated.");
            }
            yield return new WaitForSeconds(timeBetweenLaunch);
        }
    }

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
    
    }
}
