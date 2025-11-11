using UnityEngine;
using UnityEngine.UI;



public class TurnMarkObject : MonoBehaviour
{
    [SerializeField] Image Bg_Image;

    [SerializeField] Image[] Char_Image;


    [SerializeField] AnimationClip BaseEnemy;
    [SerializeField] AnimationClip MidBossEnemy;
    [SerializeField] Animation Animation;



    private void OnEnable()
    {
       

        Sprite[] sprites = new Sprite[GameManager.instance.EnemysGroup.Enemys.Count];

        for (int i = 0; i < GameManager.instance.EnemysGroup.Enemys.Count; i++)
        {
            sprites[i] = GameManager.instance.EnemysGroup.Enemys[i].EnemyData.Enemy_Sprite;
            Animation.clip = BaseEnemy;

            if (sprites[i].name == "middle_boss_portrait") Animation.clip = MidBossEnemy;
        }

       

        
        SetUpImage(sprites);
    }
    public void SetUpImage(Sprite[] sprites)
    {
        if (Char_Image.Length! == 1) return;
        


        for (int i = 0; i < 3; i++)
        {
            Char_Image[i].color = new Color(0, 0, 0, 0);
        }


        int formIndex = 3- sprites.Length;

        for (int i = 2; i >= formIndex ; i-- )
        {
           

            Char_Image[i].sprite = sprites[i - formIndex];
            Char_Image[i].color = Color.white;
            Char_Image[i].SetNativeSize();
        }
        Animation.Play();
    }

    public void SetUpImage(Sprite sprite)
    {
        Char_Image[Char_Image.Length - 1].sprite = sprite;
    }


    public void HidenImage(int index)
    { 
    
    }

}
