using UnityEngine;

// this class is used in the info cards, to keep them always face the player
public class LookAtPlayer : MonoBehaviour
{
    // the player
    public Transform player;

    private void Update()
    {
        // a simple lookat function can make it work well
        transform.LookAt(player);
    }
}
