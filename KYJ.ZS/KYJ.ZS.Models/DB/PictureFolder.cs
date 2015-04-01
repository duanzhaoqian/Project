using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 时间:2014年4月18日
    /// 作者：maq
    /// 描述:图片文件夹实体
    /// </summary>
    public class PictureFolder
    {
        /// <summary>
        ///
        /// <summary>
        private int _folderid;
        public int FolderId
        {
            set { _folderid = value; }
            get { return _folderid; }
        }

        /// <summary>
        /// 商户id
        /// <summary>
        private int _merchantid;
        public int MerchantId
        {
            set { _merchantid = value; }
            get { return _merchantid; }
        }

        /// <summary>
        ///
        /// <summary>
        private string _foldername;
        public string FolderName
        {
            set { _foldername = value; }
            get { return _foldername; }
        }

        /// <summary>
        /// 唯一标识
        /// <summary>
        private string _folderguid;
        public string FolderGuid
        {
            set { _folderguid = value; }
            get { return _folderguid; }
        }

        /// <summary>
        ///
        /// <summary>
        private DateTime _foldercreatetime;
        public DateTime FolderCreatetime
        {
            set { _foldercreatetime = value; }
            get { return _foldercreatetime; }
        }

        /// <summary>
        ///
        /// <summary>
        private DateTime _folderupdatetime;
        public DateTime FolderUpdatetime
        {
            set { _folderupdatetime = value; }
            get { return _folderupdatetime; }
        }

        /// <summary>
        ///
        /// <summary>
        private int _foldersort;
        public int FolderSort
        {
            set { _foldersort = value; }
            get { return _foldersort; }
        }

        /// <summary>
        ///
        /// <summary>
        private bool _folderisdel;
        public bool FolderIsdel
        {
            set { _folderisdel = value; }
            get { return _folderisdel; }
        }
    }
}
