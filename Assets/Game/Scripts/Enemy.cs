using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Health;
    public float Damage;
    public TMP_Text HealthText;

    public void TakeDamage(float damage)
    {
        if (Health - damage <= 0)
            Destroy(gameObject);
        else
        {
            Health -= damage;
            HealthText.text = $"HP\n{Health}";
        }
    }
}
