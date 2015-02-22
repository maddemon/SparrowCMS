using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Factories;

namespace SparrowCMS.Managers
{
    public class FactoryManager
    {
        private static FactoryManager _instance = new FactoryManager();
        public static FactoryManager GetInstance()
        {
            return _instance;
        }
        public List<ILabelFactory> GetLabelFactories()
        {
            //TODO
            return new List<ILabelFactory>() { new DefaultLabelFactory() };
        }

        //public List<IFieldFactory> GetFieldFactories()
        //{
        //    //TODO
        //    return new List<IFieldFactory>() { new DefaultFieldFactory() };
        //}

        //public List<IFunctionFactory> GetFunctionFactories()
        //{
        //    //TODO
        //    return new List<IFunctionFactory>() { new DefaultFunctionFactory() };
        //}

        public List<IApiFactory> GetApiFactories()
        {
            //TODO
            return new List<IApiFactory>() { new DefaultApiFactory() };
        }
    }
}
