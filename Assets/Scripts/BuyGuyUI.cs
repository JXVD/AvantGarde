using UnityEngine;
using UnityEngine.UI;

public class BuyGuyUI : MonoBehaviour {
    [SerializeField]
    Text dialogue;

    [SerializeField]
    Image sprite;

    [SerializeField]
    Sprite greedySprite;

    [SerializeField]
    Sprite madSprite;

    public void SetDialogue(string dialogue) {
        this.dialogue.text = dialogue;
    }

    public void SetToGreedy() {
        sprite.sprite = greedySprite;
    }

    public void SetToMad() {
        sprite.sprite = madSprite;
    }
}
