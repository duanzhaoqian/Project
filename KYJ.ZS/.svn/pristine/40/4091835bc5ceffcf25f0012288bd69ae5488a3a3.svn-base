using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KYJ.ZS.Commons.Web
{
    public class DependencyPathViewEngine : RazorViewEngine
    {
        [ThreadStatic]
        public static IViewPathFilter ViewPathFilter = null;

        private string[] originalPartialViewLocations;
        private string[] originalViewLocations;
        private string[] originalMasterLocations;

        public DependencyPathViewEngine(string baseViewPath)
            : this(baseViewPath, null)
        {
        }

        public DependencyPathViewEngine(string baseViewPath, string basePartialViewPath)
        {
            if (!string.IsNullOrEmpty(baseViewPath))
            {
                this.ViewLocationFormats = this.ViewLocationFormats
                    .Select(it => it.Replace("~/Views", string.Format("~/{0}", baseViewPath))).ToArray();
            }
            if (!string.IsNullOrEmpty(basePartialViewPath))
            {
                this.PartialViewLocationFormats = this.PartialViewLocationFormats
                    .Select(it => it.Replace("~/Views/Shared", string.Format("~/{0}", basePartialViewPath))).ToArray();
            }

            originalViewLocations = this.ViewLocationFormats;
            originalMasterLocations = this.MasterLocationFormats;
            originalPartialViewLocations = this.PartialViewLocationFormats;
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            this.ViewLocationFormats = originalViewLocations;
            this.MasterLocationFormats = originalMasterLocations;
            if (ViewPathFilter != null)
            {
                this.ViewLocationFormats = this.ViewLocationFormats
                    .Select(it => ViewPathFilter.ProcessViewPath(it)).ToArray();
                this.MasterLocationFormats = this.MasterLocationFormats
                    .Select(it => ViewPathFilter.ProcessMasterPath(it)).ToArray();
            }

            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            this.PartialViewLocationFormats = originalPartialViewLocations;
            if (ViewPathFilter != null)
            {
                this.PartialViewLocationFormats = this.PartialViewLocationFormats
                    .Select(it => ViewPathFilter.ProcessPartialPath(it)).ToArray();
            }

            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return base.CreateView(controllerContext, viewPath, masterPath);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return base.CreatePartialView(controllerContext, partialPath);
        }
    }

    public static class ViewEnginesExtensions
    {
        public static void Register(this ViewEngineCollection context, IViewEngine viewEngine)
        {
            context.Insert(context.Count, viewEngine);
        }
    }

    public interface IViewPathFilter
    {
        string ProcessViewPath(string viewPath);
        string ProcessPartialPath(string partialPath);
        string ProcessMasterPath(string masterPath);
    }
}
