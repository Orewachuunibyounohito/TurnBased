using System.Collections;
using MyScripts.Tween;
using UnityEngine;
using UnityEngine.UI;

public class HpBarView : MonoBehaviour
{
    [SerializeField] private Color normalColor  = new Color(0.06717692f, 0.6981132f, 0.2203813f, 1f);
    [SerializeField] private Color warningColor = new Color(1f, 0.6852201f, 0.435849f, 1f);

    public bool  UseDelay{ get; set; } = true;
    public float DelayBegin{ get; set; } = 1;

    public Image Bar{ get; private set; }
    public Image Delay{ get; private set; }

    private void Awake(){
        Bar       = transform.Find("Bar").GetComponent<Image>();
        Delay     = transform.Find("Delay").GetComponent<Image>();
        Bar.color = normalColor;
    }
    
    public void UpdateBar(float percent){
        Tween.MyTween(this, Bar.fillAmount, percent, SetBarAmount);
        if(percent <= 0.25) Bar.color = warningColor;
        if(UseDelay){ StartCoroutine(UpdateDelayTask(percent)); }
        else        { SetDelayAmount(percent); }
    }

    private void SetBarAmount(float percent) => Bar.fillAmount = percent;

    private IEnumerator UpdateDelayTask(float percent){
        yield return new WaitForSeconds( DelayBegin );
        UpdateDelay(percent);
    }

    private void UpdateDelay(float percent){
        Tween.MyTween(this, Delay.fillAmount, percent, SetDelayAmount);
    }
    
    private void SetDelayAmount(float percent) => Delay.fillAmount = percent;

}
