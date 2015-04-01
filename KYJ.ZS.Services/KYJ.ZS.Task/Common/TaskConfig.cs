using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace KYJ.ZS.Task
{
    using KYJ.ZS.Log4net;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/5/27 9:38:45
    /// 描述：任务配置
    /// </summary>
    public class TaskConfig
    {
        public static IList<TaskConfigElement> GetTasks()
        {
            var list = new List<TaskConfigElement>();
            var section = ConfigurationManager.GetSection("taskconfig") as TaskConfigSection;
            if (section != null)
            {
                foreach (TaskConfigElement task in section.Tasks)
                {
                    if ( task == null || !task.Enable || task.Instance == null)
                    {
                        continue;
                    }

                    if (CheckTaskRuntime(task.RunTime, DateTime.Now))
                    {
                        list.Add(task);
                    }
                }
            }
            
            return list;
        }

        public static bool CheckTaskRuntime(string runTime, DateTime targetTime)
        {
            var setting = runTime.Split(' ');

            if (setting.Length != 6)
            {
                throw new Exception(String.Format("Task run time config error : {0}", runTime));
            }
            var second = setting[0];
            var min = setting[1];
            var hour = setting[2];
            var day = setting[3];
            var mon = setting[4];
            var week = setting[5];

            return CheckTimePart(second, targetTime.Second)
                && CheckTimePart(min, targetTime.Minute)
                && CheckTimePart(hour, targetTime.Hour)
                && CheckTimePart(day, targetTime.Day)
                && CheckTimePart(mon, targetTime.Month)
                && CheckTimePart(week, (int)targetTime.DayOfWeek);
        }

        private static bool CheckTimePart(string setting, int timeIndex)
        {
            if (setting != "*")
            {
                if (!setting.Split(',').Contains(timeIndex.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class TaskConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("runtime", IsRequired = true)]
        public string RunTime
        {
            get { return (string) this["runtime"]; }
            set { this["runtime"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string TypeName
        {
            get { return (string) this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("enable", IsRequired = false, DefaultValue = false)]
        public bool Enable
        {
            get { return (bool) this["enable"]; }
            set { this["enable"] = value; }
        }

        private ITask _instance;
        bool _instanceFlag = false;
        public ITask Instance
        {
            //ITask _instance;
            get
            {
                if ( _instance == null && !_instanceFlag )
                {
                    _instanceFlag = true;

                    try
                    {
                        var type = Type.GetType( TypeName );
                        _instance = Activator.CreateInstance( type ) as ITask;
                    }
                    catch ( Exception ex )
                    {
                        //LogHelper.Info( TypeName );
                        //LogHelper.Error( ex.Message, ex );
                        RecordLog.ServiceLogs("TypeName", string.Format("ex.Message:{0};ex:{1}", ex.Message,ex));
                    }
                }

                //if (_instance == null)
                //{
                //    throw new Exception("Task'sType is not implement ITask interface");
                //}

                return _instance;
            }
        }
    }

    public class TaskConfigCollection : ConfigurationElementCollection
    {

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TaskConfigElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((TaskConfigElement)element).Name;
        }

        public TaskConfigElement this[int index]
        {
            get
            {
                return (TaskConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public TaskConfigElement this[string name]
        {
            get
            {
                return (TaskConfigElement)BaseGet(name);
            }
        }

        public int IndexOf(TaskConfigElement account)
        {
            return BaseIndexOf(account);
        }

        public void Add(TaskConfigElement account)
        {
            BaseAdd(account);
        }
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(TaskConfigElement task)
        {
            if (BaseIndexOf(task) >= 0)
                BaseRemove(task.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }

    public class TaskConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("triggerinterval", DefaultValue = 30)]
        public int Interval
        {
            get { return (int)this["triggerinterval"]; }
            set { this["triggerinterval"] = value; }
        }

        [ConfigurationProperty("tasks", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(TaskConfigCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public TaskConfigCollection Tasks
        {
            get
            {
                TaskConfigCollection collection =
                    (TaskConfigCollection)base["tasks"];
                return collection;
            }
        }
    }
}
