using DevExpress.Utils;
using DMS_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DMS_Beta.Controllers
{
    public class AttachmentController
    {
        public string SaveAttachments(Attachment attachment, int emp)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p;

            p = new SqlParameter("@filename", SqlDbType.NVarChar, 100);
            p.Value = attachment.Filename;
            parameters.Add(p);
            p = new SqlParameter("@filetype", SqlDbType.NVarChar, 50);
            p.Value = attachment.FileType;
            parameters.Add(p);
            p = new SqlParameter("@filebuffer", SqlDbType.VarBinary);
            p.Value = attachment.Filebuffer;
            parameters.Add(p);
            p = new SqlParameter("@location", SqlDbType.NVarChar, 100);
            p.Value = attachment.Location;
            parameters.Add(p);
            p = new SqlParameter("@category", SqlDbType.NVarChar, 50);
            p.Value = attachment.Type;
            parameters.Add(p);
            p = new SqlParameter("@subcategory", SqlDbType.NVarChar, 50);
            p.Value = attachment.ProccessSubType;
            parameters.Add(p);
            p = new SqlParameter("@categoryid", SqlDbType.BigInt);
            p.Value = attachment.Proccess;
            parameters.Add(p);
            p = new SqlParameter("@emp", SqlDbType.Int);
            p.Value = emp;
            parameters.Add(p);
            p = new SqlParameter("@ret", SqlDbType.NVarChar, 50);
            p.Direction = ParameterDirection.Output;
            parameters.Add(p);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SaveAttachment";
            return Database.Save(cmd, parameters);
        }

        public DataTable ShowAttachments(Int64 catid, string category, string subcat)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;

            parameter = new SqlParameter("@categoryid", SqlDbType.BigInt);
            parameter.Value = catid;
            parameters.Add(parameter);

            parameter = new SqlParameter("@category", SqlDbType.NVarChar, 50);
            parameter.Value = category;
            parameters.Add(parameter);

            parameter = new SqlParameter("@subcategory", SqlDbType.NVarChar, 50);
            parameter.Value = subcat;
            parameters.Add(parameter);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowAttachments";
            DataTable dt = Database.Showbyparams(cmd, parameters);
            return dt;
        }

        public ImageBrush CreateImage(Attachment attachment, ImageBrush brush, Stretch stretch)
        {
            using (MemoryStream ms = new MemoryStream(attachment.Filebuffer, 0,attachment.Filebuffer.Length))
            {
                brush = new ImageBrush();
                brush.Stretch = stretch;
                var imageSource = new BitmapImage();
                ms.Position = 0;
                imageSource.BeginInit();
                imageSource.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.UriSource = null;
                imageSource.StreamSource = ms;
                imageSource.EndInit();
                brush.ImageSource = imageSource;
            }
            return brush;
        }
    }
}
