using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedPractice.AudioSystem;
using TurnBasedPractice.Character.Controller;
using TurnBasedPractice.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TurnBasedPractice.InfoSystem
{

    public static class BattleInfoSystem
    {
        private static readonly object _lock = new object();

        private static List<string>         _info;
        public static bool                  IsFinish{ get; private set; }
        public static Transform             BattleInfoPanel{ get; private set; }
        public static TMPro.TextMeshProUGUI InfoText{ get; private set; }
        public static Animator              NextArrowAnimator{ get; private set; }
        public static List<SfxName>         ClickSfxes{ get; private set; }

        private static float   _currentSpeed;
        private static SfxName _currentSfx;
        private static IInputController _input;

        private const float SLOW_SPEED   = 8f;
        private const float NORMAL_SPEED = 16f;
        private const float FAST_SPEED   = 32f;

        public static void Init(BattleInfoPlaySpeed playSpeed, Transform battleInfoPanel, IInputController input){
            _info             = new List<string>();
            _input            = input;
            IsFinish          = true;
            BattleInfoPanel   = battleInfoPanel;
            InfoText          = BattleInfoPanel.Find("InfoText").GetComponent<TMPro.TextMeshProUGUI>();
            NextArrowAnimator = BattleInfoPanel.Find("NextArrow").GetComponent<Animator>();
            ClickSfxes        = new List<SfxName>(){
                SfxName.Click1,
                SfxName.Click2,
                SfxName.Click3,
                SfxName.Click4
            };
            _currentSfx = SfxName.Click1;
            SetSpeed(playSpeed);
        }

        public static void SetSpeed(BattleInfoPlaySpeed playSpeed){
            switch(playSpeed){
                case BattleInfoPlaySpeed.SLOW:   _currentSpeed = SLOW_SPEED; break;
                case BattleInfoPlaySpeed.NORMAL: _currentSpeed = NORMAL_SPEED; break;
                case BattleInfoPlaySpeed.FAST:   _currentSpeed = FAST_SPEED; break;
            }
        }

        public static void Add(string line) => _info.Add(line);
        public static void Clear()          => _info.Clear();
        public static bool HasInfo()        => _info.Count != 0;

        public static void Play(){
            if(!IsFinish){ Debug.Log($"Playing, error.\nStackTrace: {Environment.StackTrace}"); }
            if(_info == null || _info.Count == 0){ return ; }
            IsFinish = false;
            BattleInfoPanel.gameObject.SetActive(true);
            BattleInfoPanel.GetComponent<MonoBehaviour>().StartCoroutine(PlayInfoTask());
        }
        
        private static IEnumerator PlayInfoTask()
        {
            InfoText.SetText("");
            NextArrowAnimator.Play("None");
            foreach (var line in _info)
            {
                if(string.IsNullOrEmpty(line)) continue;
                foreach (var letter in line)
                {
                    InfoText.text += letter;
                    AudioPlayer.PlaySfx(_currentSfx);
                    _currentSfx = _currentSfx == SfxName.Click4 ? SfxName.Click1 : (SfxName)((int)_currentSfx + 1);
                    yield return new WaitForSeconds(1 / _currentSpeed);
                }
                if (line.Equals(_info[_info.Count - 1])) { break; }
                InfoText.text += "\n";
            }
            NextArrowAnimator.Play("Arrow_Animation");
            while (!ToNextInfo())
            {
                yield return null;
            }
            IsFinish = true;
            BattleInfoPanel.gameObject.SetActive(false);
        }

        private static bool ToNextInfo(){
            return _input.RetrieveInputMouseLeft() || Keyboard.current.anyKey.wasPressedThisFrame || _input.RetrieveInputTouch();
        }
    }
}