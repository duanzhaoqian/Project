using System;
using System.Data;
using ImageTestDao;
using TXCommons.PictureModular;

namespace TXThumbnailPremisesImage
{
    class Program
    {
        static void Main()
        {


            var dal = new GetPremises();
            var dt = dal.GetPremisesInnrtCodeAndCityId();
            int chenggong = 0;
            int shibai = 0;
            Console.WriteLine("读取数据成功");
            if (null != dt && dt.Rows.Count > 0)
            {
                Console.WriteLine("开始切割数据");
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine("切割中guid:" + dr["InnerCode"] as string);
                    try
                    {
                        var guid = dr["InnerCode"] as string;
                        var cityId = Convert.ToInt32(dr["CityId"]);
                        var isok = false;

                        //楼盘沙盘图
                        var premisesList = GetPicture.GetPremisesPictureListNoFilter(guid, false,
                            PremisesPictureType.PremisesLIST.ToString(), cityId);
                        //楼盘平面图
                        var planeList = GetPicture.GetPremisesPictureListNoFilter(guid, false,
                            PremisesPictureType.Plane.ToString(),
                            cityId);
                        //效果图
                        var effectList = GetPicture.GetPremisesPictureListNoFilter(guid, false,
                            PremisesPictureType.Effect.ToString(),
                            cityId);
                        //logo图
                        var logoList = GetPicture.GetPremisesPictureListNoFilter(guid, false,
                            PremisesPictureType.Logo.ToString(),
                            cityId);
                        //广告图
                        var advertList = GetPicture.GetPremisesPictureListNoFilter(guid, false,
                            PremisesPictureType.Advert.ToString(),
                            cityId);

                        //图片处理
                        Console.WriteLine("沙盘图");
                        if (null != premisesList && premisesList.Count > 0)
                        {
                            foreach (var item in premisesList)
                            {
                                UploadThumbnail.ThumbnailPremisesPhoto211_150(Redis.IMGPATH_BASE + item.Path,
                                    PremisesPictureType.PremisesLIST.ToString().ToUpper());
                            }
                            isok = true;
                        }
                        Console.WriteLine("平面图");
                        if (null != planeList && planeList.Count > 0)
                        {
                            foreach (var item in planeList)
                            {
                                UploadThumbnail.ThumbnailPremisesPhoto211_150(Redis.IMGPATH_BASE + item.Path,
                                    PremisesPictureType.Plane.ToString().ToUpper());
                            }
                            isok = true;
                        }
                        Console.WriteLine("效果图");
                        if (null != effectList && effectList.Count > 0)
                        {
                            foreach (var item in effectList)
                            {
                                UploadThumbnail.ThumbnailPremisesPhoto211_150(Redis.IMGPATH_BASE + item.Path,
                                    PremisesPictureType.Effect.ToString().ToUpper());
                            }
                            isok = true;
                        }
                        Console.WriteLine("Logo图");
                        if (null != logoList && logoList.Count > 0)
                        {
                            foreach (var item in logoList)
                            {
                                UploadThumbnail.ThumbnailLogoPhoto140_140(Redis.IMGPATH_BASE + item.Path,
                                    PremisesPictureType.Logo.ToString().ToUpper());
                            }
                            isok = true;
                        }
                        Console.WriteLine("广告图");
                        if (null != advertList && advertList.Count > 0)
                        {
                            foreach (var item in advertList)
                            {
                                UploadThumbnail.ThumbnailAdvertPic1190_200(Redis.IMGPATH_BASE + item.Path,
                                    PremisesPictureType.Advert.ToString().ToUpper());
                            }
                            isok = true;
                        }
                        if (isok)
                        {
                            Console.WriteLine("切图成功，guid：" + guid + ",CityId:" + cityId);
                            chenggong++;
                        }
                        else
                        {
                            Console.WriteLine("没有查询到数据，guid：" + guid + ",CityId:" + cityId);
                            shibai++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("错误信息" + ex);
                    }
                }
                Console.WriteLine("总计切图成功" + chenggong + "条数据");
                Console.WriteLine("总计切图没有查询到相关数据" + shibai + "条数据");
                Console.ReadLine();
            }
        }
    }
}
