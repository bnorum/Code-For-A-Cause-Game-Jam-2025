using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Email : MonoBehaviour
{

    [Header("Details")]
    public EmailSchema emailSchema;
    public bool isRead = false;
    public bool isStarred = false;


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
    public GameObject StarButton;
    public Sprite starOn;
    public Sprite starOff;







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

        if (!EmailManager.Instance.allEmails.Contains(this)) {
            EmailManager.Instance.allEmails.Add(this);
        }
        if (!EmailManager.Instance.unreadEmails.Contains(this)) {
            EmailManager.Instance.unreadEmails.Add(this);
        }


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

        //DIRTY HACK!!!!!!!!!!
        //Basically, this email will disappear from the unread list once it is closed...
        if (isRead) {
            EmailManager.Instance.readEmails.Add(this);
            EmailManager.Instance.unreadEmails.Remove(this);
        }
        isRead = true;


        RectTransform emailRect = EmailBG.GetComponent<RectTransform>();
        emailRect.sizeDelta = new Vector2(emailRect.sizeDelta.x, emailRect.sizeDelta.y == 54 ? 512 : 54);
        if (emailSchema.attachment != null) {
            EmailBodyWithAttachment.SetActive(!EmailBodyWithAttachment.activeSelf);
        } else {
            EmailBody.SetActive(!EmailBody.activeSelf);
        }
    }

    public void ToggleStar() {
        isStarred = !isStarred;
        StarButton.GetComponent<Image>().sprite = isStarred ? starOn : starOff;

        if (EmailManager.Instance.starredEmails.Contains(this)) {
            EmailManager.Instance.starredEmails.Remove(this);
        } else {
            EmailManager.Instance.starredEmails.Add(this);
        }
    }

    public void ToggleForwardMenu() {
        ForwardMenu.SetActive(!ForwardMenu.activeSelf);
    }
}
