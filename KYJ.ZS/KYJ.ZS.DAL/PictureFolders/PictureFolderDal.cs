using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Pages;

namespace KYJ.ZS.DAL.PictureFolders
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年4月22日
    /// 描述：图片文件夹数据访问类
    /// </summary>
    public partial class PictureFolderDal
    {
        /// <summary>
        /// 新增数据，并返回，受影响的行数
        /// </summary>
        /// <param name="model">M_PictureFolders的实体类</param>
        /// <returns>返回受影响的行数</returns>
        public long Insert(PictureFolder model)
        {
            #region SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into M_PictureFolders(");
            strSql.Append(" merchant_id, folder_name, folder_guid, folder_createtime, folder_updatetime, folder_sort");
            strSql.Append(") values (");
            strSql.Append(" @MerchantId,@FolderName,@FolderGuid,@FolderCreatetime,@FolderUpdatetime,@FolderSort");
            strSql.Append(")"); 
            #endregion
            #region 参数
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@MerchantId", model.MerchantId);
            sqlParam.AddParam("@FolderName", model.FolderName);
            sqlParam.AddParam("@FolderGuid", model.FolderGuid);
            sqlParam.AddParam("@FolderCreatetime", DateTime.Now);
            sqlParam.AddParam("@FolderUpdatetime", DateTime.Now);
            sqlParam.AddParam("@FolderSort", model.FolderSort); 
            #endregion
            return KYJ_ZushouWDB.SqlExecute(strSql.ToString(), sqlParam);
        }
        /// <summary>
        /// 更新数据，并返回，受影响的行数
        /// </summary>
        /// <param name="model">M_PictureFolders的实体类</param>
        /// <returns>返回受影响的行数</returns>
        public int UpDate(PictureFolder model)
        {
            #region SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update M_PictureFolders ");
            strSql.Append("set Merchant_Id= @MerchantId,Folder_Name= @FolderName,Folder_Guid= @FolderGuid,Folder_Updatetime= @FolderUpdatetime,Folder_Sort= @FolderSort");
            strSql.Append(" where Folder_Id=@FolderId"); 
            #endregion
            #region 参数
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@FolderId", model.FolderId);
            sqlParam.AddParam("@MerchantId", model.MerchantId);
            sqlParam.AddParam("@FolderName", model.FolderName);
            sqlParam.AddParam("@FolderGuid", model.FolderGuid);
            sqlParam.AddParam("@FolderUpdatetime", DateTime.Now);
            sqlParam.AddParam("@FolderSort", model.FolderSort); 
            #endregion
            return KYJ_ZushouWDB.SqlExecute(strSql.ToString(), sqlParam);
        }

        /// <summary>
        /// 得到一个M_PictureFolder对象
        /// </summary>
        /// <param name="FolderId">要得到对象的主键</param>
        /// <returns>返回M_PictureFolder对象</returns>
        public PictureFolder GetModel(int FolderId)
        {
            PictureFolder model = null;
            #region SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ");
            strSql.Append("merchant_id as MerchantId ,");
            strSql.Append("folder_name as FolderName ,");
            strSql.Append("folder_guid as FolderGuid ,");
            strSql.Append("folder_createtime as FolderCreatetime ,");
            strSql.Append("folder_updatetime as FolderUpdatetime ,");
            strSql.Append("folder_sort as FolderSort ,");
            strSql.Append("folder_isdel as FolderIsdel");
            strSql.Append(" from M_PictureFolders ");
            strSql.Append(" where Folder_Id=@FolderId"); 
            #endregion
            #region 参数
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@FolderId", FolderId); 
            #endregion
            using (DataTable table = KYJ_ZushouRDB.GetTable(strSql.ToString(), sqlParam))
            {
                if (table.Rows.Count > 0)
                {
                    model = DataHelper<PictureFolder>.GetEntityList(table)[0];
                }
            }
            return model;
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public PageData<PictureFolder> GetList(PagePms pms)
        { 
            #region SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("merchant_id as MerchantId ,");
            strSql.Append("folder_name as FolderName ,");
            strSql.Append("folder_guid as FolderGuid ,");
            strSql.Append("folder_createtime as FolderCreatetime ,");
            strSql.Append("folder_updatetime as FolderUpdatetime ,");
            strSql.Append("folder_sort as FolderSort ,");
            strSql.Append("folder_isdel as FolderIsdel"); 
            #endregion
            #region 参数
            PagePmsDal pagePmsDal = new PagePmsDal();
            pagePmsDal.PageIndex = pms.PageIndex;
            pagePmsDal.PageSize = pms.PageSize;
            pagePmsDal.StrWhere = pms.StrWhere;
            pagePmsDal.ColList = strSql.ToString();
            pagePmsDal.SortColnum = "folder_sort";
            pagePmsDal.TableList = "M_PictureFolders";
            pagePmsDal.StrWhere = "merchant_id >0"; 
            #endregion
            return KYJ_ZushouRDB.GetPages<PictureFolder>(pagePmsDal);
        }
    }
}
