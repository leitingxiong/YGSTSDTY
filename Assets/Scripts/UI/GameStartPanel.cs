using Imodel;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameStartPanel : MonoBehaviour, IController
    {
        private IGameModel mGameModel;
        private TextMeshProUGUI _guess;
        private TextMeshProUGUI _point;
        private TextMeshProUGUI _clean;
        private TextMeshProUGUI _gold;
        private TextMeshProUGUI _day;
        private TextMeshProUGUI _soul;

        void Start()
        {
            _day = transform.Find("Model/DayText").GetComponent<TextMeshProUGUI>();
            _gold = transform.Find("Model/GoldText").GetComponent<TextMeshProUGUI>();
            _clean = transform.Find("Model/CleanText").GetComponent<TextMeshProUGUI>();
            _point = transform.Find("Model/ActionPointText").GetComponent<TextMeshProUGUI>();
            _guess = transform.Find("Model/GuestCountText").GetComponent<TextMeshProUGUI>();
            _soul = transform.Find("Model/SoulText").GetComponent<TextMeshProUGUI>();
            transform.Find("BtnStart").GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    this.SendCommand<StartGameCommand>();
                });
            

            mGameModel = this.GetModel<IGameModel>();
            mGameModel.Gold.Register(OnGoldValueChanged);
            mGameModel.Cleanliness.Register(OnCleanlinessValueChanged);
            mGameModel.Day.Register(OnDayValueChanged);
            mGameModel.GuestCount.Register(OnGuessValueChanged);
            mGameModel.ActionPoint.Register(OnActionPointValueChanged);
            mGameModel.Soul.Register(OnSoulValueChanged);

            // 第一次需要调用一下
            OnGoldValueChanged(mGameModel.Gold.Value);
            OnCleanlinessValueChanged(mGameModel.Cleanliness.Value);
            OnDayValueChanged(mGameModel.Day.Value);
            OnGuessValueChanged(mGameModel.GuestCount.Value);
            OnActionPointValueChanged(mGameModel.ActionPoint.Value);
            OnSoulValueChanged(mGameModel.Soul.Value);
        }

        private void OnActionPointValueChanged(int Point)
        {
            if (Point <= 0)
            {
                this.SendCommand<NewDayCommand>();
            }
            _point.text = "ActionPoint：" + Point;
        }

        private void OnGuessValueChanged(int GuessCount)
        {
            if (GuessCount >= mGameModel.GuestCountLimit.Value)
            {
                GuessCount = mGameModel.GuestCountLimit.Value;
            }
            _guess.text = 
                "客人数/上限：" + GuessCount + "/" + mGameModel.GuestCountLimit.Value;
        }

        private void OnCleanlinessValueChanged(int Clean)
        {
            _clean.text = "clean：" + Clean;
        }

        private void OnGoldValueChanged(int gold)
        {
            _gold.text = "gold：" + gold;
        }

        private void OnDayValueChanged(int day)
        {
            _day.text = "day:" + day;
        }
        
        private void OnSoulValueChanged(int soul)
        {
            _soul.text = "soul：" + soul;
        }


        private void OnDestroy()
        {
            mGameModel.Gold.UnRegister(OnGoldValueChanged);
            mGameModel.Cleanliness.UnRegister(OnCleanlinessValueChanged);
            mGameModel.Day.UnRegister(OnDayValueChanged);
            mGameModel.GuestCount.UnRegister(OnGuessValueChanged);
            mGameModel.ActionPoint.UnRegister(OnActionPointValueChanged);
            mGameModel.Soul.UnRegister(OnSoulValueChanged);
            mGameModel = null;
        }

        public IArchitecture GetArchitecture()
        {
            return GameManager.Interface;
        }
    }
}