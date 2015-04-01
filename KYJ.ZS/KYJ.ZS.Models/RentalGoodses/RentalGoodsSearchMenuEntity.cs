using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.Models.RentalGoodses
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-5-9
    /// 描述：三级页面属性菜单栏
    /// </summary>
    public class RentalGoodsSearchMenuEntity
    {

        private List<CategoryAttribute> _attrs;
        private List<Brand> _brands;
        private List<CategoryNorm> _categoryNorms;
        //分类品牌集合
        public List<Brand> BrandList
        {
            get
            {
                if (_brands != null)
                    return _brands;
                else
                {
                    _brands = new List<Brand>();
                }
                return _brands;
            }
            set
            {
                _brands = value;

            }
        }
        //分类属性集合
        public List<CategoryAttribute> Attrs
        {
            get
            {
                if (_attrs != null)
                    return _attrs;
                else
                {
                    _attrs = new List<CategoryAttribute>();
                }
                return _attrs;
            }
            set
            {
                _attrs = value;

            }
        }
        //分类规格集合
        public List<CategoryNorm> CategoryNorms
        {
            get
            {
                if (_categoryNorms != null)
                    return _categoryNorms;
                else
                {
                    _categoryNorms = new List<CategoryNorm>();
                }
                return _categoryNorms;
            }
            set
            {
                _categoryNorms = value;

            }
 
        }
    }
}
