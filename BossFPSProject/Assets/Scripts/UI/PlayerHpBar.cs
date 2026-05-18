using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar: MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    public void SetHP(float currentHP, float maxHP)
    {
        hpSlider.value = currentHP / maxHP;
    }
    
    public void CloseBossBar()
    {
        gameObject.SetActive(false);
    }
}