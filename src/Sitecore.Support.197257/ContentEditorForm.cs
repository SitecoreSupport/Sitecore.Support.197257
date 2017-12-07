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
        public void TreeSearchOptionName_ClickTest()
        {
            string control = Context.ClientPage.ClientRequest.Control;
            string text = StringUtil.Mid(control, 20);
            SheerResponse.DisableOutput();
            Sitecore.Web.UI.HtmlControls.Menu menu = new Sitecore.Web.UI.HtmlControls.Menu();
            Hashtable arg_38_0 = base.FieldInfo;
            bool flag = false;
            SortedList sortedList = new SortedList(StringComparer.Ordinal);
            foreach (FieldInfo fieldInfo in arg_38_0.Values)
            {
                Item item = Client.ContentDatabase.GetItem(fieldInfo.FieldID);
                if (item != null)
                {
                    if (!sortedList.ContainsKey(GetTitle(item)))
                    {

                        sortedList.Add(Sitecore.Globalization.Translate.Text(GetTitle(item)), item.Key);
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
        private string GetTitle(Item item)
        {
            if (item.Fields["Title"].Value != "")

            {
                return item.Fields["Title"].Value;
            }
            return item.Name;
        }

    }
}