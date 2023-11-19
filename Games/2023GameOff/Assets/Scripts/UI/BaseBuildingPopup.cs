using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class BaseBuildingPopup : Popup {
    public TextMeshProUGUI _name;
    public TextMeshProUGUI description;
    public Button upgradeButton;
    public List<ChildrenStateManager> levelIndicators = new List<ChildrenStateManager>();
}