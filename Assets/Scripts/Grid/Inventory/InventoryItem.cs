using TMPro;

namespace Grid.Inventory
{
    [System.Serializable]
    public class InventoryItem : ItemBase
    {
        public TextMeshProUGUI levelText;


        public override void UpdateItemImage()
        {
            base.UpdateItemImage();
        
            if (data.info.prop.countable)
            {
                InventorySlot slot = GetComponentInParent<InventorySlot>();
                if (slot)
                {
                    slot.CountText(true);
                }

                levelText.gameObject.SetActive(false);
            }
            else
            {
                levelText.gameObject.SetActive(true);
                levelText.text = "+" + data.currentLevel.ToString();
            }
        }



    }
}