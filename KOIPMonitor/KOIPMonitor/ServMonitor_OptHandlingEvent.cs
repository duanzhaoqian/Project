using System;
using System.Collections.Generic;
using System.Text;
using ServMonitor;
using System.Data;
using System.Collections;
using System.Threading;
namespace KOIPMonitor
{
    class ServMonitor_OptHandlingEvent
    {
        public static void OptEvent(ServOptState sender)
        {
            try
            {
                KoIp.Commonality.ConsoleManage.Write(KoIp.Commonality.ErrorLevel.Serious,"sender", "\n"+sender.ToString());

                short cmd1 = 0;//主命令字
                short cmd2 = 0;//子命令字
                cmd1 = OMSCmd.RspServOpt;
                cmd2 = ErrCommon.Success;
                byte[] ByteResult = null;
                try
                {
                    cmd2 = Convert.ToInt16(sender.OPTSTATE.ToString());
                }
                catch
                {
                    cmd2 = -1;
                }


                foreach (KeyValuePair<string, Kernel.StateObject> a in CommClass.ClientConnList)
                {
                    Hashtable _hashtable_Package=new Hashtable();
                    _hashtable_Package.Add("1", a.Value);//...連結位置 
                    _hashtable_Package.Add("2", cmd1);
                    _hashtable_Package.Add("3", cmd2);
                    _hashtable_Package.Add("4", ByteResult);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(CommonFunction.SendDatas),_hashtable_Package);
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

    }
}
