using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   public enum CollectableType
    {
        healthPotion,
        manaPotion,
        money
    }

public class Collectables : MonoBehaviour
{
    // Start is called before the first frame update
    public CollectableType type = CollectableType.money;

    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;

    bool hasBeenCollected = false;

    public int value = 1;

    GameObject player;

    private void Awake()
    {   
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    void Show(){
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasBeenCollected = false;
    }

    void Hide(){
        sprite.enabled = false;
        itemCollider.enabled = false;  
    }

    void Collect(){
        Hide();
        hasBeenCollected = true;

        switch (this.type)
        {
            case CollectableType.money:
                GameManager.shareInstance.CollectableObject(this);
                break;
            case CollectableType.healthPotion:
                player.GetComponent<PlayerController>().CollectHealth(this.value);
                break;
            case CollectableType.manaPotion:
                player.GetComponent<PlayerController>().CollectMana(this.value);
                break;
        }
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect(); 
        }
    }
}
