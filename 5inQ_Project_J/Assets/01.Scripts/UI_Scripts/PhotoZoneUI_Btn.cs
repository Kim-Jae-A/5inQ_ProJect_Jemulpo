using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class PhotoZoneUI_Btn : MonoBehaviour
{
    [Header("�Կ� ���")]
    [SerializeField] Text photoText;
    [SerializeField] Text videoText;

    [SerializeField] private GameObject CameraBtn; 
    [SerializeField] private GameObject RecordBtn; 
    [SerializeField] private GameObject RecordDoneBtn;
    [SerializeField] private GameObject SwipeAnime; //animator�� �������� ���� GameObject

    Animator animator;

    Color Highlightcolor = new Color(0, 0.6f, 1);
    Color Normalcolor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

    void Awake()
    {
        animator = SwipeAnime.GetComponent<Animator>();
    }

    void Start()
    {
        HighlightPhotoText(); //���� �ؽ�Ʈ ���̶���Ʈ
        NormalVideoText();    //���� �ؽ�Ʈ �븻
        CameraBtn.SetActive(true);
        RecordBtn.SetActive(false);
        RecordDoneBtn.SetActive(false);

    }



    public void OnPhotoBtn()
    {
        if (CameraMode.isPhoto == false)//�ִϸ��̼� �ݺ����� �ʱ� ���� ����
        {
            animator.SetTrigger("doRight"); //�ִϸ��̼� Ȱ��ȭ
        }

        //��ư�� ������ ���� ��尡 ������ �׿� �´� �ؽ�Ʈ ���� 
        CameraMode.isPhoto = true;
        CameraMode.isVideo = false;
        CameraMode.isRecord = false;
        CameraMode.isRecordDone = false;
        HighlightPhotoText();
        NormalVideoText();

        //���� ��� ��ư Ȱ��ȭ/ ���� ��� ��ư ��Ȱ��ȭ
        CameraBtn.SetActive(true);   
        RecordBtn.SetActive(false);
        RecordDoneBtn.SetActive(false);

    }

    public void OnVideoBtn()
    {
        if (CameraMode.isVideo == false)//�ִϸ��̼� �ݺ����� �ʱ� ���� ����
        {
            animator.SetTrigger("doLeft"); //�ִϸ��̼� Ȱ��ȭ
        }

        //��ư ������ ���� ��尡 ������ �׿� �´� �ؽ�Ʈ ����
        CameraMode.isVideo = true;
        CameraMode.isPhoto = false;
        CameraMode.isRecord = false;
        CameraMode.isRecordDone = false;

        HighlightVideoText();
        NormalPhotoText();

        //���� ��� ��ư Ȱ��ȭ/ ���� ��� ��ư ��Ȱ��ȭ
        CameraBtn.SetActive(false);
        RecordBtn.SetActive(true);
        RecordDoneBtn.SetActive(false);
    }

    public void OnGalleryBtn()
    {
        // READ_EXTERNAL_STORAGE ������ �̹� �ο��Ǿ����� Ȯ��
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageRead))
        {
            // ������ ���ٸ�, ���� ��û
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.ExternalStorageRead);
        }
        else
        {
            //�ȵ���̵� ����Ƽ�� ����� ����Ϸ��� AndroidJavaClass�� AndroidJavaObject�� ���
            AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");//UnityPlayer��� Ŭ������ �����ϴ� AndroidJavaClass ��ü�� ����
            AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");//���� ���� ���� ��Ƽ��Ƽ�� �����ϴ� AndroidJavaObject�� ��ȯ
            AndroidJavaObject packageManager = javaObject.Call<AndroidJavaObject>("getPackageManager");//�޼ҵ带 ȣ���Ͽ� ��Ű�� ������(���� ��ġ, ����, ���� ���� ����)��
                                                                                                       //�����ϴ� AndroidJavaObject�� �����´�. 
            AndroidJavaObject intent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", "com.sec.android.gallery3d"); //�ȵ���̵� ������ ��Ű����
            javaObject.Call("startActivity", intent);//������ ���� ����
        }
    }

    #region TextStyle
    //���̶���Ʈ�� ���� �ؽ�Ʈ�� �⺻ ������ �ؽ�Ʈ�� Photo�� Video�� ������ �ۼ�
    public void HighlightPhotoText()
    {
        photoText.fontStyle = FontStyle.Bold;
        photoText.DOColor(Highlightcolor, 0);
    }

    public void NormalPhotoText()
    {
        photoText.fontStyle = FontStyle.Normal;
        photoText.DOColor(Normalcolor, 0);
    }

    public void HighlightVideoText()
    {
        videoText.fontStyle = FontStyle.Bold;
        videoText.DOColor(Highlightcolor, 0);
    }

    public void NormalVideoText()
    {
        videoText.fontStyle = FontStyle.Normal;
        videoText.DOColor(Normalcolor, 0);
    }

    #endregion
}
