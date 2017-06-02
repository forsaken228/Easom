// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com

using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
using System.Collections.Generic;
using System.Linq;
namespace Easom.Core
{

    ///<summary>
    /// Role  客户端实体操作类
    ///</summary>
    [Serializable]
    public class Role
    {
        private static IRole _actor = null;
        private static readonly object _lockActor = new object();

        #region 属性

        ///<summary>
        /// ID
        ///</summary>
        public int ID
        {
            get;
            set;
        }

        ///<summary>
        /// Name
        ///</summary>
        public string Name
        {
            get;
            set;
        }

        ///<summary>
        /// HospitalID
        ///</summary>
        public int HospitalID
        {
            get;
            set;
        }

        ///<summary>
        /// Remark
        ///</summary>
        public string Remark
        {
            get;
            set;
        }

        ///<summary>
        /// Remark
        ///</summary>
        public string ToRole
        {
            get;
            set;
        }

        ///<summary>
        /// RoleID
        ///</summary>
        public IList<Role> ManagerRoles
        {
            get
            {
                IList<Role> roles = new List<Role>();
                if (!string.IsNullOrEmpty(this.ToRole))
                {
                    string[] paras = StringUtility.Split(this.ToRole, ',');
                    int[] roleList = Array.ConvertAll(paras, id => Convert.ToInt32(id));
                    if (roleList != null && roleList.Count() > 0)
                    {
                        foreach (var id in roleList)
                        {
                            Role role = Role.Actor.GetByID(id);
                            roles.Add(role);
                        }
                    }
                    return roles;
                }
                return null;
            }
        }


        #endregion 属性

        public static IList<dynamic> ConvertToArray(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc)
        {
            IList<dynamic> sourceData = new List<dynamic>();
            IList<Role> entityList = Role.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
            if (entityList != null && entityList.Count > 0)
            {
                foreach (var item in entityList)
                {
                    string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                    string edit = "<a class=\"fa fa-edit\" href=\"javascript:edit('" + item.ID + "')\" title=\"编辑\"></a>&nbsp;";
                    edit += "<span><a class=\"fa fa-shield\" href=\"javascript:setauthor('" + item.ID + "')\" title=\"设置权限\"></a>&nbsp;";
                    edit += "<a class=\"fa fa-users\" href=\"javascript:setmangerrole('" + item.ID + "')\" title=\"设置管理角色\"></a>";
                    sourceData.Add(new dynamic[] { checkeds, item.Name, item.Remark, edit });
                }
            }
            return sourceData;
        }


        ///<summary>
        /// Actor
        ///</summary>
        public static IRole Actor
        {
            get
            {
                if (_actor == null)
                {
                    lock (_lockActor)
                    {
                        if (_actor == null)
                        {
                            _actor = new RoleService();
                        }
                    }
                }

                return _actor;
            }
        }


        public static bool IsChecked(IList<int> entityList, int id)
        {
            bool returnData = false;
            if (entityList != null && entityList.Count > 0)
            {
                var entity = from author in entityList where author == id select author;
                if (entity != null && entity.Count() > 0)
                {
                    returnData = true;
                }
            }
            return returnData;
        }


    }
}
