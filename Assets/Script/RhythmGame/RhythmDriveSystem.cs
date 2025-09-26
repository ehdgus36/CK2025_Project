using UnityEngine;
using System.Collections;
using System;

public class RhythmDriveSystem : MonoBehaviour
{
    [SerializeField] GameObject TestEffect;

    public void Start()
    {
        GetComponent<RhythmInput>().SuccessNoteEvent += OnNoteSuccess;
    }



        //베리어는 원래 성공하면 해야함



    public void OnNoteSuccess(GameObject successNote)
    {
        int LoopCount = GameManager.instance.ExcutSelectCardSystem.UsedCard.Length;

        Vector3 PlayerPos = GameManager.instance.Player.transform.position;

        for (int i = 0; i < LoopCount; i++)
        {

            switch (GameManager.instance.ExcutSelectCardSystem.UsedCard[i])
            {
                //회복
                case "C1101":
                    StartCoroutine(MoveEffect(Instantiate(TestEffect).gameObject, successNote.transform.position, PlayerPos, "아무거나", () => { RecoverHp(5); } ));

                    break;

                case "C1102":
                    StartCoroutine(MoveEffect(Instantiate(TestEffect).gameObject, successNote.transform.position, PlayerPos, "아무거나", () => { RecoverHp(5); }));
                    break;

                //전체 데미지
                case "C2071":
                    StartCoroutine(MoveEffect(Instantiate(TestEffect).gameObject, successNote.transform.position, Vector3.zero, "아무거나", () => { ALLAttack(5); }));
                    break;

                case "C2072":
                    StartCoroutine(MoveEffect(Instantiate(TestEffect).gameObject, successNote.transform.position,Vector3.zero, "아무거나", () => { ALLAttack(5); }));

                    break;
            }
        }


        StartCoroutine(MoveEffect(Instantiate(TestEffect).gameObject, 
                                  successNote.transform.position, PlayerPos, "아무거나",
                                  () => { 
                                      GameManager.instance.Player.PlayerEffectSystem.PlayEffect("guitarwall_Effect", GameManager.instance.Player.transform.position);

                                      UnitData playerData;
                                      GameDataSystem.DynamicGameDataSchema.LoadDynamicData<UnitData>(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA,
                                                                                                     out playerData);
                                      playerData.CurrentBarrier += 1;

                                      GameDataSystem.DynamicGameDataSchema.UpdateDynamicDataBase(GameDataSystem.KeyCode.DynamicGameDataKeys.PLAYER_UNIT_DATA, playerData);

                                      Debug.Log("리듬게임 방어 상승");
                                  }));

    }

    void RecoverHp(int amount)
    {
        GameManager.instance.Player.PlayerEffectSystem.PlayEffect("bresth_Effect", GameManager.instance.Player.transform.position);
    }

    void ALLAttack(int amount)
    {
        Debug.Log("실행");
    }


    IEnumerator MoveEffect(GameObject moveEffect,Vector3 startPos , Vector3 TargetPos, string LastEffectCode , Action EndEvent)
    {
        moveEffect.transform.position = startPos;

        float t = 0;
        for (int i = 0; i < 20; i++)
        {
            t += 0.05f;
            moveEffect.transform.position = Vector3.Lerp(startPos, TargetPos, t);
            yield return new WaitForSeconds(0.01f);
        }

        EndEvent.Invoke();

        Destroy(moveEffect);
       
       

    }
}
