using TMPro;
using UnityEngine;

public class DmgNum : MonoBehaviour
{
    [SerializeField]
    private TMP_Text damageTxt;
    private float floatspeed;
    void Start()
    {
        floatspeed = Random.Range(0.1f, 1.5f);
        Destroy(gameObject, 1f); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * floatspeed;
    }
    public void setValue(int value)
    {
        damageTxt.text = value.ToString();
    }
}
