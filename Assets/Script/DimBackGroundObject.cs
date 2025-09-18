using UnityEngine;

public class DimBackGroundObject : MonoBehaviour
{
    //카드선택시 배경이 어두어지고 사용가능한 유닛만 원래 색으로 보이는 기능

    [SerializeField]SpriteRenderer EnemyDis;
    [SerializeField]SpriteRenderer PlayerDis;
    

    private void Start()
    {
        this.gameObject.SetActive(false);
    }


    public void SetActiveDim(string code)
    {
        this.gameObject.SetActive(true);

        EnemyDis.gameObject.SetActive(true);
        PlayerDis.gameObject.SetActive(true);
       

        if (code == "Enemy") //Enemy쪽이 어두어짐
        {
            EnemyDis.sortingOrder = 5;
            PlayerDis.sortingOrder = 1;
        }

        if (code == "Player") // Player쪽이 어두어짐
        {
            EnemyDis.sortingOrder = 1;
            PlayerDis.sortingOrder = 5;
        }

        if (code == "All") // Player쪽이 어두어짐
        {
            EnemyDis.sortingOrder = 1;
            PlayerDis.sortingOrder = 1;
        }
    }

    
}
