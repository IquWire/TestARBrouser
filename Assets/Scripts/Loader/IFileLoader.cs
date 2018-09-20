using System.Collections.Generic;

public interface IFileLoader<T> where T : ItemDataHolder
{
    void GetList(List<T> itemList);
}
