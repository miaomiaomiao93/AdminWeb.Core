using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdminWeb.Core.IServices;
using AdminWeb.Core.Model;
using AdminWeb.Core.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Core.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Route("api/sysUserInfo")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class sysUserInfoController : Controller
    {
        IsysUserInfoServices IsysUserInfoServices;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isysUserInfoServices"></param>
        public sysUserInfoController(IsysUserInfoServices isysUserInfoServices)
        {
            this.IsysUserInfoServices = isysUserInfoServices;
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("GetSysUserInfo")]
        [HttpGet]
        public async Task<IActionResult> GetSysUserInfo(int Id)
        {
            var model = await IsysUserInfoServices.GetSysUserInfo(Id);
            return Ok(new MessageModel<sysUserInfoViewModels>()
            {
                Success = true,
                Data = model
            });
        }

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="sysUserInfoViewModels"></param>
        /// <returns></returns>
        [Route("ListPage")]
        [HttpPost]
        public IActionResult ListPage([FromBody] sysUserInfoViewModels sysUserInfoViewModels)
        {
            var models = IsysUserInfoServices.ListPagesysUserInfos(sysUserInfoViewModels);
            return Ok(models);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sysUserInfoViewModels"></param>
        /// <returns></returns>
        [Route("AddsysUserInfo")]
        [HttpPost]
        public async Task<IActionResult> AddsysUserInfo([FromBody] sysUserInfoViewModels sysUserInfoViewModels)
        {
            var result = await IsysUserInfoServices.AddSysUserInfo(sysUserInfoViewModels);
            return Ok(new MessageModel<ModuleViewModels>()
            {
                Success = result,
                Msg = result ? "用户添加成功" : "用户更新失败"
            });
        }


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="sysUserInfoViewModel"></param>
        /// <returns></returns>
        [Route("UpdatesysUserInfo")]
        [HttpPost]
        public async Task<IActionResult> UpdatesysUserInfo([FromBody] sysUserInfoViewModels sysUserInfoViewModel)
        {
            var result = await IsysUserInfoServices.UpdateSysUserInfo(sysUserInfoViewModel);
            return Ok(new MessageModel<ModuleViewModels>()
            {
                Success = result,
                Msg = result ? "用户更新成功" : "用户更新失败"
            });
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("DeletesysUserInfo")]
        [HttpGet]
        public async Task<IActionResult> DeletesysUserInfo(int Id)
        {
            var result = await IsysUserInfoServices.DeleteSysUserInfo(Id);
            return Ok(new MessageModel<ModuleViewModels>()
            {
                Success = result,
                Msg = result ? "用户删除成功" : "用户删除失败"
            });
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadFiles")]
        [Consumes("application/json", "multipart/form-data")]
        public IActionResult UploadFiles(IFormFile file)
        {
            try
            {
                //var form = Request.Form;//直接从表单里面获取文件名不需要参数
                //定义图片数组后缀格式
                string[] LimitPictureType = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP" };
                //获取图片后缀是否存在数组中
                string currentPictureExtension = Path.GetExtension(file.FileName).ToUpper();
                var db_path = "";
                if (LimitPictureType.Contains(currentPictureExtension))
                {

                    //为了查看图片就不在重新生成文件名称了
                    var new_path = Path.Combine("wwwroot/uploads/images/", file.FileName);
                    db_path = Path.Combine("uploads/images/", file.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), new_path);
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "wwwroot/uploads/images/"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "wwwroot/uploads/images/");
                    }
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        //再把文件保存的文件夹中
                        file.CopyTo(stream);
                    }
                }
                else
                {
                    return Json(new { status = -2, message = "请上传指定格式的图片" });
                }


                return Json(new { status = 0, message = "上传成功", path = db_path });
            }
            catch (Exception ex)
            {

                return Json(new { status = -3, message = "上传失败", data = ex.Message });
            }

        }
    }
}