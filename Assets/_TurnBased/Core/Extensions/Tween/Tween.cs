using System.Collections;
using UnityEngine;

namespace MyScripts.Tween{
    public class Tween
    {
        public static bool useDebug = false;
        
        public static void MyTween(MonoBehaviour mono, float duration, float from, float to, System.Action<float> func){
            mono.StartCoroutine(TweenRoutine( duration, from, to, func ));
        }
        public static void MyTween(MonoBehaviour mono, float from, float to, System.Action<float> func, float duration = 2){
            mono.StartCoroutine(TweenRoutine( duration, from, to, func ));
        }

        #region Main Tween
        private static IEnumerator TweenRoutine( float duration, float from, float to, System.Action<float> func, float[] phaseTotal=null, float[] phaseDuration=null, float deltaTime=0.02f ){
            if( phaseTotal == null || !CorrectValueArray( phaseTotal ) ){
                phaseTotal = new float[]{ 0.08f, 0.84f, 0.08f };

                #if UNITY_EDITOR
                if( useDebug ){ Debug.Log( "Total error, use Default total." ); }
                #endif
            }
            if( phaseDuration == null || !CorrectValueArray( phaseDuration ) ){
                phaseDuration = new float[]{ 0.2f, 0.6f, 0.2f };

                #if UNITY_EDITOR
                if( useDebug ){ Debug.Log( "Duration error, use Default duration." ); }
                #endif
            }
            if( phaseTotal.Length != phaseDuration.Length ){
                phaseTotal = phaseDuration;
            }

            float totalAmount = to-from;
            float min    = from,
                  max    = to;
            if( from >= to ){ max = from; min = to; }            

            float delta;
            for( int idx = 0, cnt; idx < phaseDuration.Length; idx++ ){
                cnt   = Mathf.RoundToInt( phaseDuration[idx]/deltaTime );
                delta = totalAmount*phaseTotal[idx]/cnt;
                while( cnt > 0 ){
                    cnt--;
                    yield return new WaitForSeconds( deltaTime );
                    from = Mathf.Clamp( from+delta, min, max );
                    func?.Invoke( from );
                }
            }
            if( from != to ){

                // #if UNITY_EDITOR
                if( useDebug ){ Debug.Log( $"From {from}\nto {to}" ); }
                // #endif
            }
        }

        #endregion
        
        #region Help Function
        private static bool CorrectValueArray( float[] values ){
            float total = 0f;
            foreach( var target in values ){
                total += target;
            }
            return total.Equals( 1f )? true : false;
        }
        #endregion

    }
}
