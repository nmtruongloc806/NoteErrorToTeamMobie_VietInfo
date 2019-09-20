using System;
using System.Diagnostics;
using System.IO;
using Android.Content;
using Android.Webkit;
using DashBoardSoNV.Droid.Dependencys;
using DashBoardSoNV.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(DocumentViewer_Droid))]
namespace DashBoardSoNV.Droid.Dependencys
{
    public class DocumentViewer_Droid : IDocumentViewer
    {
        public void ShowDocumentFile(string filepath)
        {
            try
            {
                Java.IO.File file = new Java.IO.File(filepath);
                file.SetReadable(true);

                //string application = "";
                string extension = Path.GetExtension(filepath);

                // get mimeTye
                //switch (extension.ToLower())
                //{
                //    case ".txt":
                //        application = "text/plain";
                //        break;
                //    case ".doc":
                //    case ".docx":
                //        application = "application/msword";
                //        break;
                //    case ".pdf":
                //        application = "application/pdf";
                //        break;
                //    case ".xls":
                //    case ".xlsx":
                //        application = "application/vnd.ms-excel";
                //        break;
                //    case ".jpg":
                //    case ".jpeg":
                //    case ".png":
                //        application = "image/jpeg";
                //        break;
                //    default:
                //        application = "*/*";
                //        break;
                //}

                string mimeType = null;
                if (!string.IsNullOrWhiteSpace(extension))
                {
                    extension = extension.Trim('.', ' ');
                    mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                }

                Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, mimeType);
                intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Select App"));
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        public void ShowOnlineFile(string url)
        {
            try
            {
                string extension = Path.GetExtension(url);
                string mimeType = null;
                if (!string.IsNullOrWhiteSpace(extension))
                {
                    extension = extension.Trim('.', ' ');
                    mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                }

                var uri = Android.Net.Uri.Parse(url);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, mimeType);
                intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Select App"));
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }
    }
}