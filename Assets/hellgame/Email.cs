using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Email : MonoBehaviour
{


    public EmailSchema emailSchema;
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

    }

    public void ToggleBody() {
        RectTransform emailRect = EmailBG.GetComponent<RectTransform>();
        emailRect.sizeDelta = new Vector2(emailRect.sizeDelta.x, emailRect.sizeDelta.y == 100 ? 512 : 100);
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
