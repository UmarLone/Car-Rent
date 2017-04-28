/****************************** Module Header ******************************\
Module Name:  <ImageResult>
Project:      CarRent
Author :    Umar Farooq
\***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI.WebControls;

public class ImageResult : ActionResult
{
    //============= Global variables and byte array ================
    public String ContentType { get; set; }
    public byte[] ImageBytes { get; set; }
    public String SourceFilename { get; set; }

    /// <summary>
    /// Get Image result of content file
    /// </summary>    
    public ImageResult(String sourceFilename, String contentType)
    {
        SourceFilename = sourceFilename;
        ContentType = contentType;
    }

    /// <summary>
    /// Return Byte array 
    /// </summary>    
    public ImageResult(byte[] sourceStream, String contentType)
    {
        ImageBytes = sourceStream;
        ContentType = contentType;
    }

    /// <summary>
    /// Change Image resulation and Create New Image
    /// </summary>
    /// <param name="context"></param>
    public override void ExecuteResult(ControllerContext context)
    {
        var response = context.HttpContext.Response;
        response.Clear();
        response.Cache.SetCacheability(HttpCacheability.NoCache);
        response.ContentType = ContentType;

        if (ImageBytes != null)
        {
            var stream = new MemoryStream(ImageBytes);
            stream.WriteTo(response.OutputStream);
            stream.Dispose();
        }
        else
        {
            ImageBytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Content/admin_layout/images/shelf_sample_image.png"));
            var stream = new MemoryStream(ImageBytes);
            stream.WriteTo(response.OutputStream);
            stream.Dispose();
        }
    }
}