using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StokTakip_Deneme_.ActiveFolder
{
    public static class ActiveClass
    {


        public static string Active(this HtmlHelper Html,string Control , string Action)
        {

            string active = "";

            var routeData = Html.ViewContext.RouteData;

            string routeControl = (string)routeData.Values[Control];
            string routeAction  = (string)routeData.Values[Action]; 


            if(Control == routeControl && Action== routeAction) { active = "active"; }

            return active;

             
        }
    }
}