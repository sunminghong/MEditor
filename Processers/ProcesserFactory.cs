using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace MEditor.Processers
{
    /// <summary>
    /// 配置标签处理工厂，即给工厂添加处理器
    /// </summary>
    public class ProcesserFactory
    {
        private static SortedList<EMark,IProcesser> _procs=new SortedList<EMark,IProcesser>();

        static ProcesserFactory()
        {
            initProcesser();
        }
        /// <summary>
        /// 取得配置后的处理工厂对象
        /// </summary>
        /// <param name="serviceType">指定为哪类服务器类型配置消息处理工厂</param>
        /// <returns></returns>
        public static void Processe(System.Windows.Forms.RichTextBox rtb, EMark mark)
        {
            if (_procs.ContainsKey(mark))
            {
                _procs[mark].Process(rtb, mark);
            }
        }

        /// <summary>
        /// 在此集中给工厂添加处理器
        /// </summary>
        /// <param name="factory"></param>
        private static void initProcesser()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ProcesserFactory));
            //得到Assembly中的所有类型  
            Type[] types = assembly.GetTypes();

            //遍历所有的类型，找到插件类型，并创建插件实例并加载  
            foreach (Type type in types)
            {
                //判断类型是否派生自IPlugin接口  
                if (type.GetInterface("IProcesser") != null)
                {
                    //创建插件实例  
                    IProcesser processer = (IProcesser)Activator.CreateInstance(type);
                    foreach (EMark m in processer.ProcessMarks)
                    {
                        _procs.Add(m, processer);
                    }
                }
            }  
        }

    }
}
