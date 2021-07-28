using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int maxHealth,currentHealth;
    private void Awake()
    {
        instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UIContorller.instance.playerSlider.maxValue = maxHealth;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerDamage(int damageCount)
    {
        currentHealth -= damageCount;
        if (currentHealth <= 0)
        {
            //gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        UIContorller.instance.playerSlider.value = currentHealth;
        UIContorller.instance.healthText.text = "Health" + currentHealth;
    }
}
