using UnityEngine;
using System.Collections;

public abstract class SpellCardBase : MonoBehaviour
{
    public string spellName;
    public float spellTime;
    public float basicHp;

    public virtual IEnumerator preStart() { yield return null; }
    public abstract IEnumerator startSpell();
    public abstract IEnumerator stopSpell();
}
