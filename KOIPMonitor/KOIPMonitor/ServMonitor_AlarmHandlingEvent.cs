﻿using System;
using System.Collections.Generic;
using System.Text;
using ServMonitor;
using System.Data;
using System.Collections;
using System.Threading;
using KoIp.Commonality;
namespace KOIPMonitor
{
    class ServMonitor_AlarmHandlingEvent
    {
        public static void AlarmEvent(ServInfoState sender)
        {
            try
            {
                KoIp.Commonality.ConsoleManage.Write(KoIp.Commonality.ErrorLevel.Serious,"sender", "\n"+sender.ToString());
                UpdateServState(sender.ID, sender.TYPE, sender.STATE);

                short cmd1 = 0;//主命令字
                short cmd2 = 0;//子命令字
                cmd1 = OMSCmd.RspGetServState;
                cmd2 = ErrCommon.Success;
                byte[] ByteResult = null;

                //List表内容
                List<KoIp.Commonality.CommClass.TTable> ListTtable = new List<KoIp.Commonality.CommClass.TTable>();
                //Table属性内容
                KoIp.Commonality.CommClass.TTable Ttable = new KoIp.Commonality.CommClass.TTable();

                Ttable.FieldName = "ID";
                Ttable.FieldValue = sender.ID;
                ListTtable.Add(Ttable);

                Ttable.FieldName = "TYPE";
                Ttable.FieldValue = sender.TYPE;
                ListTtable.Add(Ttable);

                Ttable.FieldName = "STATE";
                Ttable.FieldValue = sender.STATE;
                ListTtable.Add(Ttable);

                KoIp.Commonality.CommClass.TableToByteArry(ListTtable, ref ByteResult);

                foreach (KeyValuePair<string, Kernel.StateObject> a in CommClass.ClientConnList)
                {
                    Hashtable _hashtable_Package = new Hashtable();
                    _hashtable_Package.Add("1", a.Value);//...連結位置 
                    _hashtable_Package.Add("2", cmd1);
                    _hashtable_Package.Add("3", cmd2);
                    _hashtable_Package.Add("4", ByteResult);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(CommonFunction.SendDatas), _hashtable_Package);
                }
                
                
            }
            catch (Exception ex)
            {
                //Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Serious, "BusinessDAL.KNS>>tcpClient_ExceptionHandlingEvent>>ReceiveEvent>>", ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// 更新服务器状态
        /// </summary>
        /// <param name="ID">服务器ID</param>
        /// <param name="TYPE">服务器类型</param>
        /// <param name="STATE">服务器状态{1:正常;2:未启动;3:未知}</param>
        public static void UpdateServState(string ID, string TYPE, string STATE)
        {
            try
            {
                if (CommClass.DtServList == null)
                {
                    return;
                }
                for (int i = 0; i < CommClass.DtServList.Rows.Count; i++)
                {
                    if ((CommClass.DtServList.Rows[i]["ID"].ToString() == ID) && (CommClass.DtServList.Rows[i]["TYPE"].ToString() == TYPE))
                    {
                        CommClass.DtServList.Rows[i]["STATE"] = STATE;
                    }
                }

                //for (int i = 0; i < CommClass.DtServList.Rows.Count; i++)
                //{

                //    Commonality.ConsoleManage.Write(Commonality.ErrorLevel.Serious, "CommClass.DtServList", CommClass.DtServList.Rows[i]["STATE"].ToString());

                //}

            }
            catch (Exception ex)
            {
                KoIp.Commonality.ConsoleManage.Write(KoIp.Commonality.ErrorLevel.Serious,
                                     "KoIp.BusinessDAL.KNS>>CommonFunction>>UpdateServState>>",
                                     ex.Message);
            }
            finally
            {
                ////GC.Collect();
            }
        }
    }
}
