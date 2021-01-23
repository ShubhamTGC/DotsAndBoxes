using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSQL;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProfileSetup : MonoBehaviour
{
    public SimpleSQLManager dbmanager;
    public GameObject cardPrefeb;
    public Transform cardHolder;
    public InputField Username;
    private List<GameObject> CardList= new List<GameObject>();
    [SerializeField] private string ImagePath;
    private Sprite[] Avatars;
    public Image UserImage;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if(CardList.Count == 0)
        {
            StartCoroutine(CreateAvatarList());
        }
    }
    IEnumerator CreateAvatarList()
    {
        Avatars = Resources.LoadAll<Sprite>(ImagePath);
        for(int a = 0; a < Avatars.Length; a++)
        {
            GameObject gb = Instantiate(cardPrefeb, cardHolder, false);
            gb.GetComponent<Image>().sprite = Avatars[a];
            gb.name = Avatars[a].name;
            gb.GetComponent<Button>().onClick.RemoveAllListeners();
            gb.GetComponent<Button>().onClick.AddListener(delegate { UserSelectedImage(); });
            CardList.Add(gb);
        }
        yield return new WaitForSeconds(0.1f);
    }

    void UserSelectedImage()
    {
        GameObject selectedimg = EventSystem.current.currentSelectedGameObject;
        UserImage.sprite = selectedimg.GetComponent<Image>().sprite;
    }
}
