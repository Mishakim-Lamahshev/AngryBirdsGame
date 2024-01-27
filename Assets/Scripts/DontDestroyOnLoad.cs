using System.Linq;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    void Awake()
    {
        Object.DontDestroyOnLoad(gameObject);
    }
}
