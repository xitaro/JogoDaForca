using UnityEditorInternal;
using UnityEngine;

public class AdmAccess : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Adm adm;

    [Header("UI")]
    [SerializeField] private GameObject panelAdm;

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKey(KeyCode.LeftControl)) && (Input.GetKey(KeyCode.LeftAlt)) && ((Input.GetKeyDown(KeyCode.A))))
        {
            SetAdm();
            panelAdm.SetActive(true);
            return;
            
        }
    }

    public void SetAdm()
    {
        adm.isAdm = !adm.isAdm;
    }
}
