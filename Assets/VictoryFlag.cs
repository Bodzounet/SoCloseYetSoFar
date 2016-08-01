using UnityEngine;
using System.Collections;

public class VictoryFlag : MonoBehaviour {

    Color[] rainbow = { Color.red, new Color(1, 0.647f, 0), Color.yellow, Color.green, Color.cyan, Color.blue, new Color(0.5f, 0, 0.5f) };

    private SpriteRenderer sr;

	void Start ()
    {
        sr = this.GetComponent<SpriteRenderer>();
        sr.color = rainbow[0];

        StartCoroutine(MakeRainbow());
    }

    private IEnumerator MakeRainbow()
    {
        int i = 0;
        int j = 1;

        while (true)
        {
            for (int k = 0; k <= 25; k++)
            {
                sr.color = Color.Lerp(rainbow[i], rainbow[j], k / 25.0f);
                yield return new WaitForEndOfFrame();
            }


            i = (i == (rainbow.Length - 1) ? 0 : i + 1);
            j = (j == (rainbow.Length - 1) ? 0 : j + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Application.LoadLevel("end");
        }
    }
}
