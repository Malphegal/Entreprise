using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : LivingEntity
{
    #region METHODS

    // TODO: Completer la méthode
    public override void Movement()
    {
        throw new System.NotImplementedException();
    }

    // TODO: Add the reduction of damage taken
    /* Called if 'this' has been hit */
    public override void GotHit(int damage)
    {
        this.HP = this.HP - Mathf.Max(0, damage - this.DefenceValue);
        if (this.HP <= 0)
        {
            tag = "Untagged";
            StartCoroutine(Die());
        }
        else if (this.HP < 30)
            GetComponent<Renderer>().material.color = Color.red;
        else if (this.HP < 60)
            GetComponent<Renderer>().material.color = new Color(1, 0.5F, 0, 1);
    }

    // TODO: Add an animation ?
    /* Perform an animation of death, and color fades out */
    public override IEnumerator Die()
    {
        string deadText = Lang.GetString("livingentity.state.dead");
        GameObject go = new GameObject("txt" + deadText);
        go.transform.position = new Vector3(0 - go.transform.position.x, 0 + go.transform.position.y + go.transform.lossyScale.y, 0 + go.transform.position.z);
        go.transform.SetParent(transform, false);
        var v = go.AddComponent<TextMesh>();
        v.text = deadText;
        v.fontSize = 100;
        v.characterSize = 0.1F;

            // Colors will fade out

        Renderer[] allChild = GetComponentsInChildren<Renderer>();
        for (int i = 1; i <= 100; i++)
        {
            yield return new WaitForSeconds(0.1F);
            foreach (Renderer r in allChild)
            {
                Color c = r.material.color;
                r.material.color = new Color(c.r, c.g, c.b, 1 - 0.01F * i);
            }
        }

            // Kill the actual gameObject

        Destroy(gameObject);
    }

    #endregion
}