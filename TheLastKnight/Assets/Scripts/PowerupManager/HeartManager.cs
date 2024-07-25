using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;
    [SerializeField] int initialNumberOfHeartContainers;

    // Start is called before the first frame update
    void Start()
    {
      
        if (PlayerController.GetIsNextLevel() && PlayerController.GetRespawnCount() == 0 )
        {   // need to keep the number of health containers, and also keep the health value 
            Debug.Log("if called in heart manager"); 
            PlayerController.SetIsNextLevel(false);
        } 
        else if (PlayerController.GetRespawnCount() == 0)
        {
            // reset number of heart container to default 
            Debug.Log("elif called in heart manager"); 
            heartContainers.RuntimeValue = initialNumberOfHeartContainers;
            playerCurrentHealth.RuntimeValue = playerCurrentHealth.initialValue;
        }
        else
        {
            // reset player health to full upon respawn. no change in heart containers
            Debug.Log("else called in heart manager"); 
            playerCurrentHealth.RuntimeValue = heartContainers.RuntimeValue * 2f;
        }

        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if (i <= tempHealth - 1)
            {
                // Full Heart
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                // Empty Heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                // Half full heart
                hearts[i].sprite = halfFullHeart;
            }
        }
    }

    public void IncreaseHeartContainers(int amount)
    {
        heartContainers.RuntimeValue += amount;
        playerCurrentHealth.RuntimeValue += amount * 2; // Increase current health accordingly
        InitHearts();
    }
}
