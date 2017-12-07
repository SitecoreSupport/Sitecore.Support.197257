using System;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using System.Collections;
using Sitecore.Shell.Applications.ContentManager;

namespace Sitecore.Support.Shell.Applications.ContentManager
{
    public class ContentEditorForm: Sitecore.Shell.Applications.ContentManager.ContentEditorForm
    {
        protected void TreeSearchOptionName_Click()
        {
            string control = Context.ClientPage.ClientRequest.Control;
            string text = StringUtil.Mid(control, 20);
            SheerResponse.DisableOutput();
            Web.UI.HtmlControls.Menu menu = new Web.UI.HtmlControls.Menu();
            Hashtable arg_38_0 = base.FieldInfo;
            bool flag = false;
            SortedList sortedList = new SortedList(StringComparer.Ordinal);
            foreach (FieldInfo fieldInfo in arg_38_0.Values)
            {
                Item item = Client.ContentDatabase.GetItem(fieldInfo.FieldID);
                if (item != null)
                {
                    if (!sortedList.ContainsKey(item.GetUIDisplayName()))
                    {
                        sortedList.Add(item.GetUIDisplayName(), item.Key);
                    }
                    flag = true;
                }
            }
            if (flag)
            {
                foreach (string text2 in sortedList.Keys)
                {
                    menu.Add("F" + sortedList[text2], text2, string.Empty, string.Empty, string.Format("javascript:scContent.changeSearchCriteria(\"{0}\",\"{1}\",\"{2}\")", text, text2, text2), false, string.Empty, MenuItemType.Normal);
                }
                menu.AddDivider();
            }
            menu.Add("Remove", "Remove", string.Empty, string.Empty, "javascript:scContent.removeSearchCriteria(\"" + text + "\")", false, string.Empty, MenuItemType.Normal);
            SheerResponse.EnableOutput();
            SheerResponse.ShowPopup(control, "below", menu);
        }

    }
}