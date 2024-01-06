using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScreen : MonoBehaviour
{
    public List<Texture2D> listResponsesLevel;

    private SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        //ChangeTexture(0);
    }

    public void ChangeTexture(int index)
    {
        if (index < listResponsesLevel.Count)
        {
            Texture2D currentTexture = listResponsesLevel[index];

            if (myRenderer != null)
            {
                // Obtenir la taille actuelle de l'objet
                Vector3 currentScale = transform.localScale;

                // Ajuster la taille de la texture en fonction de la taille de l'objet
                Sprite nouveauSprite = Sprite.Create(currentTexture, new Rect(0, 0, currentTexture.width, currentTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

                // Appliquer le sprite ajusté
                myRenderer.sprite = nouveauSprite;

                // Rétablir la taille de l'objet à la taille actuelle
                transform.localScale = currentScale;
            }
        }
        else
        {
            Debug.LogError("Index out of range in ChangeTexture method.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ajoutez ici votre logique de mise à jour si nécessaire
    }
}
