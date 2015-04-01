using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PicModel
    {
        public DateTime CreateTime { get; set; }
        public string FileName { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public bool IsDel { get; set; }
        public string PicDesc { get; set; }
        public string PictureType { get; set; }
    }
}
