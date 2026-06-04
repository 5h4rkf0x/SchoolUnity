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
            Debug.Log(other + "와 충돌함");
            Player.instance.TakeDamage(100);
        }
    }
}
