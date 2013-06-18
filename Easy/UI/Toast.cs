using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Easy.UI
{
    /// <summary>
    /// Shows simple toast notifications (don't forget to mark app as toast capable in manifest)
    /// </summary>
    public class Toast
    {
        // XML templates
        private const string ToastText01Template = "<toast><visual version='1'><binding template='ToastText01'><text id='1'>{0}</text></binding></visual></toast>";
        private const string ToastText02Template = "<toast><visual version='1'><binding template='ToastText02'><text id='1'>{0}</text><text id='2'>{1}</text></binding></visual></toast>";

        // Used to show notifications
        private static ToastNotifier _notifier = ToastNotificationManager.CreateToastNotifier();

        /// <summary>
        /// Displays a toast message
        /// </summary>
        /// <param name="text">Text content</param>
        public static void Show(string text)
        {
            ShowToast(String.Format(ToastText01Template, text));
        }

        /// <summary>
        /// Displays a toast message
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="text">Text content</param>
        public static void Show(string title, string text)
        {
            ShowToast(String.Format(ToastText02Template, title, text));
        }

        /// <summary>
        /// Displays a toast message given an XML string
        /// </summary>
        /// <param name="xmlString">XML string</param>
        private static void ShowToast(string xmlString)
        {
            // Load given string as XML document
            var toastDom = new XmlDocument();
            toastDom.LoadXml(xmlString);

            // Show toast
            _notifier.Show(new ToastNotification(toastDom));
        }
    }
}
