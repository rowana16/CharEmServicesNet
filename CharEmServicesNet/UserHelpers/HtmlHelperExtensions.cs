using CharEmServicesNet.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CharEmServicesNet.UserHelpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString DisplayForPhone<T>(this HtmlHelper<T> helper, string phone)
        {
            if (phone == null)
            {
                return new HtmlString(string.Empty);
            }
            string formated = phone;
            if (phone.Length == 10)
            {
                formated = string.Format("({0}) {1}-{2}", phone.Substring(0, 3), phone.Substring(3, 3), phone.Substring(6, 4));
            }
            else if (phone.Length == 7)
            {
                formated = string.Format("{0}-{1}", phone.Substring(0, 3), phone.Substring(3, 4));
            }
            //string s = string.Format("<a href='tel:{0}'>{1}</a>", phone, formated);
            return new HtmlString(formated);
        }
    }
}