using System;
using System.Web.Mvc;
using CHCMS.Utility;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using Easom.Support.App_Start;
using Easom.Core.Support;
using Easom.Core;


namespace Easom.Support.Controllers
{
    [HandleError]
    public class UploadController : SysAdminBaseController
    {
        #region Í¼Æ¬ÉÏ´«
        [HttpPost]
        public JsonResult PhotoUpLoad(FormCollection form)
        {
            if (SiteUtility.CurRequest.Files != null && SiteUtility.CurRequest.Files.Count > 0)
            {
                string fileName = SiteUtility.CurRequest.Files[0].FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    int index = fileName.LastIndexOf("\\");
                    if (index > 0)
                    {
                        fileName = fileName.Substring(index + 1);
                    }
                }
                string saveFileName = FileUtility.CreateFileName(FileUtility.GetExtension(fileName));
                string filePath = WebSitePath.GetPath(WebSitePathConstant.UPLOAD_PHOTO);
                // ImageUtility.CompressAsJPG(SiteUtility.CurRequest.Files[0].InputStream, Path.Combine(filePath, saveFileName),100);
                FileUtility.SaveAs(SiteUtility.CurRequest.Files[0].InputStream, filePath, saveFileName);
                string fileURL ="/" + WebSitePath.GetAutoURL() + "/" + saveFileName;
                UserInfo entity = UserInfo.Actor.GetByID(CurrentUser.ID);
                entity.PictureURL = fileURL;
                UserInfo.Actor.Update(entity);
                //string reData = string.Format("{0},{1}", fileURL, saveFileName);
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }
        #endregion
    }
}

