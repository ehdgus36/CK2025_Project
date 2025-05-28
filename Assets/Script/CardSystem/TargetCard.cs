using UnityEngine;

public class TargetCard : Card
{
    [SerializeField] int TargetIndex = 0;
    [SerializeField] Material[] TargetCard_Material;

    public virtual int GetTargetIndex() 
    {
        if (TargetIndex > GameManager.instance.EnemysGroup.Enemys.Count -1)
        {
            TargetIndex = GameManager.instance.EnemysGroup.Enemys.Count - 1;
            Initialized();
        }

        return TargetIndex; 
    }

    public override void Initialized()
    {
        if (TargetIndex != 1000)
        {

            if (TargetIndex > GameManager.instance.EnemysGroup.Enemys.Count - 1)
            {
                TargetIndex = GameManager.instance.EnemysGroup.Enemys.Count - 1;
            }
        }

        if (TargetCard_Material.Length != 0)
        {
            //GetComponent<MeshRenderer>().material = TargetCard_Material[TargetIndex];
        }

        switch (TargetIndex)
        {
            case 0:
                CardName = "1번 타겟";
                Example = "1번 위치 적 공격";
                SubExample = "1번 조준!";
                break;


            case 1:
                CardName = "2번 타겟";
                Example = "2번 위치 적 공격";
                SubExample = "2번 조준!";
                break;


            case 2:
                CardName = "2번 타겟";
                Example = "1번 위치 적 공격";
                SubExample = "2번 조준!";
                break;
        }
    }
}
