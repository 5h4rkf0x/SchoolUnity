using UnityEngine;

public class PlusStateCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Player.instance.CurrentPlayerState == Player.PlayerStates.Minus)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Player.instance.TakeDamage(100);
        }
    }
}
