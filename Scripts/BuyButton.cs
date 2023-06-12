using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public int value;

    public GameObject itemOnSale;

    [SerializeField]
    TextMeshPro priceText;

    [SerializeField]
    TextMeshPro buyText;

    Shop shop;

    Player player;

    GameObject playerGun;

    GameObject playerGunParent;

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = "$" + value;
        shop = transform.parent.gameObject.GetComponent<Shop>();
        player = shop.player;
        playerGun = shop.playerGun;
        playerGunParent = playerGun.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Click()
    {
        Debug.Log("Button Clicked" + value);
        if (value <= player.GetMoney() || value == 0)
        {
            playerGun = GameObject.Find("PlayerGun");
            player.SetMoney(player.GetMoney() - value);
            value = 0;
            priceText.text = "Owned";
            buyText.text = "EQUIP";
            GameObject give = GameObject.Instantiate(itemOnSale);
            give.transform.position = playerGun.transform.position;
            give.transform.rotation = playerGun.transform.rotation;

            give.transform.parent = playerGun.transform.parent;
            give.transform.localScale = playerGun.transform.localScale;
            give.name = "PlayerGun";
            Destroy (playerGun);
        }
    }
}
