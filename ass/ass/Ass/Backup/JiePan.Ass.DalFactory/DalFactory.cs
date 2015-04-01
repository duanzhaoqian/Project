
using System;
using Autofac;
using JiePan.Ass.IDal;
namespace JiePan.Ass.Dal
{
    public class DalFactory
    {
		public static IUserInfoDal UserInfoDal {
            get { return AutoFacTool.AssContainer.Resolve<IUserInfoDal>(); }
        }

	}
}