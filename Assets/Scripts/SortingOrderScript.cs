using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderScript : MonoBehaviour
{
    #region Unity Variables
    public GameObject player;
    private SpriteRenderer sprite;
    #endregion

    #region Unity Methods
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (sprite.transform.position.y < player.transform.position.y)
        {
            sprite.sortingOrder = 1;
        }
        else if (sprite.transform.position.y > player.transform.position.y)
        {
            sprite.sortingOrder = 0;
        }
    }
    #endregion
}
