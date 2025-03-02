using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Email : MonoBehaviour
{

    [Header("Details")]
    public EmailSchema emailSchema;
    public bool isRead = false;


    [Header("UI")]
    public Image profileImage;
    public Image attachmentImage;
    public TextMeshProUGUI senderName;
    public TextMeshProUGUI senderEmail;
    public TextMeshProUGUI sendEmailWithAttachment;
    public TextMeshProUGUI subject;
    public TextMeshProUGUI body;
    public TextMeshProUGUI bodyWithAttachment;
    public GameObject EmailBG;
    public GameObject EmailBody;
    public GameObject EmailBodyWithAttachment;
    public GameObject ForwardMenu;
    public GameObject OpenButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        profileImage.sprite = emailSchema.profileImage;
        senderName.text = emailSchema.senderName;
        senderEmail.text = emailSchema.senderEmail;
        sendEmailWithAttachment.text = emailSchema.senderEmail;
        subject.text = emailSchema.subject;
        body.text = emailSchema.body;
        bodyWithAttachment.text = emailSchema.body;

        if (emailSchema.attachment != null) {
            attachmentImage.sprite = emailSchema.attachment;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRead) {
            EmailBG.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 1f);
        } else {
            EmailBG.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void ToggleBody() {
        OpenButton.transform.rotation *= Quaternion.Euler(0, 0, 180);
        isRead = true;
        RectTransform emailRect = EmailBG.GetComponent<RectTransform>();
        emailRect.sizeDelta = new Vector2(emailRect.sizeDelta.x, emailRect.sizeDelta.y == 100 ? 512 : 54);
        if (emailSchema.attachment != null) {
            EmailBodyWithAttachment.SetActive(!EmailBodyWithAttachment.activeSelf);
        } else {
            EmailBody.SetActive(!EmailBody.activeSelf);
        }
    }

    public void ToggleForwardMenu() {
        ForwardMenu.SetActive(!ForwardMenu.activeSelf);
    }
}
