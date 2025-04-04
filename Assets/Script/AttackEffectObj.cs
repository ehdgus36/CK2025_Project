using UnityEngine;
using UnityEngine.Events;

public class AttackEffectObj : MonoBehaviour
{
    [SerializeField]Transform Target;
    UnityAction DestroyEvet;
    [SerializeField] float speed;

    public void SetData(Transform target , UnityAction _event , Sprite sprite)
    {
        Target = target;
        DestroyEvet += _event;
        if (sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        if (Target.gameObject.activeSelf == false)
        {
            DestroyEvet?.Invoke();
            Destroy(this.gameObject);

            return;
        }



        if (transform.position == Target.position)
        {
            DestroyEvet?.Invoke();
            Destroy(this.gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(this.transform.position, Target.position, speed * Time.deltaTime);
    }
}
