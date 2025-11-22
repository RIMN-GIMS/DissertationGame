using UnityEngine;

public class DmgNumController : MonoBehaviour
{
    public static DmgNumController Instance;
    public DmgNum prefab;

    private void Awake()
    {
        // for scene transition
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
        }
    }
    public void create(float value , Vector3 location)
    {
        
    DmgNum damage = Instantiate(prefab, location ,transform.rotation,transform);
        damage.setValue(Mathf.RoundToInt(value));
    }

  
}
