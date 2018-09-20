using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using UnityEngine;

public class DataLoader : IDisposable//, IFileLoader<ItemDataHolder>
{
    public List<ItemDataHolder> ItemList;

    private string _url;

    public string Url
    {
        get { return _url; }

        set { _url = value; }
    }

    private string _feedTitle;

    public string FeedTitle
    {
        get { return _feedTitle; }
    }

    private string _feedDescription;

    public string FeedDescription
    {
        get { return _feedDescription; }
    }

    private bool _IsDisposed;

    public IEnumerator GetListCoroutine(Action Callback)
    {        
        Url = "https://blogs.unity3d.com/ru/feed/";
        WWW feed = new WWW(Url);
        
        yield return feed;

        if (String.IsNullOrEmpty(feed.text))
        {
            Debug.LogWarning("Empty url");
        }

        if (feed.isDone)
        {
            ItemList = new List<ItemDataHolder>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(feed.text);
            
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("rss/channel/item");
            ParseDocElements(xmlDoc, "rss / channel / title",ref _feedTitle);
            ParseDocElements(xmlDoc, "rss/channel/description", ref _feedDescription);

            foreach (XmlNode _xmlNode in xmlNodeList)
            {
                ItemDataHolder tempData = new ItemDataHolder();
                
                ParseDocElements(_xmlNode, "title", ref tempData.TitleString);
                ParseDocElements(_xmlNode, "description", ref tempData.DescriptionString);
                ParseDocElements(_xmlNode, "link", ref tempData.LinkString);
                
                //Debug.Log("Title : " + title.InnerText + "\r\n"
                //                + "Content : " + descriptionNode.InnerText + " link =" + linkNode.InnerText);
                ItemList.Add(tempData);
            }
        }

        if (Callback != null) Callback();
    }

    public List<ItemDataHolder> GetList()
    {
        return ItemList;
    }

    private void ParseDocElements(XmlNode parent, string xPath, ref string property)
    {
        XmlNode node = parent.SelectSingleNode(xPath);
        if (node != null)
            property = node.InnerText;
        else
            property = "Unresolvable";
    }

    public bool MyRemoteCertificateValidationCallback(System.Object sender,
        X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain,
        // look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    continue;
                }
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                bool chainIsValid = chain.Build((X509Certificate2) certificate);
                if (!chainIsValid)
                {
                    isOk = false;
                    break;
                }
                Debug.Log("chain.ChainStatus + " + i);
            }
        }
        return isOk;
    }

    private void Dispose(bool disposing)
    {
        if (disposing && !_IsDisposed)
        {
            ItemList.Clear();

            _url = null;

            _feedTitle = null;

            _feedDescription = null;
        }
        _IsDisposed = true;
    }

    public void ClearList()
    {
        if(ItemList!= null && ItemList.Count > 0)
        ItemList.Clear();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
