using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    GameObject portal;
    GameObject boss;
    GameObject player;
    int e;

    // Start is called before the first frame update
    void Start()
    {
        portal = GameObject.FindGameObjectWithTag("Portal");
        boss = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player");
        // Count enemies
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        e = enemies.Count;
        Debug.Log("Enemies: " + e);
    }

    // Update is called once per frame
    void Update()
    {
        // Change player health UI on load
        var thisplayer = player.GetComponent<RubyController>();
        UIHealthBar.instance.SetValue(thisplayer.health / (float)thisplayer.maxHealth);
        // If there is a boss in this scene
        if (boss)
        {
            var thisboss = boss.GetComponent<BossController>();
            if (thisboss.isKilled())
            {
                for (int i = 0; i < e; i++)
                {
                    var enemy = enemies[i].GetComponent<EnemyController>();
                    enemy.Fix();
                }
                // When the boss is killed
                Debug.Log("All Clear");
                var p = portal.GetComponent<Portal>();
                p.enable();
            }
        }

        else
        {
            for (int i = 0; i < e; i++)
            {
                var enemy = enemies[i].GetComponent<EnemyController>();
                // Reduce enemies count per dead
                if (enemy.isKilled())
                {
                    enemies.Remove(enemies[i]);
                    e = enemies.Count;
                    Debug.Log("Enemies: " + e);
                }
            }
            // If all the enemies is killed
            if (e <= 0)
            {
                Debug.Log("All Clear");
                var p = portal.GetComponent<Portal>();
                p.enable();
            }
        }
    }
}
