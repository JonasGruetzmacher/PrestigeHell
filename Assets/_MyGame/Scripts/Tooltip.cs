using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

public class Tooltip : CUIComponent
{
    public CViewSO viewSO;

    public GameObject containerHeader;
    public GameObject containerContent;
    public int characterWrapLimit;

    private LayoutElement layoutElement;
    private VerticalLayoutGroup verticalLayoutGroup;
    private RectTransform rectTransform;

    private TextMeshProUGUI headerText;
    private TextMeshProUGUI contentText;

    private Canvas canvas;

    private Image imageHeader;
    private Image imageContent;

    [SerializeField] private string header;
    [SerializeField] private string content;


    public override void Setup()
    {

        headerText = containerHeader.GetComponentInChildren<TextMeshProUGUI>();
        contentText = containerContent.GetComponentInChildren<TextMeshProUGUI>();

        imageHeader = containerHeader.GetComponent<Image>();
        imageContent = containerContent.GetComponent<Image>();

        rectTransform = GetComponent<RectTransform>();
        layoutElement = GetComponent<LayoutElement>();
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();

        canvas = GetComponentInParent<Canvas>();


    } 

    public override void Configure()
    {
        verticalLayoutGroup.padding = viewSO.padding;
        verticalLayoutGroup.spacing = viewSO.spacing;

        imageHeader.color = viewSO.themeSO.secondary_bg;
        imageContent.color = viewSO.themeSO.primary_bg;

        if (string.IsNullOrEmpty(header))
        {
            containerHeader.SetActive(false);
        }
        else
        {
            containerHeader.SetActive(true);
            headerText.text = header;
        }

        contentText.text = content;

        int headerLength = headerText.text.Length;
        int contentLength = contentText.text.Length;

        // layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        if (!Application.isPlaying)
        {
            return;
        }


        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        canvas.transform as RectTransform,
        Input.mousePosition, canvas.worldCamera,
        out movePos);


        transform.position = canvas.transform.TransformPoint(movePos);




    }

    [Button]
    public void SetText(string content, string header = "")
    {
        this.header = header;
        this.content = content;

        Configure();   
    }

}
