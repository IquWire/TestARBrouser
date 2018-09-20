
[System.Serializable]
public class ItemDataHolder
{
    public string TitleString;
    
    public string DescriptionString;

    public string LinkString;
    
    public void SetStrings(string titleText, string descriptionText, string linkText)
    {
        TitleString = titleText;
        DescriptionString = descriptionText;
        LinkString = linkText;
    }
}
