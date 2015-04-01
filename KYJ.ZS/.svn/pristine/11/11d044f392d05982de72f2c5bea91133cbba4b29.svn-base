using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/21 13:16:54
    /// 描述：
    /// </summary>
    public static class ClassValueCopyHelper
    {
        #region Copy

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="source">来源</param>
        /// <returns>成功复制的值个数</returns>
        public static int Copy<TSource, TTarget>(TSource source, TTarget target)
        {
            if (target == null || source == null)
            {
                return 0;
            }
            return Copy<TSource, TTarget>(source, target, null);
        }

        public static int Copy<TSource, TTarget>(TSource source, TTarget target, bool ignoreSourceNullProperty)
        {
            return Copy<TTarget>(source, target, ignoreSourceNullProperty, null);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="source">来源</param>
        /// <param name="type">复制的属性字段模板</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int Copy<TSource, TTarget>(TSource source, TTarget target, params string[] excludeName)
        {
            return Copy<TTarget>(source, target, false, excludeName);
        }

        public static int Copy<TTarget>(object source, TTarget target, bool ignoreSourceNullProperty, params string[] excludeName)
        {
            if (target == null || source == null)
            {
                return 0;
            }

            if (excludeName == null)
            {
                excludeName = new List<string>().ToArray();
            }

            int i = 0;

            Type type = source.GetType();
            Type desType = typeof(TTarget);

            var
                sourceFields = type.GetFields();
            foreach (FieldInfo field in sourceFields)
            {
                if (excludeName.Contains(field.Name))
                    continue;
                FieldInfo
                    targetField = desType.GetField(field.Name);
                if (targetField != null && targetField.FieldType == field.FieldType)
                {
                    var v = field.GetValue(source);
                    if (v == null && ignoreSourceNullProperty)
                        continue;

                    targetField.SetValue(target, v);
                    i++;
                }
            }

            var
                sourceProperties = type.GetProperties();
            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                if (excludeName.Contains(sourceProperty.Name))
                {
                    continue;
                }
                if (!sourceProperty.CanRead)
                    continue;
                PropertyInfo
                    targetProperty = desType.GetProperty(sourceProperty.Name);
                if (targetProperty != null &&
                    targetProperty.PropertyType == sourceProperty.PropertyType &&
                    targetProperty.CanWrite)
                {
                    var v = sourceProperty.GetValue(source, null);
                    if (v == null && ignoreSourceNullProperty)
                        continue;

                    targetProperty.SetValue(target, v, null);
                    i++;
                }
            }
            return i;
        }

        #endregion

        #region GetPropertyNames

        /// <summary>
        /// 获取实体类的属性名称
        /// </summary>
        /// <param name="source">实体类</param>
        /// <returns>属性名称列表</returns>
        public static List<string> GetPropertyNames(this object source)
        {
            if (source == null)
                return new List<string>();

            return GetPropertyNames(source.GetType());
        }

        /// <summary>
        /// 获取类类型的属性名称（按声明顺序）
        /// </summary>
        /// <param name="source">类类型</param>
        /// <returns>属性名称列表</returns>
        public static List<string> GetPropertyNames(this Type source)
        {
            return GetPropertyNames(source, true);
        }

        /// <summary>
        /// 获取类类型的属性名称
        /// </summary>
        /// <param name="source">类类型</param>
        /// <param name="declarationOrder">是否按声明顺序排序</param>
        /// <returns>属性名称列表</returns>
        public static List<string> GetPropertyNames(this Type source, bool declarationOrder)
        {
            if (source == null)
                return new List<string>();

            var list = source.GetProperties().AsQueryable();
            if (declarationOrder)
            {
                list = list.OrderBy(p => p.MetadataToken);
            }
            return list.Select(o => o.Name).ToList();
        }

        #endregion

        #region CopyValueFrom

        /*
        /// <summary>
        /// 从源对象赋值到当前对象
        /// </summary>
        /// <param name="target">当前对象</param>
        /// <param name="source">源对象</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyValueFrom( this object target, object source )
        {
            return CopyValueFrom( target, source, null );
        }

        /// <summary>
        /// 从源对象赋值到当前对象
        /// </summary>
        /// <param name="target">当前对象</param>
        /// <param name="source">源对象</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyValueFrom( this object target, object source, IEnumerable<string> excludeName )
        {
            if( target == null || source == null )
                return 0;

            return Copy<object>( source, target, excludeName );
        }
        */
        #endregion

        #region CopyValueTo

        public static List<TTarget> CopyValueTo<TSource, TTarget>(this List<TSource> list) where TTarget : new()
        {
            var
                mlist = new List<TTarget>();
            if (list == null || list.Count == 0)
                return mlist;

            foreach (var o in list)
            {
                mlist.Add(o.CopyValueTo<TTarget>());
            }

            return mlist;
        }

        public static TTarget CopyValueTo<TTarget>(this object source) where TTarget : new()
        {
            if (source == null)
                return default(TTarget);

            TTarget
                target = Activator.CreateInstance<TTarget>();

            Copy<TTarget>(source, target, false, null);

            return target;
        }

        public static int CopyValueTo<TTarget>(this object source, TTarget target)
        {
            return Copy<TTarget>(source, target, false, null);
        }

        public static int CopyValueTo<TTarget>(this object source, TTarget target, bool ignoreSourceNullProperty)
        {
            return Copy<TTarget>(source, target, ignoreSourceNullProperty, null);
        }

        public static int CopyValueTo<TTarget>(this object source, TTarget target, params string[] excludeName)
        {
            return Copy<TTarget>(source, target, false, excludeName);
        }

        /// <summary>
        /// 从当前对象赋值到目标对象
        /// </summary>
        /// <param name="source">当前对象</param>
        /// <param name="target">目标对象</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyValueTo(this object source, object target)
        {
            return CopyValueTo(source, target, null);
        }

        /// <summary>
        /// 从当前对象赋值到目标对象
        /// </summary>
        /// <param name="source">当前对象</param>
        /// <param name="target">目标对象</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyValueTo(this object source, object target, params string[] excludeName)
        {
            if (target == null || source == null)
                return 0;

            return Copy<object>(source, target, false, excludeName);
        }

        public static TTarget CopyValueTo<TTarget>(this SqlDataReader reader) where TTarget : new()
        {
            TTarget
                item = default(TTarget);

            return reader.CopyValueTo<TTarget>(item);
        }
        public static TTarget CopyValueTo<TTarget>(this SqlDataReader reader, TTarget item) where TTarget : new()
        {
            if (reader == null || !reader.HasRows)
                return item;

            if (item == null)
                item = Activator.CreateInstance<TTarget>();

            var
                sourceProperties = item.GetType().GetProperties();
            var
                count = reader.FieldCount;

            for (var i = 0; i < count; i++)
            {
                var
                    name = reader.GetName(i);//列名
                var
                    prop = sourceProperties.Where(o => string.Compare(o.Name, name, true) == 0).FirstOrDefault();//属性
                if (prop == null)//该类未包含该名称的属性
                    continue;

                if (!prop.CanWrite)//只读
                    continue;

                var
                    value = reader.GetValue(i);

                if (value == DBNull.Value)
                    value = null;

                if (value == null)
                {
                    if (!prop.PropertyType.FullName.StartsWith("System.Nullable"))
                    {
                        if (prop.PropertyType.IsValueType)
                        {
                            if (prop.PropertyType == typeof(DateTime))
                            {
                                value = new DateTime(0x76c, 1, 1);
                            }
                            else if (prop.PropertyType == typeof(bool))
                            {
                                value = false;
                            }
                            else if (prop.PropertyType == typeof(Guid))
                            {
                                value = Guid.Empty;
                            }
                            else
                            {
                                value = 0;
                            }
                        }
                    }
                }

                prop.SetValue(item, value, null);
            }

            return item;
        }

        public static IList<TTarget> CopyValueTo<TTarget>(this DataTable table) where TTarget : new()
        {
            IList<TTarget> list = new List<TTarget>();


            if (table == null || table.Rows.Count == 0)
                return list;

            var
                count = table.Columns.Count;
            var
                sourceProperties = typeof(TTarget).GetProperties();

            foreach (DataRow row in table.Rows)
            {
                TTarget item = Activator.CreateInstance<TTarget>();

                for (var i = 0; i < count; i++)
                {
                    var
                        name = table.Columns[i].ColumnName;//列名
                    var
                        prop = sourceProperties.Where(o => string.Compare(o.Name, name, true) == 0).FirstOrDefault();//属性
                    if (prop == null)//该类未包含该名称的属性
                        continue;

                    if (!prop.CanWrite)//只读
                        continue;
                    var
                        value = row[i];

                    if (value == DBNull.Value)
                        value = null;

                    if (value == null)
                    {
                        if (!prop.PropertyType.FullName.StartsWith("System.Nullable"))
                        {
                            if (prop.PropertyType.IsValueType)
                            {
                                if (prop.PropertyType == typeof(DateTime))
                                {
                                    value = new DateTime(0x76c, 1, 1);
                                }
                                else if (prop.PropertyType == typeof(bool))
                                {
                                    value = false;
                                }
                                else if (prop.PropertyType == typeof(Guid))
                                {
                                    value = Guid.Empty;
                                }
                                else if (prop.PropertyType == typeof(Byte))
                                {
                                    value = false;
                                }
                                else
                                {
                                    value = 0;
                                }
                            }
                        }
                    }

                    Guid g;
                    try
                    {
                        if (value != null && value.ToString().IsGuid(out g))
                            prop.SetValue(item, g, null);
                        else
                        {
                            if (value != null && prop.PropertyType == typeof(string) && value.GetType() != typeof(string))
                            {
                                prop.SetValue(item, value.ToString(), null);
                            }
                            else if (prop.PropertyType == typeof(bool?))
                            {
                                prop.SetValue(item, value == (object)1, null);
                            }
                            else
                            {
                                prop.SetValue(item, value, null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //LogHelper.Error("CopyValueTo Exception:{3}。Property:{0},PropertyType:{1},Value:{2}".FormatString(name, prop.PropertyType.FullName, value, ex.Message), ex);
                    }
                }

                list.Add(item);
            }

            return list;
        }

        #endregion

    }
}
