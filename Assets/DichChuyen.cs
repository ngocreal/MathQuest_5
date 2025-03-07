using UnityEngine;

public class DichChuyen : MonoBehaviour
{
    [SerializeField] GameObject dichchuyen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(dichchuyen != null)
        { 
            transform.position = dichchuyen.GetComponent<CongDichChuyen>().GetDiemDichCHuyenDen().position;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CongDichChuyen"))
        {
            dichchuyen = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CongDichChuyen"))
        {
            dichchuyen = null;
        }
    }
}
