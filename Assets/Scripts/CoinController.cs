using UnityEngine;

public class CoinController : MonoBehaviour
{

    public static CoinController instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        instance = this;
    }

    public int currentCoin;
    public CoinPickup coin;

    public void AddCoins(int coinsToAdd)
    {
        currentCoin += coinsToAdd;

        UiController.instance.UpdateCoins();

        SFXManager.instance.PlaySFXPitched(2);
    }

    public void DropCoin(Vector3 position, int value)
    {
        CoinPickup newCoin = Instantiate(coin, position + new Vector3(0.2f, 0.1f, 0f), Quaternion.identity);
        newCoin.coinAmount = value;
        newCoin.gameObject.SetActive(true);
    }

    public void SpendCoins(int coinsToSpend)
    {
        currentCoin -= coinsToSpend;

        UiController.instance.UpdateCoins();
    }
}
