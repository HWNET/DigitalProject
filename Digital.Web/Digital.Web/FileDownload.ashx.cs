using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Digital.WCFClient;
using Digital.WCFClient.ConfigService;
using Microsoft.AspNet.Identity;

namespace Digital.Web
{
    /// <summary>
    /// Summary description for FileDownload
    /// </summary>
    public class FileDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request.QueryString["fileName"]; //客户端保存的文件名
            string folderName = context.Request.QueryString["folderName"];
            byte[] buffer = null;

            var client = ServiceHub.GetCommonServiceClient<FileCabinetServiceClient>();
            buffer = client.FileStreamByFile(context.User.Identity.GetUserId(), folderName, fileName);
            client.Close();

            if (buffer != null)
            {
                context.Response.Clear();
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                context.Response.BinaryWrite(buffer);
                context.Response.Flush();
                context.Response.Close();
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}