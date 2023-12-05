using UnityEngine;
using UnityEngine.UI;

namespace ScriptsRuntime.Client.Controllers.Battle.HUD {

    public class HUDHpBar : MonoBehaviour {

        Image bgImg;
        Image barImg;

        public void Ctor() {
            bgImg = transform.GetChild(0).GetComponent<Image>();
            barImg = transform.GetChild(1).GetComponent<Image>();

            Debug.Assert(bgImg != null);
            Debug.Assert(barImg != null);
        }

        public void SetHp(int hp, int hpMax) {
            if (hpMax == 0) {
                barImg.fillAmount = 0;
                return;
            }
            float hpPrecent = (float)hp / hpMax;
            barImg.fillAmount = hpPrecent;
        }

    }

}