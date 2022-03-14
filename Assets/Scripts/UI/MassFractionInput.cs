using UnityEngine;
using UnityEngine.UI;

public class MassFractionInput : MonoBehaviour
{
    [SerializeField]
    InputField mass;

    public MassFraction Save()
    {
        return new MassFraction(int.Parse(mass.text), 1, 0, 0);
    }
}