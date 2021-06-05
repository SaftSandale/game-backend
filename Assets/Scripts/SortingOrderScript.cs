using UnityEngine;

/// <summary>
/// SortingOrderScript passt die SortingOrder in Unity an, sodass Spieler und Gegenstände auf den richtigen Ebenen liegen, je nachdem wo der Spieler sich befindet.
/// </summary>
public class SortingOrderScript : MonoBehaviour
{
    #region Unity Variables

    public GameObject player;
    private SpriteRenderer sprite;
    #endregion

    #region Unity Methods

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
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
