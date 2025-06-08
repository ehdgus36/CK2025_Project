using UnityEngine;

public class ShopEvent : MonoBehaviour
{
    [SerializeField] GameObject[] SoldOut;
    [SerializeField] GameObject Desc;

    // Update is called once per frame
    public void SoldOutEvent(int index)
    {
        SoldOut[index].SetActive(true);

    }

    public void BuyEvent(int coin)
    {
        int coins = 0;
        GameDataSystem.DynamicGameDataSchema.LoadDynamicData<int>(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, out coins);
        coins -= coin;

        GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.GOLD_DATA, coins);

    }
}
