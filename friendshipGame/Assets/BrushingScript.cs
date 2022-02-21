using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BrushingScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	[SerializeField] private Canvas canvas;
	[SerializeField] private Image dirtyTeethImg;
	[SerializeField] private int brushSize;

	private Texture2D dirtyTeethTex;
	private RectTransform rectTransform;
	//private Texture2D dirtyTeethTexture;
	//private SpriteRenderer rend;



	private Texture2D Img2Tex(ref Image img) {
		// From @TEEBQNE on StackOverflow https://stackoverflow.com/a/69621374

		Sprite imgSprite = img.sprite;

		// create a new instance of our texture to not write to it directly and overwrite it
		var tex2D = new Texture2D((int)imgSprite.rect.width, (int)imgSprite.rect.height);
		var pixels = imgSprite.texture.GetPixels((int)imgSprite.textureRect.x,
												(int)imgSprite.textureRect.y,
												(int)imgSprite.textureRect.width,
												(int)imgSprite.textureRect.height);

		tex2D.SetPixels(pixels);
		tex2D.Apply();

		// assign this new texture to our image by creating a new sprite
		img.sprite = Sprite.Create(tex2D, img.sprite.rect, img.sprite.pivot);

		return tex2D;
	}


	private void Awake() {
		rectTransform = GetComponent<RectTransform>();

		dirtyTeethTex = Img2Tex(ref dirtyTeethImg);

	}

	private void Start() {

	}

	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log("OnBeginDrag");
	}

	private void BrushTransparency(Vector2 pos, ref Image img, ref Texture2D tex, int brushSize) {
		print("BT");



		// Adapted from @TEEBQNE on StackOverflow https://stackoverflow.com/a/69621374

		int px = Mathf.Clamp(0, (int)( ( pos.x - img.rectTransform.rect.x ) * tex.width / img.rectTransform.rect.width ), tex.width);
		int py = Mathf.Clamp(0, (int)( ( pos.y - img.rectTransform.rect.y ) * tex.height / img.rectTransform.rect.height ), tex.height);

		if(px >= dirtyTeethTex.width || py >= dirtyTeethTex.height)
			return;

		px = Mathf.Clamp(px - ( brushSize / 2 ), 0, tex.width);
		py = Mathf.Clamp(py - ( brushSize / 2 ), 0, tex.height);
		int maxWidth = Mathf.Clamp(brushSize / 2 + 1, 0, tex.width - px);
		int maxHeight = Mathf.Clamp(brushSize / 2 + 1, 0, tex.height - py);

		// create an array for our colors
		Color[] colorArray = tex.GetPixels(px, py, maxWidth, maxHeight);

		// fill this with our color
		for(int x = 0; x < colorArray.Length; ++x) {
			colorArray[x].a = 0;
		}

		tex.SetPixels(px, py, maxWidth, maxHeight, colorArray);
		tex.Apply();
		img.sprite = Sprite.Create(tex, img.sprite.rect, img.sprite.pivot);
	}

	public void OnDrag(PointerEventData eventData) {
		Debug.Log("OnDrag");
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		Vector2 pos = rectTransform.anchoredPosition;
		BrushTransparency(pos, ref dirtyTeethImg, ref dirtyTeethTex, brushSize);
		
	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log("OnEndDrag");

		// Check if the player has won
		for(int x = 0; x < dirtyTeethTex.width; ++x) {
			for(int y = 0; y < dirtyTeethTex.height; ++y) {
				var pixel = dirtyTeethTex.GetPixel(x, y);
				if(pixel.r > 0.87 && pixel.r < 0.89 && pixel.g > 0.79 && pixel.g < 0.80 && pixel.b > 0.48 && pixel.b < 0.49 && pixel.a != 0) {
					return;
				}
			}
		}
		print("You won!");
		SceneManager.LoadScene("Scenes/TalkingSections");
	}

	public void OnPointerDown(PointerEventData eventData) {
		Debug.Log("OnPointerDown");
	}
}